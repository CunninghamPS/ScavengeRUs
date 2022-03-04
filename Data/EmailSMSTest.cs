using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ScavengeRUs.Data
{
    public static class EmailSMSTest
    {
        public static void sendAccessCodeEmail(int accessCode, string email)
        {

            MailboxAddress sender = new MailboxAddress("ScavengeRUs Team", "jj.cat98@gmail.com");

            string emailAddress = "game.scavengerus@gmail.com";
            string password = "ScavengeRUs2022Tier1";

            //creates a new message object
            MimeMessage message = new MimeMessage();
            //adds an account to send the message from
            message.From.Add(sender);
            //adds a recipient to the message
            message.To.Add(MailboxAddress.Parse(email));
            //adds a subject to the message
            message.Subject = "ScavengeRUs Access Code";
            //adds a message body in plain text
            message.Body = new TextPart("plain")
            {
                Text = accessCode.ToString()
            };

            SmtpClient client = new SmtpClient();

            //tries to connect to the email account and send the message
            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(emailAddress, password);
                client.Send(message);

                Console.WriteLine("Email Sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);

                client.Dispose();
            }

        }

        public static void sendURLEmail(string email)
        {
            string URL = "https://scavengerus1.azurewebsites.net/loginScreen";

            MailboxAddress sender = new MailboxAddress("ScavengeRUs Team", "game.scavengerus@gmail.com");

            string emailAddress = "game.scavengerus@gmail.com";
            string password = "ScavengeRUs2022Tier1";

            //creates a new message object
            MimeMessage message = new MimeMessage();
            //adds an account to send the message from
            message.From.Add(sender);
            //adds a recipient to the message
            message.To.Add(MailboxAddress.Parse(email));
            //adds a subject to the message
            message.Subject = "ScavengeRUs Access Code";
            //adds a message body in plain text
            message.Body = new TextPart("plain")
            {
                Text = URL
            };

            SmtpClient client = new SmtpClient();

            //tries to connect to the email account and send the message
            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(emailAddress, password);
                client.Send(message);

                Console.WriteLine("Email Sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);

                client.Dispose();
            }

        }

        public static void sendAccessCodeText(int accessCode, string phoneNum)
        {

            TwilioClient.Init("AC993c29d22fb0ea6b44222ff0bbf3bf0a", "f0ea83384d05ac3c9192d91c2955dcb5");

            MessageResource.Create(
                to: new PhoneNumber("+1" + phoneNum),
                from: new PhoneNumber("+19107089762"),
                body: accessCode.ToString());
        }

        public static void sendURLText(string phoneNum)
        {
            string URL = "https://scavengerus1.azurewebsites.net/loginScreen";

            TwilioClient.Init("AC993c29d22fb0ea6b44222ff0bbf3bf0a", "f0ea83384d05ac3c9192d91c2955dcb5");

            MessageResource.Create(
                to: new PhoneNumber("+1" + phoneNum),
                from: new PhoneNumber("+19107089762"),
                body: URL);
        }




    }
}
