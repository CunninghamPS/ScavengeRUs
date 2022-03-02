namespace ScavengeRUs.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Account
    {
        [Required]
        [StringLength(100, ErrorMessage = "First Name must be shorter than 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "Last Name must be shorter than 100 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"(0[1-9]|[12][0-9]|[3][01])\/(0[1-9]|1[0-2])\/\d{4}")]
        public string DOB { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]

        public string UserName { get; set; } = string.Empty;

        [Required]

        public string Password { get; set; } = string.Empty;
    }
}