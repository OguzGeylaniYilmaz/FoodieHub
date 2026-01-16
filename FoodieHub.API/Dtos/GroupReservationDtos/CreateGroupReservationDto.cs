namespace FoodieHub.API.Dtos.GroupReservationDtos
{
    public class CreateGroupReservationDto
    {
        public string PersoToContact { get; set; }
        public string GroupTitle { get; set; }
        public int PersonCount { get; set; }
        public string Email { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Details { get; set; }
        public string ReservationStatus { get; set; }
    }
}
