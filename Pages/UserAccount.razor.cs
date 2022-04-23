using ScavengeRUs.Models;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class UserAccount
    {
        private string inputStyle = "";
        private Account Account = new Account();
        private bool IsVisible { get; set; } = false;
        private bool SubmitBtnDisabled { get; set; } = true;
        private string ErrorMessage { get; set; } = "";
        private void HandleValidSubmit()
        {
            try
            {
                DBTest.updateUser(secretKey, Account.FirstName, Account.LastName, Account.UserName, Account.Email, Account.PhoneNumber);
            }
            catch (Exception)
            {
                inputStyle = "border: .5px solid red";
            }
            Console.WriteLine("Valid Submit");
        }

        protected override System.Threading.Tasks.Task OnInitializedAsync()
        {
            List<string> values = DBTest.getUserInfo(secretKey);
            Account.Email = values[1];
            Account.DOB = values[2];
            Account.FirstName = values[3];
            Account.LastName = values[4];
            Account.PhoneNumber = values[5];
            Account.UserName = values[6];
            return base.OnInitializedAsync();
        }

       

        private void ClearFormat()
        {
            inputStyle = "";
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
