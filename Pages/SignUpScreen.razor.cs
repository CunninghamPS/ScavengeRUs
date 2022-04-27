using ScavengeRUs.Models;
using ScavengeRUs.Data;
using Syncfusion.Blazor.Popups;
using System.Text.RegularExpressions;

namespace ScavengeRUs.Pages
{
    public partial class SignUpScreen
    {
        private Models.Account Account = new Account();
        private string firstNameStyle = "", lastNameStyle = "", emailStyle = "", phoneStyle = "", passwordStyle = "";
        private string invalidFirst = "", invalidLast = "", invalidEmail = "", invalidPhone = "", invalidPass = "";
        private bool validInput = false;
        Regex passWordRgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        Regex emailRgx = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        Regex phoneNumberRgx = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$");
        private void HandleValidSubmit()
        {
            Random random = new Random();
            Account.UserName = random.Next(111111,999999).ToString();
            try
            {
                validInput = true;
                if(Account.FirstName.Equals("") || Account.FirstName.Length > 100)
                {
                    invalidFirst = "First name is required.";
                    if (Account.FirstName.Length > 100)
                        invalidFirst = "First name cannot exceed 100 characters.";

                    firstNameStyle = "border: .5px solid red";
                    validInput = false;
                }
                else
                    invalidFirst = "";
                
                if (Account.LastName.Equals("") || Account.LastName.Length > 100)
                {
                    invalidLast = "Last name is required.";
                    if (Account.LastName.Length > 100)
                        invalidLast = "Last name cannot exceed 100 characters.";

                    lastNameStyle = "border: .5px solid red";
                    validInput = false;
                }
                else
                    invalidLast = "";
                
                if (Account.Email.Equals("") || !emailRgx.IsMatch(Account.Email))
                {
                    invalidEmail = "Please enter a valid email address.";
                    if (Account.Email.Equals(""))
                        invalidEmail = "Email address is required.";
                    
                    emailStyle = "border: .5px solid red";
                    validInput = false;
                }
                else
                    invalidEmail = "";
                
                if (Account.Password.Equals("") || Account.Password.Length > 100 || !passWordRgx.IsMatch(Account.Password))
                {
                    invalidPass = "Password must be 8 characters long with at least 1 uppercase and 1 lowercase letter, 1 number and 1 special character.";
                    if (Account.Password.Length > 100)
                        invalidPass = "Password cannot exceed 100 characters.";
                    
                    if (Account.Password.Equals(""))
                        invalidPass = "Password is required.";

                    passwordStyle = "border: .5px solid red";
                    validInput = false;
                }
                else
                    invalidPass = "";
                
                if (Account.PhoneNumber.Equals("") || !phoneNumberRgx.IsMatch(Account.PhoneNumber))
                {
                    invalidPhone = "Please enter a valid phone number in the format \"###-###-####\".";
                    if (Account.PhoneNumber.Equals(""))
                        invalidPhone = "Phone number is required.";
                    
                    phoneStyle = "border: .5px solid red";
                    validInput = false;
                }
                else
                    invalidPhone = "";

                if (validInput)
                {
                    firstNameStyle = "";
                    lastNameStyle = "";
                    emailStyle = "";
                    passwordStyle = "";
                    phoneStyle = "";
                    string accessCode = DBTest.newAccount(Account.Email, Account.DOB, Account.FirstName, Account.LastName, Account.PhoneNumber, Account.UserName, Account.Password);
                    EmailSMSTest.send(Account.Email, Account.PhoneNumber, accessCode);
                    Navigate();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Submit");
            }
            Console.WriteLine("Valid Submit");
        }
        void Navigate()
        {
            UriHelper.NavigateTo("/loginScreen");

        }
        void NavigateTermsNCond()
        {
            OpenDialog();
        }

        private bool IsVisible { get; set; } = false;
        private bool SubmitBtnDisabled { get; set; } = true;
        private string ErrorMessage { get; set; } = "";
        private ResizeDirection[] dialogResizeDirections { get; set; } = new ResizeDirection[] { ResizeDirection.All };

        private void OpenDialog()
        {
            this.IsVisible = true;
        }

        private void OnSubmit()
        {
            this.IsVisible = false;
            SubmitBtnDisabled = false;
        }

        private void ClearFormat()
        {
            firstNameStyle = "";
            lastNameStyle = "";
            emailStyle = "";
            passwordStyle = "";
            phoneStyle = "";
        }

    }
}
