using PharmacyManagementSystem.Dtos.EmailDto;

namespace PharmacyManagementSystem.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto request);
    }
}
