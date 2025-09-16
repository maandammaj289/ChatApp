

using Microsoft.AspNetCore.Identity;

namespace ChatApp.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        // بإمكانك تضيف خصائص إضافية هنا
        public string? DisplayName { get; set; }
    }
}
