using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class EmailSMSTesting
    {
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private void dbTest()
        {
            EmailSMSTest.sendAll();
        }

        private void loginTest()
        {
            DBTest.createGameURLs();
        }

    }
}
