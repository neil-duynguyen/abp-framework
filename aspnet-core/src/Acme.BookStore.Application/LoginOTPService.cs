using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Acme.BookStore
{
    [RemoteService(IsEnabled = false)]
    public class LoginOTPService : ApplicationService
    {
        private readonly IRepository<OtpRecord, Guid> _otpRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public LoginOTPService(IRepository<OtpRecord, Guid> otpRepository, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _otpRepository = otpRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public string GenerateOtpCode() {
            int otp = new Random().Next(0, 9);
            return otp.ToString("D6");
        }

        public async Task SaveOtpToDatabaseAsync(string email, string otpCode)
        {
            //check email is exit
            var isExit = await _userManager.FindByEmailAsync(email);
            Console.WriteLine("Mã OTP: " + otpCode);
            if (isExit == null) throw new Exception("Email không tồn tại trong hệ thống");

            var otpRecord = new OtpRecord
            {
                Email = email,
                OtpCode = otpCode,
                CreatedAt = DateTime.UtcNow,
                IsUsed = false
            };
            //await SendEmail(email, "Email xác nhận mã OTP", otpCode);
            await _otpRepository.InsertAsync(otpRecord);
        }

        public async Task<bool> VerifyOtpAsync(string email, string otpCode)
        {
            var otpRecord = _otpRepository.GetListAsync().Result
                .Where(x => x.Email == email && x.OtpCode == otpCode && !x.IsUsed)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();

            if (otpRecord == null)
            {
                return false;
            }

            // Kiểm tra xem OTP có hết hạn không
            if ((DateTime.UtcNow - otpRecord.CreatedAt).TotalMinutes > 5)
            {
                return false;
            }

            // Đánh dấu OTP là đã sử dụng
            otpRecord.IsUsed = true;
            await _otpRepository.UpdateAsync(otpRecord);

            return true;
        }

        public async Task SendEmail(string recipientEmail, string subject, string content)
        {
            string emailForSend = "netjprogram@gmail.com";
            string appPasswordConfiguration = "nruuhifutemjxgac";

            SmtpClient smtpClient = new SmtpClient
            {
                Port = 587,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailForSend, appPasswordConfiguration),
            };

            MailMessage message = new MailMessage()
            {
                Subject = subject,
                Body = content,
                From = new MailAddress(emailForSend),
            };

            //recipientEmail
            message.To.Add(new MailAddress("def@gmail.com"));

            await smtpClient.SendMailAsync(message);
        }

    }
}
