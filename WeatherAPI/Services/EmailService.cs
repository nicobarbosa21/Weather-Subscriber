using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _user;
    private readonly string _password;

    public EmailService(IConfiguration config)
    {
        _host = config["Smtp:Host"] ?? "";
        _port = int.Parse(config["Smtp:Port"] ?? "587");
        _user = config["Smtp:User"] ?? "";
        _password = config["Smtp:Password"] ?? "";
    }
    public void Send(string to, string subject, string body)
    {
        using var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_user, _password),
            EnableSsl = true
        };
        var mail = new MailMessage(_user, to, subject, body);
        client.Send(mail);
    }
}