namespace HotelBooking2.Models
{
    public class Room
    {
        public Guid RoomID { get; set; }
        public string Name { get; set; }
        public int Pax { get; set; }
        public decimal Price { get; set; }

        public ICollection<BookingRoom> BookingRooms { get; set; }
    }

}
