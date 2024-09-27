using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configurations
{
    public class DoctorConfiguration: IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(40);
            builder.Property(x => x.Gender).IsRequired().HasColumnType("char").HasMaxLength(1);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.SpecialtyId).IsRequired();

            builder.HasOne(x => x.Specialty).WithMany()
                .HasForeignKey(x => x.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
