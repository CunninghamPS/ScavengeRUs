using ScavengeRUs.Models;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class UserAccount
    {
        private Account Account = new Account();
        private string inputStyle = "";
        private void HandleValidSubmit()
        {

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
