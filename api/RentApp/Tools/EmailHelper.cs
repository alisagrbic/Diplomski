using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace RentApp.Tools
{
    public class EmailHelper
    {
        public static void SendEmail(string username, string destAddr, EmailTemplate email)
        {
            string sourceAddr = "zorkavegait@gmail.com";
            string pass = "Pr4kt!k4nt";

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(destAddr));
                mail.From = new MailAddress(sourceAddr);
                mail.Subject = email.Subject;
                mail.Body = username + ", " + email.Message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Credentials = new System.Net.NetworkCredential(sourceAddr, pass),
                    Port = 25,
                    EnableSsl = true
                };
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in sendEmail:" + ex.Message);
            }
        }
    }
}