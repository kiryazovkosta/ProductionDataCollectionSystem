using System;
using System.Collections.Generic;

namespace SteelDbModel.Models
{
    public partial class TankConfig
    {
        public TankConfig()
        {
            this.TankDatas = new List<TankData>();
        }

        public int Id { get; set; }
        public int ParkId { get; set; }
        public string ControlPoint { get; set; }
        public string TankName { get; set; }
        public string PhdTagProductId { get; set; }
        public string PhdTagProductName { get; set; }
        public string PhdTagLiquidLevel { get; set; }
        public decimal LiquidLevelLowExtreme { get; set; }
        public decimal LiquidLevelHighExtreme { get; set; }
        public string PhdTagProductLevel { get; set; }
        public decimal ProductLevelLowExtreme { get; set; }
        public decimal ProductLevelHighExtreme { get; set; }
        public string PhdTagFreeWaterLevel { get; set; }
        public decimal FreeWaterLevelLowExtreme { get; set; }
        public decimal FreeWaterLevelHighExtreme { get; set; }
        public string PhdTagFreeWaterVolume { get; set; }
        public decimal FreeWaterVolumeLowExtreme { get; set; }
        public decimal FreeWaterVolumeHighExtreme { get; set; }
        public string PhdTagObservableDensity { get; set; }
        public decimal ObservableDensityLowExtreme { get; set; }
        public decimal ObservableDensityHighExtreme { get; set; }
        public string PhdTagReferenceDensity { get; set; }
        public decimal ReferenceDensityLowExtreme { get; set; }
        public decimal ReferenceDensityHighExtreme { get; set; }
        public string PhdTagGrossObservableVolume { get; set; }
        public decimal GrossObservableVolumeLowExtreme { get; set; }
        public decimal GrossObservableVolumeHighExtreme { get; set; }
        public string PhdTagGrossStandardVolume { get; set; }
        public decimal GrossStandardVolumeLowExtreme { get; set; }
        public decimal GrossStandardVolumeHighExtreme { get; set; }
        public string PhdTagNetStandardVolume { get; set; }
        public decimal NetStandardVolumeLowExtreme { get; set; }
        public decimal NetStandardVolumeHighExtreme { get; set; }
        public string PhdTagWeightInAir { get; set; }
        public decimal WeightInAirLowExtreme { get; set; }
        public decimal WeightInAirHighExtreme { get; set; }
        public string PhdTagAverageTemperature { get; set; }
        public decimal AverageTemperatureLowExtreme { get; set; }
        public decimal AverageTemperatureHighExtreme { get; set; }
        public string PhdTagTotalObservableVolume { get; set; }
        public decimal TotalObservableVolumeLowExtreme { get; set; }
        public decimal TotalObservableVolumeHighExtreme { get; set; }
        public string PhdTagWeightInVacuum { get; set; }
        public decimal WeightInVacuumLowExtreme { get; set; }
        public decimal WeightInVacuumHighExtreme { get; set; }
        public string PhdTagMaxVolume { get; set; }
        public decimal MaxVolumeLowExtreme { get; set; }
        public decimal MaxVolumeHighExtreme { get; set; }
        public string PhdTagAvailableRoom { get; set; }
        public decimal AvailableRoomLowExtreme { get; set; }
        public decimal AvailableRoomHighExtreme { get; set; }
        public decimal UnusableResidueLevel { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string DeletedFrom { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CreatedFrom { get; set; }
        public string ModifiedFrom { get; set; }
        public virtual Park Park { get; set; }
        public virtual ICollection<TankData> TankDatas { get; set; }
    }
}
