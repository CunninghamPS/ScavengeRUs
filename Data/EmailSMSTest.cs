using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using Azure;
using Azure.Communication;
using Azure.Communication.Sms;

namespace ScavengeRUs.Data
{
    public static class EmailSMSTest
    {

        private static string connectionString = "endpoint=https://scavengerussms1.communication.azure.com/;accesskey=GPpHXMPXR92zsuWf7PiL3bXPDi3sawyALab5ubLSjSjw58Uhtzuqt8xk74UYvrMGvHmHumw4AsryBAmMCwWXPQ==";
        private static string PNUM = "+18443145032";

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
            SmsClient smsClient = new SmsClient(connectionString);

            SmsSendResult sendResult = smsClient.Send(
                from: PNUM,
                to: "+1" + phoneNum,
                message: accessCode.ToString()
            );
        }

        public static void sendURLText(string phoneNum)
        {
            string URL = "https://scavengerus1.azurewebsites.net/loginScreen";

            SmsClient smsClient = new SmsClient(connectionString);

            SmsSendResult sendResult = smsClient.Send(
                from: PNUM,
                to: "+1" + phoneNum,
                message: URL
            );
        }




    }
}
