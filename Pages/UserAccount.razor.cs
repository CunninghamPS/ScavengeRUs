using ScavengeRUs.Models;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class UserAccount
    {
        private string inputStyle = "";
        private Account Account = new Account();
        private void HandleValidSubmit()
        {

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

        private bool IsVisible { get; set; } = false;
        private bool SubmitBtnDisabled { get; set; } = true;
        private string ErrorMessage { get; set; } = "";

        private void ClearFormat()
        {
            inputStyle = "";
        }

    }
}
