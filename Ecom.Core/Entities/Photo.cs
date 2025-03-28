namespace Ecom.Core.Entities
{
    public class Photo : BaseEntity<int>
    {
        public string ImageName { get; set; }
        //[ForeignKey("Product")]
        public int ProductId { get; set; }
        //public Product Product { get; set; }
    }
}
