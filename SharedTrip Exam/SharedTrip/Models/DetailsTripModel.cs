namespace SharedTrip.Models
{
    public class DetailsTripModel
    {
        public string Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string DepartureTime { get; set; }
        public string Description { get; set; }
        public int Seats { get; set; }
    }
}
