﻿namespace CollectingProductionDataSystem.Models.Nomenclatures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CollectingProductionDataSystem.Models.Abstract;
    using CollectingProductionDataSystem.Models.Contracts;
    using CollectingProductionDataSystem.Models.Productions;
    using CollectingProductionDataSystem.Models.Productions.Mounthly;

    public partial class DailyProductType : DeletableEntity, IEntity
    {
        public DailyProductType()
        {
            this.UnitsDailyConfigs = new HashSet<UnitDailyConfig>();
            this.UnitsMonthlyConfigs = new HashSet<UnitMonthlyConfig>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UnitDailyConfig> UnitsDailyConfigs { get; set; }
        public virtual ICollection<UnitMonthlyConfig> UnitsMonthlyConfigs { get; set; }
    }

}
