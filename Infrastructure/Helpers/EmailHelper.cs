using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static void SingleSendMailAsync(string email)
        {
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress("3177830400@qq.com");
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress(email));
            //邮件标题。
            mailMessage.Subject = "发送邮件测试";
            //邮件内容。
            mailMessage.Body = "这是一封测试邮件";
            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.qq.com";
            //使用安全加密连接。
            client.EnableSsl = false;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;
            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential("3177830400@qq.com", "");
            //发送
            client.Send(mailMessage);
        }
    }
}
