﻿namespace CollectingProductionDataSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using CollectingProductionDataSystem.Application.UnitsDataServices;
    using CollectingProductionDataSystem.Data.Contracts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using AutoMapper;
    using System.Collections.Generic;
    using CollectingProductionDataSystem.Models.Productions;
    using CollectingProductionDataSystem.Web.ViewModels.Units;

    public class UnitsController : BaseController
    {
        private readonly IUnitsDataService unitsData;

        public UnitsController(IProductionData dataParam, IUnitsDataService unitsDataParam)
            : base(dataParam)
        {
            this.unitsData = unitsDataParam;
        }

        [HttpGet]
        public ActionResult UnitsData()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ReadUnitsData([DataSourceRequest]DataSourceRequest request, DateTime? date, int? processUnit)
        {
            var dbResult = this.unitsData.GetUnitsDataForDateTime(date, processUnit);
            var kendoResult = dbResult.ToDataSourceResult(request, ModelState);
            kendoResult.Data = Mapper.Map<IEnumerable<UnitsData>, IEnumerable<UnitDataViewModel>>((IEnumerable<UnitsData>)kendoResult.Data);
            return Json(kendoResult);
        }
    }
}