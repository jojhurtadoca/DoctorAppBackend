using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configurations
{
    public class MedicalHistoryConfiguration: IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.MedicalRecordId).IsRequired();

            builder.HasOne(x => x.MedicalRecord).WithMany()
                .HasForeignKey(x => x.MedicalRecordId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
