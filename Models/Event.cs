namespace Events.Models {
    public class Event {
        public int Id { get; set; }
        public string Theme { get; set; } = "Absent";
        public string Description { get; set; } = "Absent";
        public string Organizer { get; set; } = String.Empty;
        public string Speaker { get; set; } = "Absent";
        public DateTime? Time { get; set; }
        public string Place { get; set; } = String.Empty;
    }
}
