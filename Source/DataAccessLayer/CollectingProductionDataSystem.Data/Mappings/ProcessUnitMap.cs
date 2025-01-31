using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CollectingProductionDataSystem.Models.Productions;

namespace CollectingProductionDataSystem.Data.Mappings
{
    public class ProcessUnitMap : EntityTypeConfiguration<ProcessUnit>
    {
        public ProcessUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(50);         

            // Table & Column Mappings
            this.Ignore(t => t.SortableName);
            this.ToTable("ProcessUnits");     

            // Relationships
            this.HasRequired(t => t.Factory)
                .WithMany(t => t.ProcessUnits)
                .HasForeignKey(d => d.FactoryId);

            this.HasMany(t => t.ApplicationUserProcessUnits)
               .WithRequired()
               .HasForeignKey(x => x.ProcessUnitId).WillCascadeOnDelete(false);
        }
    }
}
