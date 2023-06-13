using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(string name, string email, string subject, string message)
        {
            // Create a new MimeMessage
            var emailMessage = new MimeMessage();

            // Set the From address
            emailMessage.From.Add(new MailboxAddress("Sender", email));

            // Set the To address
            emailMessage.To.Add(new MailboxAddress("Recipient", "boufaiedmortadha7@gmail.com"));

            // Set the Subject
            emailMessage.Subject = subject;

            // Set the Body
            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Name: {name}\nEmail: {email}\n\n{message}"
            };

            using (var client = new SmtpClient())
            {
                // Connect to the SMTP server
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Authenticate with the SMTP server
                client.Authenticate("boufaiedmortadha7@gmail.com", "qhnbcpctlufompqg");

                // Send the email
                client.Send(emailMessage);

                // Disconnect from the SMTP server
                client.Disconnect(true);
            }

            return RedirectToAction("Contact");
        }
    }
    public class ContactFormModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
