namespace Application.ExecutorProvider;
public interface IExecutorIdentityProvider
{
	string GetExecutorSerialNumber();
	string GetExecutorId();
	string GetMacAddress();
}
