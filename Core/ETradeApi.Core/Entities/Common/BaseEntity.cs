namespace ETradeApi.Core.Entities.Common;

	public abstract class BaseEntity
	{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
}
