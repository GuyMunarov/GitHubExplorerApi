using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitHubExplorerApi.Data.Config
{
    public class GitHubRepositoryConfiguration : IEntityTypeConfiguration<GitHubRepository>
    {
        public void Configure(EntityTypeBuilder<GitHubRepository> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.GitHubId).IsRequired();
            builder.Property(p => p.name).IsRequired();
            builder.HasOne(b => b.User).WithMany()
                .HasForeignKey(p => p.UserId);
        }
    }
}
