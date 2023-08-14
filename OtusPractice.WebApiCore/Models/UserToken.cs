namespace OtusPractice.WebApiCore.Models
{
    public class UserToken
    {
        public string Token { get; set; }

        public UserToken()
        {
            
        }

        public UserToken(string token)
        {
            Token = token;
        }
    }
}
