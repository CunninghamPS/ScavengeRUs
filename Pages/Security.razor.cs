using ScavengeRUs.Models;
using ScavengeRUs.Data;
using Syncfusion.Blazor.Popups;
using System.Text.RegularExpressions;

namespace ScavengeRUs.Pages
{
    public partial class Security
    {
        private string Pass1 = "", Pass2 = "";
        private string inputStyle = "";
        private string feedback = "";
        private string stringOnSubmit = "";
        private Account Account = new Account();
        private bool IsVisible { get; set; } = false;
        private bool SubmitBtnDisabled { get; set; } = true;
        private ResizeDirection[] dialogResizeDirections { get; set; } = new ResizeDirection[] { ResizeDirection.All };


        protected override System.Threading.Tasks.Task OnInitializedAsync()
        {
            try
            {
                List<string> values = DBTest.getUserInfo(secretKey);
                Account.FirstName = values[3];
                Account.LastName = values[4];
                Account.UserName = values[6];
            }
            catch (Exception)
            {
                Console.WriteLine("Error Getting Logged In User Info");
            }
            return base.OnInitializedAsync();
        }

        private void HandleValidSubmit()
        {
            Regex rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            try
            {
                if (Pass1.Equals("") || Pass2.Equals(""))
                    throw new Exception();

                if (!Pass1.Equals(Pass2))
                    throw new Exception();
                else
                {
                    if (rgx.IsMatch(Pass1))
                    {
                        Account.Password = Pass1;
                        DBTest.updateUserPassword(secretKey, Account.Password);
                        stringOnSubmit = "Password Updated!";
                        inputStyle = "";
                        feedback = "";
                    }
                    else
                    {
                        feedback = "Password must be 8 characters long with at least 1 uppercase and 1 lowercase letter, 1 number and 1 special character.";
                        inputStyle = "border: .5px solid red";
                    }
                }

            }
            catch (Exception)
            {
                feedback = "Passwords must match";
                inputStyle = "border: .5px solid red";
            }
            Console.WriteLine("Valid Submit");
        }

        private void ClearFormat()
        {
            inputStyle = "";
            stringOnSubmit = "";
        }

        private void deleteUser()
        {
            try
            {
                DBTest.deleteUserFromDB(secretKey);
                logout();
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot delete user");
            }
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

        void confirmDelete()
        {
            OpenDialog();
        }

        private void OpenDialog()
        {
            this.IsVisible = true;
        }

        private void OnSubmit()
        {
            this.IsVisible = false;
            SubmitBtnDisabled = false;
        }

        public async Task<bool> navigateTo(string dest)
        {
            string[] paths = { "profile", "security" };
            string url = NavigationManager.Uri;
            int length = NavigationManager.Uri.Split('/').Length;
            string addon = NavigationManager.Uri.Split('/')[length - 1];
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
