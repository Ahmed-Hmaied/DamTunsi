namespace DamTunsi.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.Now;
        public string Channel { get; set; } = string.Empty;

        public void MarkAsRead() => IsRead = true;

        // Navigation properties
        public User User { get; set; } = null!;
    }
}
