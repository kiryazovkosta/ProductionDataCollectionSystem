﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CollectingProductionDataSystem.Data.Contracts;
using CollectingProductionDataSystem.Models.Nomenclatures;
using CollectingProductionDataSystem.Web.Areas.NomManagement.Models.ViewModels;

namespace CollectingProductionDataSystem.Web.Areas.NomManagement.Controllers
{
    public class TransportTypeController : GenericNomController<TransportType, TransportTypeViewModel>
    {
        public TransportTypeController(IProductionData dataParam)
            : base(dataParam)
        {
        }
    }
}