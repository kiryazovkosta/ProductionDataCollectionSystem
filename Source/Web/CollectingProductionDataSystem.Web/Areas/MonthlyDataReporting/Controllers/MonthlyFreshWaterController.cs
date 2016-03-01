﻿namespace CollectingProductionDataSystem.Web.Areas.MonthlyDataReporting.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using CollectingProductionDataSystem.Application.Contracts;
    using CollectingProductionDataSystem.Constants;
    using CollectingProductionDataSystem.Data.Contracts;
    using CollectingProductionDataSystem.Web.Areas.MonthlyDataReporting.Models;
    using Resources = App_GlobalResources.Resources;

    [Authorize(Roles = "Administrator, MonthlyFreshWaterReporter")]
    public class MonthlyFreshWaterController : GenericMonthlyController<MonthlyEnergyViewModel>
    {
        private const string defaultView = "EnergyReport";

        public MonthlyFreshWaterController(IProductionData dataParam, IUnitMothlyDataService monthlyServiceParam)
            : base(dataParam, monthlyServiceParam, CommonConstants.CirculatingWater, defaultView,
                new MonthlyEnergyReportsViewModel(
                    reportName: Resources.Layout.UnitMonthlyFWData,
                    controllerName: "MonthlyFreshWater",
                    monthlyReportTypeId: CommonConstants.FreshWate))
        {
        }
    }
}