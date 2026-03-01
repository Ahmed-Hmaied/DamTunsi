using DamTunsi.Models.Enums;

namespace DamTunsi.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string HospitalName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string LicenseFilePath { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public HospitalStatus Status { get; set; } = HospitalStatus.Pending;
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // Computed - not stored
        public bool IsApproved => Status == HospitalStatus.Approved;

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<BloodRequest> Requests { get; set; } = new List<BloodRequest>();
    }
}
