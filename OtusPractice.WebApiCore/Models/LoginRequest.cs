using OtusPractice.Domain.Models;
using System;

namespace OtusPractice.WebApiCore.Models
{
    public class LoginRequest
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }

        public LoginRequest()
        {
            
        }

        public LoginRequest(string userLogin, string userPassword)
        {
            UserLogin = userLogin;
            UserPassword = userPassword;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.UserLogin)) return false;
            if (string.IsNullOrWhiteSpace(this.UserPassword)) return false;
            return true;
        }


        public bool IsChecked(User user)
        {
            return user.Login == this.UserLogin && user.Password == this.UserPassword;
        }
    }
}
