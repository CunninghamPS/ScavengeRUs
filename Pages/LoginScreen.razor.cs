﻿using ScavengeRUs.Data;

namespace ScavengeRUs.Pages
{
    public partial class LoginScreen
    {
        private Models.Account user;
        private string feedback = "";
        private string inputStyle = "";
        private string secretKey;

        protected override Task OnInitializedAsync()
        {
            user = new Models.Account();
            return base.OnInitializedAsync();
        }
        private async Task<bool> ValidateUser()
        {
            user.Email = user.Email.ToLower();

            Console.WriteLine(user.Email);
            if (DBTest.validateUser(user.Email, user.Password))
            {
                secretKey = DBTest.login(user.Email, user.Password);
                await navigateToMain(secretKey);
            }
            else
            {
                feedback = "Wrong Username or Password";
                inputStyle = "border: .5px solid red";
            }

            return await Task.FromResult(true);
        }

        private async Task<bool> navigateToMain(string addOn)
        {
            NavigationManager.NavigateTo("/map/" + addOn);
            return await Task.FromResult(true);
        }

        private async Task<bool> SignUp()
        {
            NavigationManager.NavigateTo("/signUpScreen");
            return await Task.FromResult(true);
        }

        private void ClearFormat()
        {
            inputStyle = "";
        }

    }
}
