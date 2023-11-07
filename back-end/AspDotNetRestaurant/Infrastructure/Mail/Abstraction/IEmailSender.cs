using Infrastructure.Mail.Configuration;
using Infrastructure.Mail.Model;

namespace Infrastructure.Mail.Abstraction;
public interface IEmailSender
{
	Task SendEmailAsync(EmailMessage email,MailAccount toEmail);
}
