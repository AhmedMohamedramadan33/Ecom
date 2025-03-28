using Microsoft.AspNetCore.Http;

namespace Ecom.Core.DTO
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<string> Photos { get; set; } = new List<string>();
        //public virtual List<PhotoDto> Photos { get; set; }
    }
    public record PhotoDto
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }
    public record AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photo { get; set; }
    }
    public record UpdateProductDto : AddProductDto
    {
        public IFormFileCollection? Photo { get; set; }

    }
}
