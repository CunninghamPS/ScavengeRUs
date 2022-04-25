using ScavengeRUs.Models;
using ScavengeRUs.Data;
using Syncfusion.Blazor.Popups;

namespace ScavengeRUs.Pages
{
    public partial class SignUpScreen
    {
        private Account Account = new Account();
        private string inputStyle = "";
        private bool password = false;

        private void HandleValidSubmit()
        {
            Random random = new Random();
            Account.UserName = random.Next(111111,999999).ToString();
            try
            {   
                DBTest.newAccount(Account.Email, Account.DOB, Account.FirstName, Account.LastName, Account.PhoneNumber, Account.UserName, Account.Password);
                Navigate();
            }
            catch (Exception)
            {
                inputStyle = "border: .5px solid red";
            }
            Console.WriteLine("Valid Submit");
        }
        void Navigate()
        {
            UriHelper.NavigateTo("/loginScreen");

        }
        void NavigateTermsNCond()
        {
            OpenDialog();
        }

        private bool IsVisible { get; set; } = false;
        private bool SubmitBtnDisabled { get; set; } = true;
        private string ErrorMessage { get; set; } = "";
        private ResizeDirection[] dialogResizeDirections { get; set; } = new ResizeDirection[] { ResizeDirection.All };

        private void OpenDialog()
        {
            this.IsVisible = true;
        }

        private void OnSubmit()
        {
            this.IsVisible = false;
            SubmitBtnDisabled = false;
        }

        private void ClearFormat()
        {
            inputStyle = "";
        }

    }
}
