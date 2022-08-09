using System;
using System.Net;
using System.Net.Mail;
using UnityEngine;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Net.Security;

public class AlternateSendEmail : MonoBehaviour
{
    public static AlternateSendEmail alterMail;
    private void Start() // Pretend to be a Singleton
    {
        DontDestroyOnLoad(gameObject);
        if (alterMail == null)
        {
            alterMail = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int CreateCode()
    {
        byte[] verificationCode = new byte[5];
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(verificationCode);
        return BitConverter.ToUInt16(verificationCode, 0);
    }
    public async Task<bool> SendEmail(string recipient, string body)
    {
        await Task.Delay(0);
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("noreply@hol.com");
        mail.To.Add(new MailAddress(recipient));
        mail.Subject = "Verification Code";
        mail.Body = body;

        SmtpServer.Credentials = new NetworkCredential("holgames44@gmail.com", "knloyhhxnhyomeyl") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        try
        {
            print("Sending Email");
            SmtpServer.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("DAMN IT!");
            Debug.LogError("Error message: " + ex.Message);
            return false;

        }

    }

}
