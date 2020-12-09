using Jobsity.FinancialChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobsity.FinancialChat.Persistence.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();            

            builder.Property(m => m.UserName)
                .IsRequired();

            builder.Property(m => m.Body)
                .IsRequired();

            builder.Property(m => m.When)
                .IsRequired();
        }
    }
}
