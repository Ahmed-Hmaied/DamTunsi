using DamTunsi.Models.Enums;

namespace DamTunsi.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int RequestId { get; set; }
        public DateTime DonationDate { get; set; } = DateTime.Now;
        public DonationStatus Status { get; set; } = DonationStatus.Pending;
        public string Notes { get; set; } = string.Empty;

        public void Confirm() => Status = DonationStatus.Confirmed;
        public void Cancel() => Status = DonationStatus.Cancelled;

        // Navigation properties
        public Donor Donor { get; set; } = null!;
        public BloodRequest Request { get; set; } = null!;
    }
}
