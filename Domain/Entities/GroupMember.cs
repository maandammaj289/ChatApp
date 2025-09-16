
namespace ChatApp.Domain.Entities
{
    public class GroupMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid GroupId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? Role { get; set; }
        public ChatGroup? Group { get; set; }
    }
}