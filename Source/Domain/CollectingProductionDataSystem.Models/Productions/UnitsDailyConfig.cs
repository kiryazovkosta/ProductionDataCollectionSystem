﻿namespace CollectingProductionDataSystem.Models.Productions
{
    using CollectingProductionDataSystem.Models.Abstract;
    using CollectingProductionDataSystem.Models.Contracts;
    using CollectingProductionDataSystem.Models.Nomenclatures;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class UnitsDailyConfig : DeletableEntity, IEntity
    {
        private ICollection<UnitsDailyData> unitsDailyDatas;
        private ICollection<RelatedUnitDailyConfigs> relatedUnitDailyConfigs;

        public UnitsDailyConfig()
        {
            this.unitsDailyDatas = new HashSet<UnitsDailyData>();
            this.relatedUnitDailyConfigs = new HashSet<RelatedUnitDailyConfigs>();
            //this.UnitsConfigs = new HashSet<UnitConfig>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ProcessUnitId { get; set; }
        public int ProductId { get; set; }
        public int? DailyProductTypeId { get; set; }
        public int MeasureUnitId { get; set; }
        public string AggregationFormula { get; set; }
        public bool AggregationCurrentLevel { get; set; }
        public string AggregationMembers { get; set; }
        [DefaultValue(true)]
        public bool IsEditable { get; set; }
        public virtual MeasureUnit MeasureUnit { get; set; }
        public virtual ProcessUnit ProcessUnit { get; set; }
        public virtual Product Product { get; set; }
        public virtual DailyProductType DailyProductType { get; set; }
        public virtual ICollection<UnitsDailyData> UnitsDailyDatas 
        {
            get { return this.unitsDailyDatas; }
            set { this.unitsDailyDatas = value; }
        }
        public virtual ICollection<RelatedUnitDailyConfigs> RelatedUnitDailyConfigs 
        {
            get { return this.relatedUnitDailyConfigs; }
            set { this.relatedUnitDailyConfigs = value; }
        }
        
        //public virtual ICollection<UnitConfig> UnitsConfigs { get; set; }
    }
}
