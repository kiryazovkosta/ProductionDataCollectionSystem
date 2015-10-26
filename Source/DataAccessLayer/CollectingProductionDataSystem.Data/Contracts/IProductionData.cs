﻿namespace CollectingProductionDataSystem.Data.Contracts
{
    using System;
    using CollectingProductionDataSystem.Models.Identity;
    using CollectingProductionDataSystem.Models.Inventories;
    using CollectingProductionDataSystem.Models.Nomenclatures;
    using CollectingProductionDataSystem.Models.Productions;
    using CollectingProductionDataSystem.Models.Transactions;

    public interface IProductionData : IDisposable
    {
        IDbContext Context { get; }

        IDeletableEntityRepository<Plant> Plants { get; }

        IDeletableEntityRepository<Factory> Factories { get; }

        IDeletableEntityRepository<ProcessUnit> ProcessUnits { get; }

        IDeletableEntityRepository<UnitConfig> UnitConfigs { get; }

        IApprovableEntityRepository<UnitsData> UnitsData { get; }

        IDeletableEntityRepository<UnitsManualData> UnitsManualData { get; }

        IApprovableEntityRepository<UnitsDailyData> UnitsDailyDatas { get; }

        IDeletableEntityRepository<UnitsManualDailyData> UnitsManualDailyDatas { get; }

        IDeletableEntityRepository<Area> Areas { get; }

        IDeletableEntityRepository<Park> Parks { get; }

        IDeletableEntityRepository<TankConfig> Tanks { get; }

        IDeletableEntityRepository<UnitDailyConfig> UnitsDailyConfigs { get; }

        IApprovableEntityRepository<TankData> TanksData { get; }

        IDeletableEntityRepository<MeasuringPointProductsData> MeasurementPointsProductsDatas { get; }

        IDeletableEntityRepository<Product> Products { get; }

        IDeletableEntityRepository<ProductType> ProductTypes { get; }

        IDeletableEntityRepository<ShiftProductType> ShiftProductTypes { get; }

        IDeletableEntityRepository<DailyProductType> DailyProductTypes { get; }

        IDeletableEntityRepository<Ikunk> Ikunks { get; }

        IDeletableEntityRepository<Zone> Zones { get; }

        IDeletableEntityRepository<TransportType> TransportTypes { get; }

        IDeletableEntityRepository<EditReason> EditReasons { get; }

        IDeletableEntityRepository<TankMasterProduct> TankMasterProducts { get; }

        IDeletableEntityRepository<UnitsApprovedData> UnitsApprovedDatas { get; }

        IDeletableEntityRepository<ApplicationUser> Users { get; }

        IDeletableEntityRepository<ApplicationRole> Roles { get; }

        IDeletableEntityRepository<UnitsApprovedDailyData> UnitsApprovedDailyDatas { get; }

        IDeletableEntityRepository<ProductionPlanConfig> ProductionPlanConfigs { get; }

        IDeletableEntityRepository<MeasuringPointConfig> MeasuringPointConfigs { get; }

        IDeletableEntityRepository<MeasuringPointsConfigsData> MeasuringPointsConfigsDatas { get; }

        IDeletableEntityRepository<MaxAsoMeasuringPointDataSequenceNumber> MaxAsoMeasuringPointDataSequenceNumberMap { get; }

        IDeletableEntityRepository<Direction> Directions { get; }

        IDeletableEntityRepository<MaterialType> MaterialTypes { get; }

        IDeletableEntityRepository<MeasureUnit> MeasureUnits { get; }

        IDeletableEntityRepository<ActiveTransactionsData> ActiveTransactionsDatas { get; }

        IRepository<UnitConfigUnitDailyConfig> UnitConfigUnitDailyConfigs { get; }

        IDeletableEntityRepository<Shift> Shifts { get; }

        IDbContext DbContext { get; }

        IEfStatus SaveChanges(string userName);
    }
}
