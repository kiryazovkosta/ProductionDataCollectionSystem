﻿using System.Data.Entity;
using CollectingProductionDataSystem.Application.FileServices;
using CollectingProductionDataSystem.Data;
using CollectingProductionDataSystem.Data.Concrete;
using CollectingProductionDataSystem.Data.Contracts;
using CollectingProductionDataSystem.Infrastructure.Log;
using CollectingProductionDataSystem.Models.Contracts;
using CollectingProductionDataSystem.Models.Productions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectingProductionDataSystem.Infrastructure.Extentions;
using Uniformance.PHD;
using CollectingProductionDataSystem.Models.Transactions;
using System.Data;

namespace CollectingProductionDataSystem.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var ninject = new NinjectConfig();
            //var timer = new Stopwatch();
            //timer.Start();
            var kernel = ninject.Kernel;

            var data = kernel.GetService(typeof(IProductionData)) as IProductionData;

            //var shiftData = data.UnitsData.All().Where(x => x.ShiftId == ShiftType.Second 
            //    && x.RecordTimestamp == new DateTime(2016, 2, 1, 0, 0, 0) 
            //    && x.UnitConfig.ProcessUnitId == 50).ToList();

            //int monthIndex = DateTime.Now.Month;
            //var lastDate = new DateTime(2016, 2, 16, 0, 0, 0);

            //var shiftData = data.UnitsData.All().Where(x => x.ShiftId == ShiftType.Second
            //    && x.RecordTimestamp == lastDate
            //    && x.UnitConfig.ProcessUnitId == 34).ToList();

            //var prevDay = lastDate.AddDays(-1);
            //while(prevDay.Month == monthIndex)
            //{
            //    foreach (var item in shiftData)
            //    {
            //        item.RecordTimestamp = prevDay;
            //        item.ShiftId = ShiftType.First;
            //        data.UnitsData.Add(item);
            //    }

            //    data.SaveChanges("Loader");
            //    prevDay = prevDay.AddDays(-1);
            //}

            //for (int i = 0; i < 19; i++)
            //{
            //    ProcessActiveTransactionsData(i);   
            //}

            //for (int i = 18; i >= 0; i--)
            //{
            //    ProcessScaleTransactionsData(i);   
            //}

            //ProcessTransactionsData();

            DoCalculation();

            System.Console.WriteLine("finished");
            //ConvertProductsForInternalPipes(data);

            //TransformUnitDailyConfigTable(data);
            //TransformUnitConfigTable(data);
            //var fileName = @"d:\Proba\ХО-2-Конфигурация инсталации.csv";
            //var fileUploader = kernel.GetService(typeof(IFileUploadService)) as IFileUploadService;
            //timer.Stop();
            //Console.WriteLine("Time for ninject init {0}.", timer.Elapsed);
            //timer.Reset();
            //timer.Start();
            //var result = fileUploader.UploadFileToDatabase(fileName, ";");
            //timer.Stop();
            //if (result.IsValid)
            //{
            //    Console.WriteLine("File was uploaded successfully!!!");
            //    Console.WriteLine("Estimated time for action {0}", timer.Elapsed);
            //}
            //else
            //{
            //    result.EfErrors.ForEach(x =>
            //        Console.WriteLine("{0} => {1}", x.MemberNames.FirstOrDefault(), x.ErrorMessage)
            //        );
            //}
            //TreeShiftsReports(DateTime.Today.AddDays(-2), 1);
            //SeedShiftsToDatabase(uow);
        }

        private static void ConvertProductsForInternalPipes(IProductionData data)
        {
            var products = data.Products.All();

            List<int> exclusionProductTypeList = new List<int> { 61, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75 };

            foreach (var product in products.Where(x => !exclusionProductTypeList.Contains(x.ProductTypeId)))
            {
                product.IsAvailableForInnerPipeLine = true;
            }

            data.SaveChanges("Initial Loading");

        }

        /// <summary>
        /// Transforms the unit config table.
        /// </summary>
        /// <param name="data">The data.</param>
        private static void TransformUnitConfigTable(IProductionData data)
        {
            var records = data.UnitConfigs.All().Where(x => x.IsConverted == false);
            TransformRecords(records, data);
        }

        private static void TransformUnitDailyConfigTable(IProductionData data)
        {
            var recordsDependOnUnitConfig = data.UnitsDailyConfigs.All().Where(x => x.IsConverted == false && x.AggregationCurrentLevel == false);
            var recordsDependOnUnitDailyConfig = data.UnitsDailyConfigs.All().Where(x => x.IsConverted == false && x.AggregationCurrentLevel == true);
            TransformRecordsDependOnUnitConfig(recordsDependOnUnitConfig, data);
            TransformRecordsDependOnUnitDailyConfig(recordsDependOnUnitDailyConfig, data);
        }

        /// <summary>
        /// Transforms the records depend on unit daily config.
        /// </summary>
        /// <param name="recordsDependOnUnitDailyConfig">The records depend on unit daily config.</param>
        private static void TransformRecordsDependOnUnitDailyConfig(IQueryable<UnitDailyConfig> recordsDependOnUnitDailyConfig, IProductionData data)
        {
            foreach (var record in recordsDependOnUnitDailyConfig)
            {
                var depedentRecords = FindDependantRecords<UnitDailyConfig>(record.AggregationMembers, data);
                if (depedentRecords.Count() > 0)
                {
                    record.RelatedUnitDailyConfigs.Clear();
                    record.RelatedUnitDailyConfigs.AddRange(depedentRecords.Select((x, ix) =>
                        new RelatedUnitDailyConfigs
                        {
                            UnitsDailyConfigId = record.Id,
                            RelatedUnitsDailyConfigId = x.Id,
                            Position = ix + 1
                        }).ToList());
                    record.IsConverted = true;
                    record.ProductId = record.ProductId == 0 ? 1 : record.ProductId;

                    data.UnitsDailyConfigs.Update(record);
                }
            }

            var result = data.SaveChanges("InitialLoading");
        }


        /// <summary>
        /// Transforms the records depend on unit config.
        /// </summary>
        /// <param name="recordsDependOnUnitConfig">The records depend on unit config.</param>
        private static void TransformRecordsDependOnUnitConfig(IQueryable<UnitDailyConfig> records, IProductionData data)
        {
            foreach (var record in records)
            {
                var depedentRecords = FindDependantRecords<UnitConfig>(record.AggregationMembers, data);
                if (depedentRecords.Count() > 0)
                {
                    record.UnitConfigUnitDailyConfigs.Clear();
                    record.UnitConfigUnitDailyConfigs.AddRange(depedentRecords.Select((x, ix) =>
                        new UnitConfigUnitDailyConfig
                        {
                            UnitDailyConfigId = record.Id,
                            UnitConfigId = x.Id,
                            Position = ix + 1
                        }).ToList());
                    record.IsConverted = true;
                    record.ProductId = record.ProductId == 0 ? 1 : record.ProductId;

                    data.UnitsDailyConfigs.Update(record);
                }
            }

            data.SaveChanges("InitialLoading");
        }

        /// <summary>
        /// Transforms the records.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <param name="data">The data.</param>
        private static void TransformRecords(IQueryable<UnitConfig> records, IProductionData data)
        {
            foreach (var record in records)
            {
                var depedentRecords = FindDependantRecords<UnitConfig>(record.AggregationMembers, data);
                if (depedentRecords.Count() > 0)
                {
                    record.UnitConfigUnitDailyConfigs.Clear();
                    record.UnitConfigUnitDailyConfigs.AddRange(depedentRecords.Select((x, ix) =>
                        new UnitConfigUnitDailyConfig
                        {
                            UnitDailyConfigId = record.Id,
                            UnitConfigId = x.Id,
                            Position = ix + 1
                        }).ToList());
                    record.IsConverted = true;
                    record.ProductId = record.ProductId == 0 ? 1 : record.ProductId;

                    data.UnitConfigs.Update(record);
                }
            }

            data.SaveChanges("InitialLoading");
        }

        /// <summary>
        /// Finds the dependant records.
        /// </summary>
        /// <param name="aggregationMembers">The aggregation members.</param>
        /// <returns></returns>
        private static IEnumerable<T> FindDependantRecords<T>(string aggregationMembers, IProductionData data)
            where T : class, IEntity, IAggregatable
        {
            if (string.IsNullOrEmpty(aggregationMembers))
            {
                return new List<T>();
            }

            var recordCodes = aggregationMembers.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var records = data.DbContext.Set<T>().ToList().Where(x => recordCodes.Any(y => y == x.Code));

            return records;
        }




        //private static void SeedShiftsToDatabase(ProductionData uow, DateTime dateParam)
        //{
        //    var shifts = uow.ProductionShifts.All().ToArray();

        //    var timer = new Stopwatch();

        //    timer.Start();

        //    // need to use some kind of structure to store shifts begin and end timestamps
        //    // TODO: Remove AddDays(-1)
        //    //var shift1BeginTimestamp = DateTime.Today.AddMinutes(shifts[0].BeginMinutes);
        //    //var shift1EndTimestamp = DateTime.Today.AddMinutes(shifts[0].BeginMinutes + shifts[0].OffsetMinutes);

        //    //var shift2BeginTimestamp = DateTime.Today.AddMinutes(shifts[1].BeginMinutes);
        //    //var shift2EndTimestamp = DateTime.Today.AddMinutes(shifts[1].BeginMinutes + shifts[1].OffsetMinutes);

        //    //var shift3BeginTimestamp = DateTime.Today.AddMinutes(shifts[2].BeginMinutes);
        //    //var shift3EndTimestamp = DateTime.Today.AddMinutes(shifts[2].BeginMinutes + shifts[2].OffsetMinutes);

        //    uow.DbContext.DbContext.Configuration.AutoDetectChangesEnabled = false;
        //    var unitDatas = uow.UnitsData.All().Where(x => x.RecordTimestamp < new DateTime(2015, 9, 15, 5, 30, 0)).ToList();

        //    (unitDatas.Where(x => x.RecordTimestamp == dateParam)).ToList() as ICollection<UnitsData>)
        //        .ForEach(x => { x.ShiftId = ShiftType.First; x.RecordTimestamp = x.RecordTimestamp.Date; })
        //        .ForEach(x =>
        //        {
        //            var ent = uow.DbContext.Entry<UnitsData>(x);
        //            if (ent.State == EntityState.Detached)
        //            {
        //                uow.DbContext.DbContext.Set<UnitsData>().Attach(x);
        //            }
        //            ent.State = EntityState.Modified;
        //        });

        //    (unitDatas.Where(x => x.RecordTimestamp.Between(shift2BeginTimestamp, shift2EndTimestamp)).ToList() as ICollection<UnitsData>)
        //        .ForEach(x => { x.ShiftId = ShiftType.Second; x.RecordTimestamp = x.RecordTimestamp.Date; })
        //         .ForEach(x =>
        //         {
        //             var ent = uow.DbContext.Entry<UnitsData>(x);
        //             if (ent.State == EntityState.Detached)
        //             {
        //                 uow.DbContext.DbContext.Set<UnitsData>().Attach(x);
        //             }
        //             ent.State = EntityState.Modified;
        //         });

        //    (unitDatas.Where(x => x.RecordTimestamp.Between(shift3BeginTimestamp, shift3EndTimestamp)).ToList() as ICollection<UnitsData>)
        //        .ForEach(x => { x.ShiftId = ShiftType.Third; x.RecordTimestamp = x.RecordTimestamp.AddDays(-1).Date; })
        //         .ForEach(x =>
        //         {
        //             var ent = uow.DbContext.Entry<UnitsData>(x);
        //             if (ent.State == EntityState.Detached)
        //             {
        //                 uow.DbContext.DbContext.Set<UnitsData>().Attach(x);
        //             }
        //             ent.State = EntityState.Modified;
        //         });
        //    uow.DbContext.DbContext.Configuration.AutoDetectChangesEnabled = false;
        //    uow.SaveChanges("Initial Loading");

        //}

        private static void TreeShiftsReports(DateTime dateParam, int processUnitIdParam)
        {
            using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(), new Logger())))
            {
                var timer = new Stopwatch();
                timer.Start();
                var data = context.UnitsData.All()
                    .Include(x => x.UnitConfig)
                    .Where(x => x.UnitConfig.ProcessUnitId == processUnitIdParam && x.RecordTimestamp == dateParam)
                    .ToList();

                var result = data.Select(x => new MultiShift
                {
                    TimeStamp = x.RecordTimestamp,
                    Code = x.UnitConfig.Code,
                    Position = x.UnitConfig.Position,
                    UnitConfigId = x.UnitConfigId,
                    UnitName = x.UnitConfig.Name,
                    Shift1 = data.Where(y => y.RecordTimestamp == dateParam && y.ShiftId == ShiftType.First).Where(u => u.UnitConfigId == x.UnitConfigId).FirstOrDefault(),
                    Shift2 = data.Where(y => y.RecordTimestamp == dateParam && y.ShiftId == ShiftType.Second).Where(u => u.UnitConfigId == x.UnitConfigId).FirstOrDefault(),
                    Shift3 = data.Where(y => y.RecordTimestamp == dateParam && y.ShiftId == ShiftType.Third).Where(u => u.UnitConfigId == x.UnitConfigId).FirstOrDefault(),
                }).Distinct(new MultiShiftComparer()).ToList();

                Console.WriteLine("Estimated Time For Action: {0}", timer.Elapsed);

                foreach (var item in result)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}", item.TimeStamp, item.UnitName, item.Shift1, item.Shift2, item.Shift3.RealValue/*,item.TotalQuantityValue*/);
                }

                Console.WriteLine("Estimated Time For Action: {0}", timer.Elapsed);
                timer.Stop();
            }
        }

        internal static void ProcessActiveTransactionsData(int offsetInDays)
        { 
            try
            {
                Console.WriteLine("Begin active transactions synchronization!");
                var now = DateTime.Now;
                var today = DateTime.Today.AddDays(offsetInDays * -1);
                var fiveOClock = new DateTime(today.Year, today.Month, today.Day, 4, 30, 0);
                
                var ts = now - fiveOClock;
                if(ts.TotalMinutes > 2 /*&& ts.Hours == 0*/)
                {
                    using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(),new Logger())))
                    {
                        var currentDate = today.AddDays(-1);
                        var existsActiveTransactionData = context.ActiveTransactionsDatas.All().Where(x => x.RecordTimestamp == currentDate).Any();
                        if (!existsActiveTransactionData)
                        {
                            var activeTransactionsTags = context.MeasuringPointConfigs.All().Where(x => !String.IsNullOrEmpty(x.ActiveTransactionStatusTag)).ToList();
                            if (activeTransactionsTags.Count > 0)
                            {
                                using (PHDHistorian oPhd = new PHDHistorian())
                                {
                                    using (PHDServer defaultServer = new PHDServer("srv-vm-mes-phd.neftochim.bg"))
                                    {
                                        defaultServer.Port = 3150;
                                        defaultServer.APIVersion = Uniformance.PHD.SERVERVERSION.RAPI200;
                                        oPhd.DefaultServer = defaultServer;
                                        // TODAY-2D7H23M
                                        oPhd.StartTime = string.Format("NOW - {0}D{1}H{2}M", ts.Days, ts.Hours, ts.Minutes);
                                        oPhd.EndTime = string.Format("NOW - {0}D{1}H{2}M", ts.Days, ts.Hours, ts.Minutes);
                                        oPhd.Sampletype = SAMPLETYPE.Snapshot;
                                        oPhd.MinimumConfidence = 100;
                                        oPhd.MaximumRows = 1;

                                        var tagsList = new Tags();
                                        foreach (var item in activeTransactionsTags)
                                        {
                                            var activeTransactionData = ProcessMeasuringPoint(tagsList, item, oPhd, currentDate, context);
                                            if (activeTransactionData != null)
                                            {
                                                context.ActiveTransactionsDatas.Add(activeTransactionData);
                                                Console.WriteLine(
                                                    string.Format("Active transaction processing TK [{3}] ProductId[{0}] Mass[{1}] MassReverse[{2}]",
                                                        activeTransactionData.ProductId,
                                                        activeTransactionData.Mass, 
                                                        activeTransactionData.MassReverse,
                                                        item.MeasuringPointName));
                                            }
                                        }

                                        context.SaveChanges("Phd2Sql");
                                    }
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("End active transactions synchronization!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }

        private static ActiveTransactionsData ProcessMeasuringPoint(Tags tagsList, MeasuringPointConfig item, PHDHistorian oPhd, DateTime currentDate, ProductionData context)
        {
            tagsList.RemoveAll();
            tagsList.Add(new Tag { TagName = item.ActiveTransactionStatusTag });
            var result = oPhd.FetchRowData(tagsList);
            var row = result.Tables[0].Rows[0];
            if (row["Value"].ToString() == "1")
            {
                tagsList.RemoveAll();
                tagsList.Add(new Tag { TagName = item.ActiveTransactionProductTag });
                if (item.DirectionId == 1)
                {
                    tagsList.Add(new Tag { TagName = item.ActiveTransactionMassTag });
                }
                else if (item.DirectionId == 2)
                {
                    tagsList.Add(new Tag { TagName = item.ActiveTransactionMassReverseTag });
                }
                else 
                { 
                    tagsList.Add(new Tag { TagName = item.ActiveTransactionMassTag });
                    tagsList.Add(new Tag { TagName = item.ActiveTransactionMassReverseTag });
                }

                var activeTransactionData = new ActiveTransactionsData
                {
                    RecordTimestamp = currentDate,
                    MeasuringPointConfigId = item.Id
                };

                var valueResult = oPhd.FetchRowData(tagsList);
                foreach (DataRow valueRow in valueResult.Tables[0].Rows)
                {
                    if (valueRow[0].ToString().Equals(item.ActiveTransactionProductTag))
                    {
                        int code = Convert.ToInt32(valueRow["Value"]);
                        var product = context.Products.All().Where(x => x.Code == code).FirstOrDefault();
                        activeTransactionData.ProductId = product != null ? product.Id : 1;
                    }
                    else 
                    {
                        var value = 0m;
                        if (valueRow[0].ToString().Equals(item.ActiveTransactionMassTag))
                        {
                            Console.WriteLine(item.ActiveTransactionMassTag);
                            if(decimal.TryParse(valueRow["Value"].ToString(), out value))
                            {
                                if (!string.IsNullOrEmpty(item.MassCorrectionFactor))
                                {
                                    Console.WriteLine(item.MassCorrectionFactor);
                                    value = value * Convert.ToDecimal(item.MassCorrectionFactor); 
                                }
                                activeTransactionData.Mass = value;
                            }
                        }
                        else if (valueRow[0].ToString().Equals(item.ActiveTransactionMassReverseTag))
                        {
                            Console.WriteLine(item.ActiveTransactionMassTag);
                            if(decimal.TryParse(valueRow["Value"].ToString(), out value))
                            {
                                if (!string.IsNullOrEmpty(item.MassCorrectionFactor))
                                {
                                    Console.WriteLine(item.MassCorrectionFactor);
                                    value = value * Convert.ToDecimal(item.MassCorrectionFactor); 
                                }
                                activeTransactionData.MassReverse = value;
                            }
                        }
                    }
                }

                return activeTransactionData;
            }

            return null;
        }

        internal static void ProcessScaleTransactionsData(int offsetInDays)
        {
            try
            {
                Console.WriteLine("Begin scale transactions synchronization!");
                {
                    var now = DateTime.Now;
                    var today = DateTime.Today.AddDays(offsetInDays * -1);
                    var fiveOClock = new DateTime(today.Year, today.Month, today.Day, 4, 30, 0);

                    Console.WriteLine(string.Format("Today.Ticks: {0}", today.Ticks));
                
                    var ts = now - fiveOClock;
                    if(ts.TotalMinutes > 2 /*&& ts.Hours == 0*/)
                    {
                        var phdValues = new Dictionary<int, long>();

                        using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(),new Logger())))
                        {
                            if (!context.MeasuringPointsConfigsDatas.All().Where(x => x.TransactionNumber >= today.Ticks).Any())
                            {

                                var currentPhdTimestamp = string.Format("NOW-{0}H{1}M", Math.Truncate(ts.TotalHours), ts.Minutes);
                                var previousPhdTimestamp = string.Format("NOW-{0}H{1}M", Math.Truncate(ts.TotalHours) + 24, ts.Minutes);

                                var scaleMeasuringPointProducts = context.MeasuringPointProductsConfigs
                                    .All()
                                    .Include(x => x.MeasuringPointConfig)
                                    .Include(x => x.MeasuringPointConfig.Zone)
                                    .Include(x => x.Product)
                                    .Include(x => x.Product.ProductType)
                                    .Include(x => x.Direction)
                                    .ToList();
                                foreach (var scaleMeasuringPointProduct in scaleMeasuringPointProducts)
                                {
                                    using (PHDHistorian oPhd = new PHDHistorian())
                                    {
                                        using (PHDServer defaultServer = new PHDServer("srv-vm-mes-phd.neftochim.bg"))
                                        {
                                            defaultServer.Port = 3150;
                                            defaultServer.APIVersion = Uniformance.PHD.SERVERVERSION.RAPI200;
                                            oPhd.DefaultServer = defaultServer;
                                            oPhd.StartTime = currentPhdTimestamp;
                                            oPhd.EndTime = currentPhdTimestamp;
                                            oPhd.Sampletype = SAMPLETYPE.Snapshot;
                                            oPhd.MinimumConfidence = 100;
                                            oPhd.MaximumRows = 1;

                                            var result = oPhd.FetchRowData(scaleMeasuringPointProduct.PhdProductTotalizerTag);
                                            var row = result.Tables[0].Rows[0];
                                            var value = Convert.ToInt64(row["Value"]);
                                            phdValues.Add(scaleMeasuringPointProduct.Id, value);
                                        }
                                    }
                                }

                                foreach (var scaleMeasuringPointProduct in scaleMeasuringPointProducts)
                                {
                                    using (PHDHistorian oPhd = new PHDHistorian())
                                    {
                                        using (PHDServer defaultServer = new PHDServer("srv-vm-mes-phd.neftochim.bg"))
                                        {
                                            defaultServer.Port = 3150;
                                            defaultServer.APIVersion = Uniformance.PHD.SERVERVERSION.RAPI200;
                                            oPhd.DefaultServer = defaultServer;
                                            oPhd.StartTime = previousPhdTimestamp;
                                            oPhd.EndTime = previousPhdTimestamp;
                                            oPhd.Sampletype = SAMPLETYPE.Snapshot;
                                            oPhd.MinimumConfidence = 100;
                                            oPhd.MaximumRows = 1;

                                            var result = oPhd.FetchRowData(scaleMeasuringPointProduct.PhdProductTotalizerTag);
                                            var row = result.Tables[0].Rows[0];
                                            var value = Convert.ToInt64(row["Value"]);
                                            phdValues[scaleMeasuringPointProduct.Id] = phdValues[scaleMeasuringPointProduct.Id] - value;
                                            //logger.InfoFormat("Processing data for {0} {1} {2} {3}", scaleMeasuringPointProduct.PhdProductTotalizerTag, currentPhdTimestamp, value, phdValues[scaleMeasuringPointProduct.Id]);
                                        }
                                    }
                                }

                                foreach (KeyValuePair<int, long> entry in phdValues)
                                {
                                    if (entry.Value > 0)
                                    {
                                        var scaleProduct = scaleMeasuringPointProducts.FirstOrDefault(x => x.Id == entry.Key);

                                        decimal? mass = null;
                                        decimal? revMass = null;
                                        if (scaleProduct.MeasuringPointConfig.DirectionId == 1)
                                        {
                                            mass = entry.Value;
                                        }
                                        else if (scaleProduct.MeasuringPointConfig.DirectionId == 2)
                                        {
                                            revMass = entry.Value;
                                        }
                                        else
                                        {
                                            if (scaleProduct.DirectionId == 1)
                                            {
                                                mass = entry.Value;
                                            }
                                            else
                                            {
                                                revMass = entry.Value;
                                            }
                                        }

                                        var measuringPointConfigData = new MeasuringPointsConfigsData
                                        {
                                            MeasuringPointConfigId = scaleProduct.MeasuringPointConfigId,
                                            TransactionNumber = today.Ticks + entry.Key,
                                            RowId = -1,
                                            Mass = mass,
                                            MassReverse = revMass,
                                            TransactionBeginTime = today,
                                            TransactionEndTime = today.AddHours(4),
                                            InsertTimestamp = DateTime.Now,
                                            MeasuringPointId = scaleProduct.MeasuringPointConfigId,
                                            ZoneId = scaleProduct.MeasuringPointConfig.Zone.Id,
                                            ProductId = scaleProduct.ProductId,
                                            ProductNumber = scaleProduct.Product.Code,
                                            ProductName = scaleProduct.Product.Name,
                                            ProductType = scaleProduct.Product.ProductType.Id,
                                            BaseProductNumber = scaleProduct.Product.Code,
                                            BaseProductName = scaleProduct.Product.Name,
                                            BaseProductType = scaleProduct.Product.ProductType.Id,
                                            FlowDirection = scaleProduct.DirectionId
                                        };
                                        Console.WriteLine(string.Format("Processing virtual transaction for {0} {1} {2} {3}", entry.Key, scaleProduct.PhdProductTotalizerTag, entry.Value, scaleProduct.Direction.Name));
                                        context.MeasuringPointsConfigsDatas.Add(measuringPointConfigData);
                                    }
                                }

                                context.SaveChanges("Phd2SqlTotalizer");
                            }
                        }
                    }
                }

                Console.WriteLine("End scale transactions synchronization!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }

        internal static void ProcessTransactionsData()
        {
            try
            {
                Console.WriteLine("Begin synchronization!");
                
                using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(),new Logger())))
                {
                    var max = context.MaxAsoMeasuringPointDataSequenceNumberMap.All().FirstOrDefault();
                    if (max != null)
                    {
                        var transactions = GetTransactionsFromAso(max, context);
                        if (transactions.Count > 0)
                        {
                            context.MeasuringPointsConfigsDatas.BulkInsert(transactions, "Aso2SapoLoader");
                            var status = context.SaveChanges("Aso2Sql");
                            max.LastTransactionsFetchingDateTime = DateTime.Now;
                            context.MaxAsoMeasuringPointDataSequenceNumberMap.Update(max);
                            context.SaveChanges("Aso2Sql");
                            Console.WriteLine("Successfully synchronization {0} records from Aso to Cpds", status.ResultRecordsCount);
                            Console.WriteLine("Last transactions fetching date-time was updated to {0}.", DateTime.Now);   
                        }
                    }

                    Console.WriteLine("End synchronization");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }

        private static List<MeasuringPointsConfigsData> GetTransactionsFromAso(MaxAsoMeasuringPointDataSequenceNumber max, ProductionData context)
        {
            var result = new List<MeasuringPointsConfigsData>();
            Console.WriteLine(string.Format("Last transaction fetching date-time was {0}", max.LastTransactionsFetchingDateTime));
            var maxLastTransactionsFetchingDateTime = max.LastTransactionsFetchingDateTime;
            var adapter = new AsoDataSetTableAdapters.flow_MeasuringPointsDataTableAdapter();
            var table = new AsoDataSet.flow_MeasuringPointsDataDataTable();
            adapter.Fill(table, maxLastTransactionsFetchingDateTime.Value);
            if (table.Rows.Count > 0)
            {
                var mesurinpPointsByTransactions = context.MeasuringPointConfigs.All().Where(x => x.IsUsedPhdTotalizers == true).Select(x => x.Id).ToList();

                foreach (AsoDataSet.flow_MeasuringPointsDataRow row in table.Rows)
                {
                    if (mesurinpPointsByTransactions.Contains(row.MeasuringPointId))
                    {
                        continue;
                    }

                    var tr = new MeasuringPointsConfigsData();
                    tr.MeasuringPointId = row.MeasuringPointId;
                    tr.MeasuringPointConfigId = row.MeasuringPointId;
                    tr.TransactionNumber = row.TransactionNumber;
                    tr.RowId = row.RowId;
                    tr.TransactionBeginTime = row.TransactionBeginTime;
                    tr.TransactionEndTime = row.TransactionEndTime;
                    tr.ExciseStoreId = row.ExciseStoreId;
                    tr.ZoneId = row.ZoneId;
                    tr.BaseProductNumber = row.BaseProductNumber;
                    tr.BaseProductType = row.BaseProductType;
                    tr.BaseProductName = row.BaseProductName;
                    tr.ProductNumber = row.ProductNumber;
                    var prod = context.Products.All().Where(p => p.Code == row.ProductNumber).FirstOrDefault();
                    tr.ProductId = prod.Id;
                    tr.ProductType = row.ProductType;
                    tr.ProductName = row.ProductName;
                    tr.FlowDirection = row.FlowDirection;
                    tr.EngineeringUnitMass = row.EngineeringUnitMass;
                    tr.EngineeringUnitVolume = row.EngineeringUnitVolume;
                    tr.EngineeringUnitDensity = row.EngineeringUnitDensity;
                    tr.EngineeringUnitTemperature = row.EngineeringUnitTemperature;
                    tr.FactorMass = row.FactorMass;
                    tr.FactorVolume = row.FactorVolume;
                    tr.FactorDensity = row.FactorDensity;
                    tr.FactorTemperature = row.FactorTemperature;
                    if (!row.IsTotalizerBeginGrossObservableVolumeNull())
                    {
                        tr.TotalizerBeginGrossObservableVolume = row.TotalizerBeginGrossObservableVolume;
                    }
                    if (!row.IsTotalizerEndGrossObservableVolumeNull())
                    {
                        tr.TotalizerEndGrossObservableVolume = row.TotalizerEndGrossObservableVolume;
                    }
                    if (!row.IsTotalizerBeginGrossStandardVolumeNull())
                    {
                        tr.TotalizerBeginGrossStandardVolume = row.TotalizerBeginGrossStandardVolume;
                    }
                    if (!row.IsTotalizerEndGrossStandardVolumeNull())
                    {
                        tr.TotalizerEndGrossStandardVolume = row.TotalizerEndGrossStandardVolume;
                    }
                    if (!row.IsTotalizerBeginMassNull())
                    {
                        tr.TotalizerBeginMass = row.TotalizerBeginMass;
                    }
                    if (!row.IsTotalizerEndMassNull())
                    {
                        tr.TotalizerEndMass = row.TotalizerEndMass;
                    }
                    if (!row.IsTotalizerBeginGrossObservableCommonVolumeNull())
                    {
                        tr.TotalizerBeginGrossObservableCommonVolume = row.TotalizerBeginGrossObservableCommonVolume;
                    }
                    if (!row.IsTotalizerEndGrossObservableCommonVolumeNull())
                    {
                        tr.TotalizerEndGrossObservableCommonVolume = row.TotalizerEndGrossObservableCommonVolume;
                    }
                    if (!row.IsTotalizerBeginGrossStandardCommonVolumeNull())
                    {
                        tr.TotalizerBeginGrossStandardCommonVolume = row.TotalizerBeginGrossStandardCommonVolume;
                    }
                    if (!row.IsTotalizerEndGrossStandardCommonVolumeNull())
                    {
                        tr.TotalizerEndGrossStandardCommonVolume = row.TotalizerEndGrossStandardCommonVolume;
                    }
                    if (!row.IsTotalizerBeginCommonMassNull())
                    {
                        tr.TotalizerBeginCommonMass = row.TotalizerBeginCommonMass;
                    }
                    if (!row.IsTotalizerEndCommonMassNull())
                    {
                        tr.TotalizerEndCommonMass = row.TotalizerEndCommonMass;
                    }
                    if (!row.IsGrossObservableVolumeNull())
                    {
                        tr.GrossObservableVolume = row.GrossObservableVolume;
                    }
                    if (!row.IsGrossStandardVolumeNull())
                    {
                        tr.GrossStandardVolume = row.GrossStandardVolume;
                    }
                    if (!row.IsMassNull())
                    {
                        tr.Mass = row.Mass;
                    }
                    if (!row.IsAverageObservableDensityNull())
                    {
                        tr.AverageObservableDensity = row.AverageObservableDensity;
                    }
                    if (!row.IsAverageReferenceDensityNull())
                    {
                        tr.AverageReferenceDensity = row.AverageReferenceDensity;
                    }
                    if (!row.IsAverageTemperatureNull())
                    {
                        tr.AverageTemperature = row.AverageTemperature;
                    }
                    if (!row.IsTotalizerBeginGrossObservableVolumeReverseNull())
                    {
                        tr.TotalizerBeginGrossObservableVolumeReverse = row.TotalizerBeginGrossObservableVolumeReverse;
                    }
                    if (!row.IsTotalizerEndGrossObservableVolumeReverseNull())
                    {
                        tr.TotalizerEndGrossObservableVolumeReverse = row.TotalizerEndGrossObservableVolumeReverse;
                    }
                    if (!row.IsTotalizerBeginGrossStandardVolumeReverseNull())
                    {
                        tr.TotalizerBeginGrossStandardVolumeReverse = row.TotalizerBeginGrossStandardVolumeReverse;
                    }
                    if (!row.IsTotalizerEndGrossStandardVolumeReverseNull())
                    {
                        tr.TotalizerEndGrossStandardVolumeReverse = row.TotalizerEndGrossStandardVolumeReverse;
                    }
                    if (!row.IsTotalizerBeginMassReverseNull())
                    {
                        tr.TotalizerBeginMassReverse = row.TotalizerBeginMassReverse;
                    }
                    if (!row.IsTotalizerEndMassReverseNull())
                    {
                        tr.TotalizerEndMassReverse = row.TotalizerEndMassReverse;
                    }
                    if (!row.IsTotalizerBeginGrossObservableCommonVolumeReverseNull())
                    {
                        tr.TotalizerBeginGrossObservableCommonVolumeReverse = row.TotalizerBeginGrossObservableCommonVolumeReverse;
                    }
                    if (!row.IsTotalizerEndGrossObservableCommonVolumeReverseNull())
                    {
                        tr.TotalizerEndGrossObservableCommonVolumeReverse = row.TotalizerEndGrossObservableCommonVolumeReverse;
                    }
                    if (!row.IsTotalizerBeginGrossStandardCommonVolumeReverseNull())
                    {
                        tr.TotalizerBeginGrossStandardCommonVolumeReverse = row.TotalizerBeginGrossStandardCommonVolumeReverse;
                    }
                    if (!row.IsTotalizerEndGrossStandardCommonVolumeReverseNull())
                    {
                        tr.TotalizerEndGrossStandardCommonVolumeReverse = row.TotalizerEndGrossStandardCommonVolumeReverse;
                    }
                    if (!row.IsTotalizerBeginCommonMassReverseNull())
                    {
                        tr.TotalizerBeginCommonMassReverse = row.TotalizerBeginCommonMassReverse;
                    }
                    if (!row.IsTotalizerEndCommonMassReverseNull())
                    {
                        tr.TotalizerEndCommonMassReverse = row.TotalizerEndCommonMassReverse;
                    }
                    if (!row.IsGrossObservableVolumeReverseNull())
                    {
                        tr.GrossObservableVolumeReverse = row.GrossObservableVolumeReverse;
                    }
                    if (!row.IsGrossStandardVolumeReverseNull())
                    {
                        tr.GrossStandardVolumeReverse = row.GrossStandardVolumeReverse;
                    }
                    if (!row.IsMassReverseNull())
                    {
                        tr.MassReverse = row.MassReverse;
                    }
                    if (!row.IsAverageObservableDensityReverseNull())
                    {
                        tr.AverageObservableDensityReverse = row.AverageObservableDensityReverse;
                    }
                    if (!row.IsAverageReferenceDensityReverseNull())
                    {
                        tr.AverageReferenceDensityReverse = row.AverageReferenceDensityReverse;
                    }
                    if (!row.IsAverageTemperatureReverseNull())
                    {
                        tr.AverageTemperatureReverse = row.AverageTemperatureReverse;
                    }
                    tr.InsertTimestamp = row.InsertTimestamp;
                    if (!row.IsResultIdNull())
                    {
                        tr.ResultId = row.ResultId;
                    }
                    if (!row.IsAdditiveResultIdNull())
                    {
                        tr.AdditiveResultId = row.AdditiveResultId;
                    }
                    if (!row.IsTamasCreateTimestampNull())
                    {
                        tr.TamasCreateTimestamp = row.TamasCreateTimestamp;
                    }
                    if (!row.IsTamasRecipeTransIdNull())
                    {
                        tr.TamasRecipeTransId = row.TamasRecipeTransId;
                    }
                    if (!row.IsEpksSequenceNumberNull())
                    {
                        tr.EpksSequenceNumber = row.EpksSequenceNumber;
                    }
                    if (!row.IsMartaRowNumNull())
                    {
                        tr.MartaRowNum = row.MartaRowNum;
                    }
                    if (!row.IsAlcoholContentNull())
                    {
                        tr.AlcoholContent = row.AlcoholContent;
                    }
                    if (!row.IsAlcoholContentReverseNull())
                    {
                        tr.AlcoholContentReverse = row.AlcoholContentReverse;
                    }
                    if (!row.IsBatchIdNull())
                    {
                        tr.BatchId = row.BatchId;
                    }

                    Console.WriteLine(string.Format("Processing sequence number {0}", row.SequenceNumber));
                    result.Add(tr);
                }
            }

            return result;
        }

        internal static void DoCalculation()
        {
            using (PHDHistorian oPhd = new PHDHistorian())
            {
                using (PHDServer defaultServer = new PHDServer("srv-vm-mes-phd.neftochim.bg"))
                {
                    defaultServer.Port = 3150;
                    defaultServer.APIVersion = Uniformance.PHD.SERVERVERSION.RAPI200;
                    oPhd.DefaultServer = defaultServer;
                    oPhd.Sampletype = SAMPLETYPE.Snapshot;
                    oPhd.MinimumConfidence = 100;
                    oPhd.MaximumRows = 1;

                     var tags = "TSN_KT014009_QN_T.PV@TSN_KT014009_PD.PV@1000".Split('@');

                    var recordDataTime = new DateTime(2016, 2, 23, 0, 0, 0);

                    //468000000000
                    //756000000000
                    //1044000000000


                    var end = recordDataTime.AddTicks(1044000000000);
                    var begin = end.AddHours(-8);

                    var endTimestamp = DateTime.Now - end;
                    var beginTimestamp = DateTime.Now - begin;

                    var endPhdTimestamp = string.Format("NOW-{0}H{1}M", Math.Truncate(endTimestamp.TotalHours), endTimestamp.Minutes);
                    var beginPhdTimestamp = string.Format("NOW-{0}H{1}M", Math.Truncate(beginTimestamp.TotalHours), beginTimestamp.Minutes);

                    oPhd.StartTime = endPhdTimestamp;
                    oPhd.EndTime = endPhdTimestamp;
                    var result = oPhd.FetchRowData(tags[0]);
                    var row = result.Tables[0].Rows[0];
                    var endValue = Convert.ToInt64(row["Value"]);

                    result = oPhd.FetchRowData(tags[1]);
                    row = result.Tables[0].Rows[0];
                    var pressure = Convert.ToDecimal(row["Value"]);

                    oPhd.StartTime = beginPhdTimestamp;
                    oPhd.EndTime = beginPhdTimestamp;
                    result = oPhd.FetchRowData(tags[0]);
                    row = result.Tables[0].Rows[0];
                    var beginValue = Convert.ToInt64(row["Value"]);

                    var value = ((endValue - beginValue) * pressure) / Convert.ToDecimal(tags[2]);
                    Console.WriteLine(value);
                }
            }



           
        }
    }

}
