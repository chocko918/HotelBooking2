namespace HotelBooking2.Models
{
    public class BookingRoom
    {
        public Guid BookingRoomID { get; set; }
        public Guid BookingID { get; set; }
        public Guid RoomID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Booking Booking { get; set; }
        public Room Room { get; set; }
    }
}
