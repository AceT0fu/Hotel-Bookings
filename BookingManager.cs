public class BookingManager : IBookingManager {

    IDictionary<int, RoomBooking> rooms = new Dictionary<int, RoomBooking>();
    // IEnumerable<int> rooms = new int[]{101, 102, 201, 203};
    public BookingManager() {
        rooms.Add(101, new RoomBooking());
        rooms.Add(102, new RoomBooking());
        rooms.Add(201, new RoomBooking());
        rooms.Add(203, new RoomBooking());
    }

    public bool IsRoomAvailable(int room, DateTime date) {
        if (rooms.ContainsKey(room)) {
            RoomBooking booking = rooms[room];
            return booking.isDateAvailable(date);
        } else {
            throw new InvalidOperationException("Invalid room");
        }
    }

    public void AddBooking(string guest, int room, DateTime date) {
        
        if (IsRoomAvailable(room, date)) {
            RoomBooking booking = rooms[room];
            booking.book(date, guest);
        } else {
            throw new InvalidOperationException("Booking unavailable");
        }
    }

    public IEnumerable<int> getAvailableRooms(DateTime date) {
        int[] free = new int[rooms.Count];
        int index = 0;

        foreach (var room in rooms) {
            int roomNo = room.Key;
            RoomBooking booking = room.Value;

            if (booking.isDateAvailable(date)) {
                free[index] = roomNo;
                index++;
                Console.Write(roomNo + " ");
            }
        }
        Console.WriteLine();
        return free;
    }

    static void Main(string[] args) {
        BookingManager bm = new BookingManager();
        var today = new DateTime(2012, 3, 28);
        var tomorrow = new DateTime(2012, 3, 29);
        Console.WriteLine(bm.IsRoomAvailable(101, today)); //outputs true
        bm.AddBooking("Patel", 101, today);
        Console.WriteLine(bm.IsRoomAvailable(101, today)); // outputs false

        // book same room different day
        Console.WriteLine(bm.IsRoomAvailable(101, tomorrow)); // outputs true
        Console.WriteLine(bm.IsRoomAvailable(102, today)); // outputs true
        bm.AddBooking("Wang", 101, tomorrow);
        Console.WriteLine(bm.IsRoomAvailable(101, tomorrow)); // outputs false
        // bm.AddBooking("Li", 101, today); // throws an exception

        bm.getAvailableRooms(today); // 102 201 203

        bm.AddBooking("Smith", 102, tomorrow); 
        bm.getAvailableRooms(tomorrow); // 201 203
    }
}