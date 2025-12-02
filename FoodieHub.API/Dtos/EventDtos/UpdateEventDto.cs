namespace FoodieHub.API.Dtos.EventDtos
{
    public class UpdateEventDto
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
    }
}
