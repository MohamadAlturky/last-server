using Infrastructure.Mail.Abstraction;
using Infrastructure.Mail.Configuration;
using Infrastructure.Mail.Model;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Infrastructure.Mail.Smtp;

namespace Infrastructure.Mail.HiastMail;
internal class HiastMailSender : IEmailSender
{
	private readonly MailAccount _serverAccount;
	private readonly SmtpServer _smtpServer;
	public HiastMailSender(IOptions<MailAccount> options, IOptions<SmtpServer> smtpServerOptions)
	{
		_serverAccount = options.Value;
		_smtpServer = smtpServerOptions.Value;
	}

	public async Task SendEmailAsync(EmailMessage emailMessage, MailAccount toEmail)
	{
		try
		{
			var fromAddress = new MailAddress(_serverAccount.Email);
			var toAddress = new MailAddress(toEmail.Email);
			string fromPassword = _serverAccount.Password;
			string subject = emailMessage.Subject;
			string body = emailMessage.Content;

			var smtpClient = new SmtpClient
			{
				Host = _smtpServer.URL,
				Port = 25,
				EnableSsl = false,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};

			var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = body
			};

			await smtpClient.SendMailAsync(message);
		}
		catch (Exception)
		{
			throw new Exception("smtpClient.SendMailAsync(message)");
		}

	}

}
