namespace ScavengeRUs.Data
{
    using System.ComponentModel.DataAnnotations;
    public class AccountModel
    {
        [StringLength(100, ErrorMessage = "First Name must be shorter than 100 characters")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Last Name must be shorter than 100 characters")]
        public string LastName { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        [StringLength(100, ErrorMessage = "Password must be 8 characters long. At least 1 uppercase and 1 lowercase letter, 1 number and 1 special character")]
        public string Password { get; set; } = string.Empty;
    }
}
