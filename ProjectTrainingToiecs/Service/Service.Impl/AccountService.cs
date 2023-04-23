using ProjectTrainingToiecs.Service.Service.Model;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
namespace ProjectTrainingToiecs.Service.Service.Impl
{
    public class AccountService : IAccountService
    {
        public string CheckLogin(AccountModel model)
        {
            return "";
        }
        public string DeCodePassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public string SendEmailDigitNumber(AccountModel model)
        {
            var code = "";
            try
            {
                //atoj mã xác thực
                Random generator = new Random();
                code = generator.Next(0, 1000000).ToString("D6");

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("vuductinh97@gmail.com", "nrqpiuogvgdgxtdk"),
                    EnableSsl = true,
                    UseDefaultCredentials = true
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("vuductinh97@gmail.com"),
                    Subject = "Mã xác thực tạo thành công tài khoản khóa học",
                    Body = String.Format("<h1>Vui lòng điền mã xác thực này {0} để hoàn thành đăng ký</h1>" +
                    "<p style= 'color : red'>Mã có hiệu lực trong 1 phút(lưu ý không chia sẻ mã này với ai)</p>",code),
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(model.Email);
                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {
                code = "";
            }
            return code;
        }
    }
}
