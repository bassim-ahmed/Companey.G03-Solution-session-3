using Company.G03.DAL.Models;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace Company.G03.PL.Helper
{
	public  static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//mail server :gmail.com
			//smtp
			var clinet = new SmtpClient("smtp.gmail.com", 587);
			clinet.EnableSsl = true;
			clinet.Credentials = new NetworkCredential("bassimahmed20@gmail.com", "qlmbjofnwbpenblf");
			clinet.Send("bassimahmed20@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
