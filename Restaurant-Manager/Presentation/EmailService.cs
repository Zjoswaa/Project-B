using System;
using System.Net;
using System.Net.Mail;

public static class EmailService
{
    public static void SendEmail(string recipientEmail)
    {
        // Define the email message
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("mail.escapedine@gmail.com");
        mail.To.Add(recipientEmail);
        mail.Subject = "Test Email";
        mail.Body = "Hello! This is a test email sent from the project";

        // Define the SMTP client
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587; // SMTP port for TLS
        smtpServer.Credentials = new NetworkCredential("mail.escapedine@gmail.com", "czzw ygug utxw fpwv");
        smtpServer.EnableSsl = true; // Enable SSL for secure connection

        try
        {
            // Send the email
            smtpServer.Send(mail);
            Console.WriteLine("Email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email. Error: " + ex.Message);
        }
    }
}