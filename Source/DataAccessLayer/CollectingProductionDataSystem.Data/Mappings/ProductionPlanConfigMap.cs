﻿namespace CollectingProductionDataSystem.Data.Mappings
{
    using CollectingProductionDataSystem.Models.Productions;
    using System.Data.Entity.ModelConfiguration;

    public class ProductionPlanConfigMap : EntityTypeConfiguration<ProductionPlanConfig>
    {
        public ProductionPlanConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ProcessUnitId)
                .IsRequired();

            this.Property(t => t.Percentages)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("ProductionPlanConfigs");     

            // Relationships
            this.HasRequired(t => t.ProcessUnit)
                .WithMany(t => t.ProductionPlanConfigs)
                .HasForeignKey(d => d.ProcessUnitId);

            this.HasOptional(t => t.MeasureUnit)
                .WithMany(t => t.ProductionPlanConfigs)
                .HasForeignKey(x => x.MeasureUnitId);
        }
    }
}
