using ScavengeRUs.Models;
using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class UserAccount
    {
        private string inputStyle = "";
        private Models.Account user;
        private void HandleValidSubmit()
        {

        }

        protected override System.Threading.Tasks.Task OnInitializedAsync()
        {
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
