namespace DevTracker.Core.Application.InboundPorts;
public interface ILoginService
{
    public abstract string GenerateToken(string username);
    public abstract bool ValidateLogin(string username, string password);
}
