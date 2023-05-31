using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.Configurations;

public class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> leaveType)
    {
        leaveType.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        );

        leaveType.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}