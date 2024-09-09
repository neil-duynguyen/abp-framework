using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class LoginOTPController : AbpController
    {
        private readonly LoginOTPService _loginOTPService;
        public LoginOTPController(LoginOTPService loginOTPService)
        {
            _loginOTPService = loginOTPService;
        }

        [HttpPost("CreateOTP")]
        public async Task<IActionResult> CreateOTP(string email)
        {
            try
            {
                string otp = _loginOTPService.GenerateOtpCode();
                await _loginOTPService.SaveOtpToDatabaseAsync(email, otp);
                return Ok("Tạo OTP thành công");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateOTP")]
        public async Task<IActionResult> UpdateOTP(string email, string otpCode)
        {
            bool isVaild = await _loginOTPService.VerifyOtpAsync(email, otpCode);
            if (isVaild)
            {
                return Ok("OTP đã được xác thực thành công.");
            }
            else {
                return BadRequest("OTP không hợp lệ hoặc đã hết hạn.");
            }
        }
    }
}
