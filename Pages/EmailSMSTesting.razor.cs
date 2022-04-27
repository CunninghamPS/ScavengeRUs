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
            EmailSMSTest.sendAccessCodeText("test", "2766082298");
        }

        private void loginTest()
        {
            DBTest.createGameURLs();
        }

    }
}
