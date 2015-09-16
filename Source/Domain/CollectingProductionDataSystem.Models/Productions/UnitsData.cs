namespace CollectingProductionDataSystem.Models.Productions
{
    using System;
    using CollectingProductionDataSystem.Models.Abstract;
    using CollectingProductionDataSystem.Models.Contracts;
    using CollectingProductionDataSystem.Models.Nomenclatures;

    public partial class UnitsData : AuditInfo, IApprovableEntity, IEntity
    {
        public int Id { get; set; }
        public DateTime RecordTimestamp { get; set; }
        public int UnitConfigId { get; set; }
        public decimal? Value { get; set; }
        public bool IsApproved { get; set; }
        public virtual UnitConfig UnitConfig { get; set; }
        public virtual UnitsManualData UnitsManualData { get; set; }

        public override string ToString()
        {
            if(this.UnitsManualData != null)
            {
                return this.UnitsManualData.Value.ToString();
            }

            if (this.Value.HasValue)
            {
               return this.Value.Value.ToString(); 
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
