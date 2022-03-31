namespace ScavengeRUs.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Account
    {
        [Required]
        [StringLength(100, ErrorMessage = "*First name cannot exceed 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "*Last name cannot exceed 100 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$", ErrorMessage="*Date of Birth must be in MM/DD/YYYY\n")]
        [StringLength(100, ErrorMessage = "*Date of Birth must be in MM/DD/YYYY")]
        public string DOB { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "*Please enter a valid email address.\n")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone(ErrorMessage="*Please enter a valid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]

        public string UserName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "*Password must be 8 characters long.\nAt least 1 uppercase and 1 lowercase letter, 1 number and 1 special character.\n")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; } = string.Empty;
    }
}