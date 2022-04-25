using ScavengeRUs.Models;
using ScavengeRUs.Data;
using System.Text.RegularExpressions;

namespace ScavengeRUs.Pages
{
    public partial class UserAccount
    {
        private string firstNameStyle = "", lastNameStyle = "", emailStyle = "", phoneStyle = "", displayNameStyle = "";
        private string invalidFirst = "", invalidLast = "", invalidEmail = "", invalidPhone = "", invalidDisplayName = "";
        private string stringOnSubmit = "";
        private bool validInput = false;
        Regex passWordRgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        Regex emailRgx = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        Regex phoneNumberRgx = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$");

        private Account Account = new Account();
        private bool IsVisible { get; set; } = false;
        private bool SubmitBtnDisabled { get; set; } = true;
        private string ErrorMessage { get; set; } = "";
        private void HandleValidSubmit()
        {
            try
            {
                validInput = true;
                if (Account.FirstName.Equals("") || Account.FirstName.Length > 100)
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

                if (Account.UserName.Equals("") || Account.UserName.Length > 15)
                {
                    invalidDisplayName = "Display name is required.";
                    if (Account.UserName.Length > 15)
                        invalidDisplayName = "Display name cannot exceed 15 characters.";

                    displayNameStyle = "border: .5px solid red";
                    validInput = false;
                }
                else
                    invalidDisplayName = "";

                if (validInput)
                {
                    firstNameStyle = "";
                    lastNameStyle = "";
                    emailStyle = "";
                    phoneStyle = "";
                    DBTest.updateUser(secretKey, Account.FirstName, Account.LastName, Account.UserName, Account.Email, Account.PhoneNumber);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Update");
            }
            Console.WriteLine("Valid Update");
            stringOnSubmit = "Changes Saved!";
        }

        protected override System.Threading.Tasks.Task OnInitializedAsync()
        {
            stringOnSubmit = "";
            try
            {
                List<string> values = DBTest.getUserInfo(secretKey);
                Account.Email = values[1];
                Account.DOB = values[2];
                Account.FirstName = values[3];
                Account.LastName = values[4];
                Account.PhoneNumber = values[5];
                Account.UserName = values[6];
            }
            catch(Exception)
            {
                Console.WriteLine("Error Getting Logged In User Info");
            }
            return base.OnInitializedAsync();
        }

       

        private void ClearFormat()
        {
            firstNameStyle = "";
            lastNameStyle = "";
            emailStyle = "";
            phoneStyle = "";
            displayNameStyle = "";
        }

        private void logout()
        {
            if (secretKey != null)
            {
                DBTest.logout(secretKey);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                DBTest.addUsersToGame();
            }
        }

        public async Task<bool> navigateTo(string dest)
        {
            string[] paths = { "profile", "security" };
            string url = NavigationManager.Uri;
            int length = NavigationManager.Uri.Split('/').Length;
            string addon = NavigationManager.Uri.Split('/')[length - 1];
            Console.WriteLine(url);
            Console.WriteLine(addon);
            if (paths.Contains(addon))
            {
                NavigationManager.NavigateTo("/" + dest);
            }
            else
                NavigationManager.NavigateTo("/" + dest + "/" + addon);
            return await System.Threading.Tasks.Task.FromResult(true);
        }
    }
}
