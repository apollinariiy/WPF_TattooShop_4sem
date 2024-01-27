using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Services
{
    internal class EmailMessage
    {
            private static readonly string address;
            private static readonly SecureString securePassword;
            private static readonly string cfgHost;
            private static readonly int cfgPort;

            public static bool IsAvailable { get; }

            static EmailMessage()
            {
                try
                {
                    address = ConfigurationManager.AppSettings["SMTPAddress"];
                    securePassword = new SecureString();
                    foreach (var ch in ConfigurationManager.AppSettings["SMTPPassword"].ToCharArray())
                        securePassword.AppendChar(ch);
                    securePassword.MakeReadOnly();
                    cfgHost = ConfigurationManager.AppSettings["SMTPHost"];
                    cfgPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                    IsAvailable = true;
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.ToString());
                }
            }

            public static bool SendMail(string fromName, string toMail, string toName, string subject, string body)
            {
                if (!IsAvailable)
                    return false;

                try
                {
                    var from = new MailAddress(address, fromName);
                    var to = new MailAddress(toMail, toName);

                    using (var mailMessage = new MailMessage(from, to))
                    {
                        using (var smtp = new SmtpClient())
                        {
                            mailMessage.Subject = subject;
                            mailMessage.Body = body;

                            smtp.Host = cfgHost;
                            smtp.Port = cfgPort;
                            smtp.EnableSsl = true;
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.UseDefaultCredentials = false;

                            smtp.Credentials = new NetworkCredential(from.Address, securePassword);

                            smtp.Send(mailMessage);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                MessageBox.Show(ex.ToString());
                return false;
            }
            }
    }
}
