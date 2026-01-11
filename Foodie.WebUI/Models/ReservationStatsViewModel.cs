namespace Foodie.WebUI.Models
{
    public class ReservationStatsViewModel
    {
        public int TotalReservations { get; set; }
        public int CustomerCount { get; set; }
        public int ConfirmedReservations { get; set; }
        public int PendingReservations { get; set; }
    }
}
