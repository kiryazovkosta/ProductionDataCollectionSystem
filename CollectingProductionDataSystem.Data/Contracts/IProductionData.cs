﻿namespace CollectingProductionDataSystem.Data
{
    using System;

    using System.Data.Entity;
    using CollectingProductionDataSystem.Data.Contracts;
    using CollectingProductionDataSystem.Models;
    using CollectingProductionDataSystem.Contracts;
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

        IDeletableEntityRepository<UnitConfig> Units { get; }

        IImmutableEntityRepository<UnitsData> UnitsData { get; }

        IImmutableEntityRepository<UnitsInspectionData> UnitsInspectionData { get; }

        IDeletableEntityRepository<Area> Areas { get; }

        IDeletableEntityRepository<Park> Parks { get; }

        IDeletableEntityRepository<TankConfig> Tanks { get; }

        IImmutableEntityRepository<TankData> TanksData { get; }

        IDeletableEntityRepository<Product> Products { get; }

        IDeletableEntityRepository<ProductType> ProductTypes { get; }

        IDeletableEntityRepository<Ikunk> Ikunks { get; }

        IDeletableEntityRepository<Zone> Zones { get; }

        IDeletableEntityRepository<MeasurementPoint> MeasurementPoints { get; }

        IDeletableEntityRepository<MeasurementPointsProductsConfig> MeasurementPointsProductConfigs { get; }

        IDeletableEntityRepository<TransportType> TransportTypes { get; }


        IDbContext DbContext { get; }

        int SaveChanges(string userName);
    }
}
