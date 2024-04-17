using System.ComponentModel.DataAnnotations;

namespace HotelBooking2.Models
{
    public class Booking
    {
        public Guid BookingID { get; set; }
        public Guid CustomerID { get; set; }
        public decimal TotalPrice { get; set; }

        public Customer Customer { get; set; }
        public ICollection<BookingRoom> BookingRooms { get; set; }
    }
}
