using DamTunsi.Models.Enums;

namespace DamTunsi.Models
{
    public class Donor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public DateTime LastDonationDate { get; set; }

        // Computed - not stored
        public DateTime NextEligibleDate() => LastDonationDate.AddDays(56);
        public bool CanDonate() => IsAvailable && DateTime.Today >= NextEligibleDate();
        public void ToggleAvailability() => IsAvailable = !IsAvailable;

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
