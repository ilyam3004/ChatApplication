using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.ChatId);

        builder.Property(c => c.ChatName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(c => c.Users)
            .WithOne(u => u.Chat)
            .HasForeignKey(u => u.ChatId);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId);
    }
}