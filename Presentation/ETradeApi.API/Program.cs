using ETradeApi.API.Middlewares;
using ETradeApi.Application;
using ETradeApi.Application.Validations;
using ETradeApi.Infrastructure;
using ETradeApi.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using System.Text;
using ETradeApi.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

//Cors
builder.Services.AddCors(opt => opt.AddDefaultPolicy(p =>
{
	p.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
}));

// SeriLog
var columnOptions = new ColumnOptions
{
	AdditionalColumns = new Collection<SqlColumn>
	{
		new SqlColumn
			{ColumnName = "Username",DataType=SqlDbType.NVarChar,PropertyName="Username"},
	}
};
Logger log = new LoggerConfiguration()
	.WriteTo.Console() //Cosnole logla
	.WriteTo.File("logs/log.txt") //Dosyaya logla
	.WriteTo.MSSqlServer(
	connectionString: builder.Configuration.GetConnectionString("MsSQL"), 
	sinkOptions: new MSSqlServerSinkOptions 
				{ TableName = "Logs", AutoCreateSqlTable = true },
	columnOptions:columnOptions)
	.CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.AddControllers().AddFluentValidation(conf =>
	conf.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("Admin", opt =>
	{
		opt.TokenValidationParameters = new()
		{
			ValidateAudience = true, //Token'ý hangi siteler kullansýn
			ValidateIssuer = true, //Token'ý daðýtacak site
			ValidateLifetime = true, //Token'ýn süresi
			ValidateIssuerSigningKey = true, //Token'ýn bizim projeye ait olduðunu belirten bir security key.

			ValidAudience = builder.Configuration["Token:Audience"],
			ValidIssuer = builder.Configuration["Token:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
			//Access token süresi için yapý
			LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

			NameClaimType = ClaimTypes.Name
		};
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.ConfigureExtensionHandler();
app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
//app.UseLogUserNameMiddlaware();
app.MapControllers();
// SignalR Map Hubs
app.MapHubs();
app.Run();