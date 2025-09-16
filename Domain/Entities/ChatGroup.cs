namespace ChatApp.Domain.Entities
{
    public class ChatGroup
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<GroupMember> Members { get; set; } = new List<GroupMember>();
    }
}
