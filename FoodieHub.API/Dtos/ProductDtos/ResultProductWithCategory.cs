namespace FoodieHub.API.Dtos.ProductDtos
{
    public class ResultProductWithCategory
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
