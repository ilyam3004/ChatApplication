using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Configurations;

public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.ChatId);

        builder.Property(c => c.ChatOwnerId)
            .IsRequired();

        builder.Property(c => c.ChatName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId);
    }
}