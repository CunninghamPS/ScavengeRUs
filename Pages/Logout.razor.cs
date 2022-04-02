using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class Logout
    {
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
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

    }
}
