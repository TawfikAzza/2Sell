using System.Net;
using System.Net.Mail;

namespace PrintAsPdf;

class mail_core
{
    //Mail components
    static MailAddress sFrom;
    static MailAddress sTo;
    static MailMessage newEmail;

    //smtpServer
    SmtpClient SMTPServer;
    static string smtpServerAddress;
    static int smtpServerPort;
    static NetworkCredential cred;
    static bool ssl;


    public mail_core() { }


    public void NewMail(string recieverEmail, string recieverName, string senderEmail, string senderName, string subject, string message, string attachementFile)
    {
 
        sTo = new MailAddress(recieverEmail, recieverName);
        sFrom = new MailAddress(senderEmail, senderName);
        newEmail = new MailMessage(sFrom, sTo) 
        {
            Subject = subject,
            Body = message  
        };                
        if (!attachementFile.Equals("")) newEmail.Attachments.Add(new Attachment(attachementFile));
    }


    public void smtpServerSettings(string server, int port, string EmailUsername, string EmailPassword, bool sslEnable)
    {

        smtpServerAddress = server;
        smtpServerPort = port;
        cred = new NetworkCredential(EmailUsername, EmailPassword);
        ssl = sslEnable;
    }

    public void addAttachment(string filename)
    {

        newEmail.Attachments.Add(new Attachment(filename));
    }


    public bool sendMail()
    {
        try
        {
            SMTPServer = new SmtpClient(smtpServerAddress);
            SMTPServer.Port = smtpServerPort;
            SMTPServer.UseDefaultCredentials = false;
            SMTPServer.Credentials = cred;
            SMTPServer.EnableSsl = ssl;
            SMTPServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SMTPServer.Send(newEmail);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
            return false;
        }
    }

}