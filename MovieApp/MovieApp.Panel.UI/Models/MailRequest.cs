using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class MailRequest
    {
        [Required(ErrorMessage ="Ad soyad alanı boş geçilemez")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mail alanı boş geçilemez")]
        public string SenderMail { get; set; }
        public string? RecieverMail { get; set; }
        [Required(ErrorMessage = "Konu alanı boş geçilemez")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Mesaj alanı boş geçilemez")]
        public string Body { get; set; }

    }
}
