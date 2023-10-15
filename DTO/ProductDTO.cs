namespace ProductsAPI.DTO
{

    public class ProductDTO
    {
        public int ProductId { get; set; }
        
        public string ProducName { get; set; }=null!;

        public decimal Price { get; set; }
    }
}