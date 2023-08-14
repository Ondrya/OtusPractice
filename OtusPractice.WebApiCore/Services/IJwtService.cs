namespace OtusPractice.WebApiCore.Services
{
    public interface IJwtService
    {
        string GenerateSecurityToken(string login);
    }
}