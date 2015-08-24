namespace CollectingProductionDataSystem.Application.UnitsDataServices
{
    using System;
    using System.Linq;
    using CollectingProductionDataSystem.Models.Productions;

    public interface IUnitsDataService
    {
        IQueryable<UnitsData> GetUnitsDataForDateTime(DateTime? date, int? processUnit);
    }
}