using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configurations
{
    public class PatientConfiguration: IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(40);
            builder.Property(x => x.Gender).IsRequired().HasColumnType("char").HasMaxLength(1);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedById).IsRequired(false);
            builder.Property(x => x.UpdatedById).IsRequired(false);

            builder.HasOne(x => x.CreatedBy).WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UpdatedBy).WithMany()
                .HasForeignKey(x => x.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
