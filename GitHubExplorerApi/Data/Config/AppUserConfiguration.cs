using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitHubExplorerApi.Data.Config
{
    public class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
        {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                builder.Property(p => p.Id).IsRequired();
                builder.Property(p => p.PasswordSalt).IsRequired();
                 builder.Property(p => p.PasswordHash).IsRequired();
                builder.Property(p => p.DisplayName).IsRequired();

                builder.HasMany(b => b.GitHubRepositories).WithOne().OnDelete(DeleteBehavior.Cascade);
                
            }
        }
    }

