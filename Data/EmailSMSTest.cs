using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using Azure;
using Azure.Communication;
using Azure.Communication.Sms;
using ScavengeRUs.Data;

namespace ScavengeRUs.Data
{
    public static class EmailSMSTest
    {

        private static string connectionString = "endpoint=https://scavengerussms1.communication.azure.com/;accesskey=GPpHXMPXR92zsuWf7PiL3bXPDi3sawyALab5ubLSjSjw58Uhtzuqt8xk74UYvrMGvHmHumw4AsryBAmMCwWXPQ==";
        private static string PNUM = "+18443145032";

        public static void sendAll()
        {
            string[,] accountInfo = DBTest.getMessageInfo();

            for(int i = 0; i < accountInfo.GetLength(0); i ++)
            {
                //the requirements say to send the access code as a text and the url to the email
                //sendAccessCodeEmail(accountInfo[i, 0], accountInfo[i, 1]);
                sendAccessCodeText(accountInfo[i, 0], accountInfo[i, 2]);
                sendURLEmail(accountInfo[i, 1]);
                //sendURLText(accountInfo[i, 2]);
            }

        }

        public static void sendAccessCodeEmail(string accessCode, string email)
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
                Text = accessCode
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
                Text = "Thank you for Playing ScavengeRUs. The current hunt is accessed here " + URL + " and your access code has been sent to your phone.Good luck and have fun!"
            };

            SmtpClient client = new SmtpClient();

            //tries to connect to the email account and send the message
            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(emailAddress, password);
                client.Send(message);
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

        public static void sendAccessCodeText(string accessCode, string phoneNum)
        {
            SmsClient smsClient = new SmsClient(connectionString);

            SmsSendResult sendResult = smsClient.Send(
                from: PNUM,
                to: "+1" + phoneNum,
                message: "Thank you for playing ScavengeRUs: Your access code is " + accessCode
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
