using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Data.Configurations
{
    public class MedicalRecordConfiguration: IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();

            builder.HasOne(x => x.Patient).WithOne(x => x.MedicalRecord)
                .HasForeignKey<MedicalRecord>(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
