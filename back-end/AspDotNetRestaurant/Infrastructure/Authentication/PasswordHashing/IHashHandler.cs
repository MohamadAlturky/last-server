namespace Infrastructure.Authentication.PasswordHashing;
public interface IHashHandler
{
	string GetHash(string text);
}
