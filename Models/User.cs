using DamTunsi.Models.Enums;

namespace DamTunsi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;

        // Computed - not stored in DB
        public int Age => DateTime.Today.Year - BirthDate.Year;
        public bool IsEligibleAge() => Age >= 18 && Age <= 65;

        // Password methods
        public void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        // Navigation properties
        public Donor? Donor { get; set; }
        public Hospital? Hospital { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
