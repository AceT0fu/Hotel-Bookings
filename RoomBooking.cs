public class RoomBooking {

    IDictionary<DateTime, String> bookings = new Dictionary<DateTime, String>();

    public void book(DateTime date, string guest) {
        // lock individual room
        lock (bookings) {
            if (isDateAvailable(date)) {
                bookings.Add(date, guest);
            } else {
                throw new InvalidOperationException("Booking unavailable");
            }
        }
    }

    public bool isDateAvailable(DateTime date) {
        // lock ensures value returned is always up to date
        // we could remove the lock as there is little thread-safety risk here
        lock(bookings) {
            return !bookings.ContainsKey(date);
        }
    }
}