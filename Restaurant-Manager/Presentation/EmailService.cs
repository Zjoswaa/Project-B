using System;
using System.Net;
using System.Net.Mail;

public static class EmailService
{
    public static void SendReservationEmail(string RecipientName, string LocationName, string Date, string Time, int PlayerAmount, string RecipientEmail)
    {
        // Define the email message
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("mail.escapedine@gmail.com");
        mail.To.Add(RecipientEmail);
        mail.Subject = "Your Escape &amp; Dine reservation";
        mail.Body = $"<p><strong>Dear {RecipientName},</strong></p><p>Thank you for booking your adventure at Escape &amp; Dine! We&rsquo;re excited to confirm your reservation and look forward to your visit.</p><hr/><p><strong>Reservation Details:</strong></p><ul><li><strong>Name</strong>: {RecipientName}</li><li><strong>Location</strong>: {LocationName}</li><li><strong>Date</strong>: {Date}</li><li><strong>Time</strong>: {Time}</li><li><strong>Guests</strong>: {PlayerAmount}</li><li><strong>Duration</strong>: Approx. 3 Hours</li></ul><hr/><p><strong>After the Game:</strong></p><p>Once you've conquered the challenge, you're invited to relax and celebrate with a meal at our restaurant! If there are no wishes to eat at our restaurant, you are free to leave after completing the escape room.</p><hr/><p>If you need to modify your reservation, feel free to use the reservation manager you used to make this reservation.</p><p>We look forward to seeing you soon for an exciting and unforgettable experience!</p><p></p><p></p><p>Best regards,<br /><br /><strong>Team Escape &amp; Dine</strong><br />Hogeschool Rotterdam | Wijnhaven 99<br/>mail.escapedine@gmail.com</p>";
        mail.IsBodyHtml = true;

        // Define the SMTP client
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Port = 587; // SMTP port for TLS
        smtpClient.Credentials = new NetworkCredential("mail.escapedine@gmail.com", "czzw ygug utxw fpwv");
        smtpClient.EnableSsl = true; // Enable SSL for secure connection

        try
        {
            // Send the email
            smtpClient.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send email. Error: " + ex.Message);
        }
    }
}
