namespace Order.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy  { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }   
    }
}
