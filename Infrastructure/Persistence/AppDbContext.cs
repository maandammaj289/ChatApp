using ChatApp.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ChatApp.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Message> Messages => Set<Message>();
        public DbSet<ChatGroup> Groups => Set<ChatGroup>();
        public DbSet<GroupMember> GroupMembers => Set<GroupMember>();
        public DbSet<DeviceToken> DeviceTokens => Set<DeviceToken>();
        public DbSet<OutboxMessage> Outbox => Set<OutboxMessage>();
        //public DbSet<User> Users => Set<User>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // مهم! يجب استدعاءه لتكوين جداول Identity

            modelBuilder.Entity<GroupMember>()
            .HasOne(m => m.Group)
            .WithMany(g => g.Members)
            .HasForeignKey(m => m.GroupId);


            modelBuilder.Entity<Message>()
            .HasIndex(m => new { m.SenderId, m.ReceiverUserId, m.ReceiverGroupId, m.SentAt });
        }
    }
}