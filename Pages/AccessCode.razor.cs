using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class AccessCode
    {
        private string accessCode;
        private string secretKey;
        private void enterGame()
        {
            if (DBTest.hasAccess(accessCode, urlID))
            {
                Console.WriteLine("Success");
                secretKey = DBTest.login(accessCode);
                navigateToMain(secretKey);
            }
            else
            {
                Console.WriteLine("failed");
            }
        }

        private async Task<bool> navigateToMain(string addOn)
        {
            NavigationManager.NavigateTo("/map/" + addOn);
            return await Task.FromResult(true);
        }

        private async Task<bool> gamePage()
        {
            NavigationManager.NavigateTo("/");
            return await Task.FromResult(true);
        }
    }
}
