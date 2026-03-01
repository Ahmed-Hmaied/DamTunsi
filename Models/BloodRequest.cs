using DamTunsi.Models.Enums;

namespace DamTunsi.Models
{
    public class BloodRequest
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public int QuantityNeeded { get; set; }
        public UrgencyLevel UrgencyLevel { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Open;
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public DateTime ExpiresAt { get; set; }

        public bool IsExpired() => DateTime.Now > ExpiresAt;
        public void MarkAsFulfilled() => Status = RequestStatus.Fulfilled;

        // Navigation properties
        public Hospital Hospital { get; set; } = null!;
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
