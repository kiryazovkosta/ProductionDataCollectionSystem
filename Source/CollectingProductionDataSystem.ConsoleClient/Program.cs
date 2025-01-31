﻿using System.Data.Entity;
using System.IO;
using CollectingProductionDataSystem.Application.FileServices;
using CollectingProductionDataSystem.Data;
using CollectingProductionDataSystem.Data.Concrete;
using CollectingProductionDataSystem.Data.Contracts;
using CollectingProductionDataSystem.Enumerations;
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
using CollectingProductionDataSystem.Models.Productions.Mounthly;
using CollectingProductionDataSystem.PhdApplication.Contracts;
using Ninject;
using Uniformance.PHD;
using CollectingProductionDataSystem.Models.Transactions;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using CollectingProductionDataSystem.Data.Common;
using CollectingProductionDataSystem.Extentions;
using CollectingProductionDataSystem.Application.MonthlyTechnologicalDataServices;
using CollectingProductionDataSystem.Application.CalculatorService;
using CollectingProductionDataSystem.Application.TransactionsDailyDataServices;
using CollectingProductionDataSystem.Constants;
using CollectingProductionDataSystem.Models.Inventories;
using CollectingProductionDataSystem.Application.Contracts;
using System.Transactions;
using System.Runtime.Serialization.Formatters.Binary;
using AutoMapper;
using CollectingProductionDataSystem.ConsoleClient.Models;
using CollectingProductionDataSystem.Infrastructure.Mapping;

namespace CollectingProductionDataSystem.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ninject = new NinjectConfig();
            var timer = new Stopwatch();
            timer.Start();
            IKernel kernel = NinjectConfig.GetInjector;

            IProductionData data = kernel.Get<ProductionData>();
            ICalculatorService calculator = kernel.Get<CalculatorService>();
            ConfigAutoMapper();

            //DateTime targetRecordDateTime = DateTime.Now;
            //var tanksPhdConfigs = new Dictionary<string, int>();
            //var tanksData = new Dictionary<int, TankData>();

            //List<TankConfig> tanks = data.Tanks.All().Include(x => x.Park).ToList();
            //foreach (var tank in tanks)
            //{
            //    var tankData = new TankData();
            //    tankData.RecordTimestamp = new DateTime(year: 2016, month: 12, day: 23, hour: 15, minute: 0, second: 0);
            //    tankData.TankConfigId = tank.Id;
            //    tankData.ParkId = tank.ParkId;
            //    tankData.NetStandardVolume = tank.UnusableResidueLevel;
            //    tankData.Confidence = 100;

            //    SetTankPhdConfigValue(tank.PhdTagProductId, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagLiquidLevel, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagProductLevel, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagFreeWaterLevel, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagReferenceDensity, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagNetStandardVolume, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagWeightInAir, tank.Id, tanksPhdConfigs);
            //    SetTankPhdConfigValue(tank.PhdTagWeightInVacuum, tank.Id, tanksPhdConfigs);
            //    tanksData.Add(tank.Id, tankData);
            //}

            //List<string> tags = tanksPhdConfigs.Keys.ToList();
            //if (tags.Count() > 0)
            //{
            //    using (PHDHistorian oPhd = new PHDHistorian())
            //    {
            //        string phdServer = "10.94.0.213";
            //        using (PHDServer defaultServer = new PHDServer(phdServer))
            //        {
            //            defaultServer.Port = 3150;
            //            defaultServer.APIVersion = Uniformance.PHD.SERVERVERSION.RAPI200;
            //            oPhd.DefaultServer = defaultServer;
            //            oPhd.StartTime = "12/23/2016 3:0:0 PM";
            //            oPhd.EndTime = "12/23/2016 3:0:0 PM";
            //            oPhd.Sampletype = SAMPLETYPE.Raw;
            //            oPhd.MinimumConfidence = 100;
            //            oPhd.MaximumRows = 1;

            //            var oTags = new Tags();
            //            foreach (var tag in tags)
            //            {
            //                oTags.Add(new Tag(tag));
            //            }

            //            DataSet result = oPhd.FetchRowData(oTags);
            //            ProcessingPhdTankData(result.Tables[0], tanksData, tanksPhdConfigs, targetRecordDateTime, data);
            //            foreach (var tankData in tanksData)
            //            {
            //                Console.WriteLine($"{tankData.Value.TankConfigId} {tankData.Value.ParkId} {tankData.Value.ProductName} {tankData.Value.WeightInVacuum} {tankData.Value.LiquidLevel} {tankData.Value.NetStandardVolume}");
            //            }

            //            if (tanksData.Count() > 0)
            //            {
            //                data.TanksData.BulkInsert(tanksData.Values.ToList(), userName: "Phd2SqlLoading");
            //                data.SaveChanges(userName: "Phd2SqlLoading");
            //                Console.WriteLine($"Successfully added {tanksData.Count()} TanksData records for: {targetRecordDateTime:yyyy-MM-dd HH:ss:mm}!");
            //            }
            //            result = null;
            //        }
            //    }
            //}




            //var transactions = kernel.Get<TransactionsDailyDataService>();
            //var date = new DateTime(year: 2016, month: 10, day: 30, hour: 0, minute: 0, second: 0);
            //HashSet<MeasuringPointsConfigsReportData> output = transactions.ReadTransactionsDailyData(date, CommonConstants.OutputDirection);
            //var result = new List<MeasuringPointsConfigsReportData>();
            //foreach (var item in output)
            //{
            //    MeasuringPointsConfigsReportData record = item;
            //    record.RecordTimestamp = date;
            //    record.Direction = CommonConstants.OutputDirection;
            //    result.Add(record);
            //}

            //data.MeasuringPointsConfigsReportDatas.BulkInsert(result, userName: "Initial Loading");
            //data.SaveChanges(userName: "Initial Loading");

            //Console.WriteLine(result.Count());

            //WritePositionsConfidence(data);
            //UpdateShiftUnitData(kernel, data);

            //AddOrUpdateProductionPlanConfigs(data);
            //var targetMonth = DateTime.Today;
            //var service = new MonthlyTechnicalDataService(data, kernel, calculator);
            //var monthlyProductionData = service.ReadMonthlyTechnologicalData(targetMonth);
            //using (var sw = new StreamWriter(@"c:\temp\MonthlyTechReport.txt", false))
            //{
            //    sw.WriteLine("Id;Code;Name;Factory;ProcessUnit;MaterialType;MeasurementUnit;;PlanValue;PlanPercentage;;FactValue;FactPercentage;FactValueDifference;FactPercentageDifference;;YearValue;YearPercentage;YearValueDifference;YearPercentageDifference");
            //    foreach (var item in monthlyProductionData)
            //    {
            //        sw.WriteLine(item.ToString());
            //    }
            //}

            //Console.WriteLine(monthlyProductionData.Count());



            var lastRealDate = new DateTime(year: 2017, month: 2, day: 9, hour: 0, minute: 0, second: 0);
            //CreateShiftData(data, lastRealDate);
            //CreateDailyData(data, lastRealDate);
            CreateProductionPlanData(data, lastRealDate);

            //TransactionOptions transantionOption = DefaultTransactionOptions.Instance.TransactionOptions;
            //using (var transaction = new TransactionScope(TransactionScopeOption.Required, transantionOption))
            //{
            //    try
            //    {
            //        //CopyRelatedUnitConfigs(data);
            //        //CopyRelatedUnitDailyConfigs(data);
            //        //CopyUnitConfigUnitDailyConfigs(data);

                    
            //        //UpdatePlanNorms(data, 16, 52);
            //        //UpdatePlanNorms(data, 17, 53);
            //        //UpdateProductionPlanDatas(data);
            //        UpdateUnitsApprovedDailyDatas(data);

            //        //UpdateApplicationUserProcessUnits(data);

            //        //transaction.Dispose();
            //        //transaction.Complete();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        transaction.Dispose();
            //    }
            //    finally
            //    {
            //        //transaction.Dispose();
            //    }
            //}



            //UpdateShiftUnitData(kernel, data);
            //var shiftData = data.UnitsData.All().Where(x => x.ShiftId == ShiftType.Second 
            //    && x.RecordTimestamp == new DateTime(2016, 2, 1, 0, 0, 0) 
            //    && x.UnitConfig.ProcessUnitId == 50).ToList();
            //int monthIndex = DateTime.Now.Month - 1;
            //var lastDate = new DateTime(2016, 2, 19, 0, 0, 0);
            //var shiftData = data.UnitsData.All().Where(x => x.ShiftId == ShiftType.Second
            //    && x.RecordTimestamp == lastDate
            //    && x.UnitConfig.ProcessUnitId == 37).ToList();
            //var prevDay = lastDate.AddDays(-1);
            //while (prevDay.Month == monthIndex)
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
            //DoCalculation();
            //ProcessProductionReportTransactionsData();
            //System.Console.WriteLine("finished");
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
            Console.WriteLine("finished");
        }

        private static void UpdateApplicationUserProcessUnits(IProductionData data)
        {
        }

        private static void UpdatePlanNorms(IProductionData data, int oldIndex, int newIndex)
        {
           var oldProductionPlanConfigs =
           data.ProductionPlanConfigs.All()
               .Where(x => x.ProcessUnitId == oldIndex && x.IsDeleted == false)
               .ToDictionary(x => x.Code, x => x.Id);
            var newUProductionPlanConfigs = data.ProductionPlanConfigs.AllWithDeleted()
                .Where(x => x.ProcessUnitId == newIndex)
                .ToDictionary(x => x.Code, x => x.Id);

            var productionPlanConfigsIndexes = new Dictionary<int, int>();
            foreach (var config in oldProductionPlanConfigs)
            {
                var newId = newUProductionPlanConfigs[config.Key];
                productionPlanConfigsIndexes.Add(config.Value, newId);
            }
            Console.WriteLine($"UpdatePlanNorms->{oldProductionPlanConfigs.Count}");
            Console.WriteLine($"UpdatePlanNorms->{newUProductionPlanConfigs.Count}");
            Console.WriteLine($"UpdatePlanNorms->{productionPlanConfigsIndexes.Count}");

            var firstDayInMonth = new DateTime(2017, 2, 1, 0, 0, 0);
            var planNormsDatas = data.PlanNorms.All()
                .Where( x =>x.Month == firstDayInMonth)
                .Where(x => x.ProductionPlanConfig.ProcessUnitId == oldIndex)
                .ToList();
            foreach (var planNormsData in planNormsDatas)
            {
                Console.WriteLine(planNormsData.ProductionPlanConfig.ProcessUnitId);
                if (planNormsData.ProductionPlanConfig.ProcessUnitId == oldIndex)
                {
                    var key = productionPlanConfigsIndexes[planNormsData.ProductionPlanConfigId];
                    Console.WriteLine($"{planNormsData.ProductionPlanConfigId};{key}");
                    planNormsData.ProductionPlanConfigId = key;
                    data.PlanNorms.Update(planNormsData);
                }

            }

            var status = data.SaveChanges("Updater");
        }

        private static void UpdateUnitsApprovedDailyDatas(IProductionData data)
        {
            var firstDayInMonth = new DateTime(2017, 2, 1, 0, 0, 0);
            var oldProcessUnitsIndexes = new int[] { 16, 17 };
            var approvedDatas = data.UnitsApprovedDailyDatas.All()
                .Where(
                    x =>
                        x.RecordDate.CompareTo(firstDayInMonth) >= 0 &&
                        oldProcessUnitsIndexes.Contains(x.ProcessUnitId))
                .ToList();
            Console.WriteLine($"UpdateUnitsApprovedDailyDatas->{approvedDatas.Count}");

            foreach (var approvedData in approvedDatas)
            {
                if (approvedData.ProcessUnitId == 16)
                {
                    approvedData.ProcessUnitId = 52;
                }

                if (approvedData.ProcessUnitId == 17)
                {
                    approvedData.ProcessUnitId = 53;
                }

                data.UnitsApprovedDailyDatas.Update(approvedData);
            }

            data.SaveChanges("Updater");
        }

        private static void UpdateUnitsApprovedDatas(IProductionData data)
        {
            var firstDayInMonth = new DateTime(2017, 2, 1, 0, 0, 0);
            var oldProcessUnitsIndexes = new int[] { 16, 17 };
            var approvedDatas = data.UnitsApprovedDatas.All()
                .Where(
                    x =>
                        x.RecordDate.CompareTo(firstDayInMonth) >= 0 &&
                        oldProcessUnitsIndexes.Contains(x.ProcessUnitId))
                .ToList();
            Console.WriteLine(approvedDatas.Count);
            foreach (var approvedData in approvedDatas)
            {
                if (approvedData.ProcessUnitId == 16)
                {
                    approvedData.ProcessUnitId = 52;
                }

                if (approvedData.ProcessUnitId == 17)
                {
                    approvedData.ProcessUnitId = 53;
                }

                data.UnitsApprovedDatas.Update(approvedData);
            }

            data.SaveChanges("Updater");
        }

        private static void UpdateProductionPlanDatas(IProductionData data)
        {
            var oldProductionPlanConfigs =
                       data.ProductionPlanConfigs.All()
                           .Where(x => x.ProcessUnitId == 16 || x.ProcessUnitId == 17 && x.IsDeleted == false)
                           .ToDictionary(x => x.Code, x => x.Id);
            var newUProductionPlanConfigs = data.ProductionPlanConfigs.AllWithDeleted()
                .Where(x => x.ProcessUnitId == 52 || x.ProcessUnitId == 53)
                .ToDictionary(x => x.Code, x => x.Id);

            var productionPlanConfigsIndexes = new Dictionary<int, int>();
            foreach (var config in oldProductionPlanConfigs)
            {
                var newId = newUProductionPlanConfigs[config.Key];
                productionPlanConfigsIndexes.Add(config.Value, newId);
            }
            Console.WriteLine($"UpdateProductionPlanDatas->{oldProductionPlanConfigs.Count}");
            Console.WriteLine($"UpdateProductionPlanDatas->{newUProductionPlanConfigs.Count}");
            Console.WriteLine($"UpdateProductionPlanDatas->{productionPlanConfigsIndexes.Count}");

            var firstDayInMonth = new DateTime(2017, 2, 1, 0, 0, 0);
            var oldProcessUnitsIndexes = new int[] {16, 17};
            var productionPlanDatas = data.ProductionPlanDatas.All()
                .Where(
                    x =>
                        x.RecordTimestamp.CompareTo(firstDayInMonth) >= 0 &&
                        oldProcessUnitsIndexes.Contains(x.ProcessUnitId))
                .ToList();
            foreach (var productionPlanData in productionPlanDatas)
            {
                if (productionPlanData.ProcessUnitId == 16)
                {
                    productionPlanData.ProcessUnitId = 52;
                }

                if (productionPlanData.ProcessUnitId == 17)
                {
                    productionPlanData.ProcessUnitId = 53;
                }

                productionPlanData.FactoryId = 4;
                var key = productionPlanConfigsIndexes[productionPlanData.ProductionPlanConfigId];
                Console.WriteLine($"{productionPlanData.ProductionPlanConfigId};{key}");
                productionPlanData.ProductionPlanConfigId = key;
                data.ProductionPlanDatas.Update(productionPlanData);
            }

            data.SaveChanges("Updater");
        }

        private static void CopyUnitConfigUnitDailyConfigs(IProductionData data)
        {
            var oldUnitConfigs =
                        data.UnitConfigs.All()
                            .Where(x => x.ProcessUnitId == 16 || x.ProcessUnitId == 17 && x.IsDeleted == false)
                            .ToDictionary(x => x.Code, x => x.Id);
            var newUnitConfigs = data.UnitConfigs.AllWithDeleted()
                .Where(x => x.ProcessUnitId == 52 || x.ProcessUnitId == 53)
                .ToDictionary(x => x.Code, x => x.Id);

            var unitCongigsIndexes = new Dictionary<int, int>();
            foreach (var config in oldUnitConfigs)
            {
                var newId = newUnitConfigs[config.Key];
                unitCongigsIndexes.Add(config.Value, newId);
            }
            Console.WriteLine($"CopyUnitConfigUnitDailyConfigs->{oldUnitConfigs.Count}");
            Console.WriteLine($"CopyUnitConfigUnitDailyConfigs->{newUnitConfigs.Count}");
            Console.WriteLine($"CopyUnitConfigUnitDailyConfigs->{unitCongigsIndexes.Count}");

            var oldUnitDailyConfigs =
                       data.UnitsDailyConfigs.All()
                           .Where(x => x.ProcessUnitId == 16 || x.ProcessUnitId == 17 && x.IsDeleted == false)
                           .ToDictionary(x => x.Code, x => x.Id);
            var newUnitDailyConfigs = data.UnitsDailyConfigs.AllWithDeleted()
                .Where(x => x.ProcessUnitId == 52 || x.ProcessUnitId == 53)
                .ToDictionary(x => x.Code, x => x.Id);

            var unitDailyConfigsIndexes = new Dictionary<int, int>();
            foreach (var config in oldUnitDailyConfigs)
            {
                var newId = newUnitDailyConfigs[config.Key];
                unitDailyConfigsIndexes.Add(config.Value, newId);
            }

            var relatedRecords = new Dictionary<string, int>();
            foreach (var item in oldUnitConfigs)
            {
                var relatedUnitConfigs =
                    data.UnitConfigUnitDailyConfigs.All().Where(x => x.UnitConfigId == item.Value).ToList();
                foreach (var record in relatedUnitConfigs)
                {
                    var key = record.UnitConfigId + "+" + record.UnitDailyConfigId;
                    if (!relatedRecords.ContainsKey(key))
                    {
                        relatedRecords.Add(key, record.Position);
                    }
                }
            }

            foreach (var item in oldUnitDailyConfigs)
            {
                var relatedDailyUnitConfigs =
                    data.UnitConfigUnitDailyConfigs.All().Where(x => x.UnitDailyConfigId == item.Value).ToList();
                foreach (var record in relatedDailyUnitConfigs)
                {
                    var key = record.UnitConfigId + "+" + record.UnitDailyConfigId;
                    if (!relatedRecords.ContainsKey(key))
                    {
                        relatedRecords.Add(key, record.Position);
                    }
                }
            }

            var keys = relatedRecords.Keys;

            foreach (var key in keys)
            {
                var relatedData = key.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                var ucId = Convert.ToInt32(relatedData[0]);
                var ucdId = Convert.ToInt32(relatedData[1]);
                if (unitCongigsIndexes.ContainsKey(ucId))
                {
                    ucId = unitCongigsIndexes[ucId];
                }

                if (unitDailyConfigsIndexes.ContainsKey(ucdId))
                {
                    ucdId = unitDailyConfigsIndexes[ucdId];
                }

                var unitConfigUnitDailyConfigRecord = new UnitConfigUnitDailyConfig()
                {
                    UnitConfigId = ucId,
                    UnitDailyConfigId = ucdId,
                    Position = relatedRecords[key]
                };

                Console.WriteLine($"{unitConfigUnitDailyConfigRecord.UnitConfigId};{unitConfigUnitDailyConfigRecord.UnitDailyConfigId};{unitConfigUnitDailyConfigRecord.Position}");
                data.UnitConfigUnitDailyConfigs.Add(unitConfigUnitDailyConfigRecord);
            }

            var status = data.SaveChanges("Coping");
        }

        private static void CopyRelatedUnitDailyConfigs(IProductionData data)
        {
            var oldUnitDailyConfigs =
                       data.UnitsDailyConfigs.All()
                           .Where(x => x.ProcessUnitId == 16 || x.ProcessUnitId == 17 && x.IsDeleted == false)
                           .ToDictionary(x => x.Code, x => x.Id);
            var newUnitDailyConfigs = data.UnitsDailyConfigs.AllWithDeleted()
                .Where(x => x.ProcessUnitId == 52 || x.ProcessUnitId == 53)
                .ToDictionary(x => x.Code, x => x.Id);

            var unitDailyConfigsIndexes = new Dictionary<int, int>();
            foreach (var config in oldUnitDailyConfigs)
            {
                var newId = newUnitDailyConfigs[config.Key];
                unitDailyConfigsIndexes.Add(config.Value, newId);
            }
            Console.WriteLine($"CopyRelatedUnitDailyConfigs->{oldUnitDailyConfigs.Count}");
            Console.WriteLine($"CopyRelatedUnitDailyConfigs->{newUnitDailyConfigs.Count}");
            Console.WriteLine($"CopyRelatedUnitDailyConfigs->{unitDailyConfigsIndexes.Count}");

            var relatedDailyRecords = new Dictionary<string, int>();

            foreach (var config in oldUnitDailyConfigs)
            {
                var relatedUnitDailyConfigs =
                    data.RelatedUnitDailyConfigs.All()
                        .Where(x => x.UnitsDailyConfigId == config.Value).ToList();

                var relatedRelatedUnitDailyConfigs =
                    data.RelatedUnitDailyConfigs.All()
                        .Where(x => x.RelatedUnitsDailyConfigId == config.Value).ToList();
                if (relatedUnitDailyConfigs.Count == relatedRelatedUnitDailyConfigs.Count
                    && relatedRelatedUnitDailyConfigs.Count == 0)
                {
                    continue;
                }

                foreach (var item in relatedUnitDailyConfigs)
                {
                    var key = item.UnitsDailyConfigId + "+" + item.RelatedUnitsDailyConfigId;
                    if (!relatedDailyRecords.ContainsKey(key))
                    {
                        relatedDailyRecords.Add(key, item.Position);
                    }
                }

                foreach (var item in relatedRelatedUnitDailyConfigs)
                {
                    var key = item.UnitsDailyConfigId + "+" + item.RelatedUnitsDailyConfigId;
                    if (!relatedDailyRecords.ContainsKey(key))
                    {
                        relatedDailyRecords.Add(key, item.Position);
                    }
                }
            }

            var dailyKeys = relatedDailyRecords.Keys;

            foreach (var key in dailyKeys)
            {
                var unitConfigs = key.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                var ucId = Convert.ToInt32(unitConfigs[0]);
                var rucId = Convert.ToInt32(unitConfigs[1]);
                if (unitDailyConfigsIndexes.ContainsKey(ucId))
                {
                    ucId = unitDailyConfigsIndexes[ucId];
                }

                if (unitDailyConfigsIndexes.ContainsKey(rucId))
                {
                    rucId = unitDailyConfigsIndexes[rucId];
                }

                var relatedUnitsDailyConfigRecord = new RelatedUnitDailyConfigs()
                {
                    UnitsDailyConfigId = ucId,
                    RelatedUnitsDailyConfigId = rucId,
                    Position = relatedDailyRecords[key]
                };

                Console.WriteLine($"{relatedUnitsDailyConfigRecord.UnitsDailyConfigId};{relatedUnitsDailyConfigRecord.RelatedUnitsDailyConfigId};{relatedDailyRecords[key]}");
                data.RelatedUnitDailyConfigs.Add(relatedUnitsDailyConfigRecord);
            }

            var status = data.SaveChanges("Coping");
        }

        private static void CopyRelatedUnitConfigs(IProductionData data)
        {
            var oldUnitConfigs =
                        data.UnitConfigs.All()
                            .Where(x => x.ProcessUnitId == 16 || x.ProcessUnitId == 17 && x.IsDeleted == false)
                            .ToDictionary(x => x.Code, x => x.Id);
            var newUnitConfigs = data.UnitConfigs.AllWithDeleted()
                .Where(x => x.ProcessUnitId == 52 || x.ProcessUnitId == 53)
                .ToDictionary(x => x.Code, x => x.Id);

            var unitCongigsIndexes = new Dictionary<int, int>();
            foreach (var config in oldUnitConfigs)
            {
                var newId = newUnitConfigs[config.Key];
                unitCongigsIndexes.Add(config.Value, newId);
            }
            Console.WriteLine($"CopyRelatedUnitConfigs->{oldUnitConfigs.Count}");
            Console.WriteLine($"CopyRelatedUnitConfigs->{newUnitConfigs.Count}");
            Console.WriteLine($"CopyRelatedUnitConfigs->{unitCongigsIndexes.Count}");

            var relatedRecords = new Dictionary<string, int>();

            foreach (var config in oldUnitConfigs)
            {
                var relatedUnitConfigs =
                    data.RelatedUnitConfigs.All()
                        .Where(x => x.UnitConfigId == config.Value).ToList();

                var relatedRelatedUnitConfigs =
                    data.RelatedUnitConfigs.All()
                        .Where(x => x.RelatedUnitConfigId == config.Value).ToList();
                if (relatedUnitConfigs.Count == relatedRelatedUnitConfigs.Count
                    && relatedRelatedUnitConfigs.Count == 0)
                {
                    continue;
                }

                foreach (var item in relatedUnitConfigs)
                {
                    var key = item.UnitConfigId + "+" + item.RelatedUnitConfigId;
                    if (!relatedRecords.ContainsKey(key))
                    {
                        relatedRecords.Add(key, item.Position);
                    }
                }

                foreach (var item in relatedRelatedUnitConfigs)
                {
                    var key = item.UnitConfigId + "+" + item.RelatedUnitConfigId;
                    if (!relatedRecords.ContainsKey(key))
                    {
                        relatedRecords.Add(key, item.Position);
                    }
                }
            }

            var keys = relatedRecords.Keys;

            foreach (var key in keys)
            {
                var unitConfigs = key.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                var ucId = Convert.ToInt32(unitConfigs[0]);
                var rucId = Convert.ToInt32(unitConfigs[1]);
                if (unitCongigsIndexes.ContainsKey(ucId))
                {
                    ucId = unitCongigsIndexes[ucId];
                }

                if (unitCongigsIndexes.ContainsKey(rucId))
                {
                    rucId = unitCongigsIndexes[rucId];
                }

                var relatedUnitConfigRecord = new RelatedUnitConfigs()
                {
                    UnitConfigId = ucId,
                    RelatedUnitConfigId = rucId,
                    Position = relatedRecords[key]
                };

                Console.WriteLine($"{relatedUnitConfigRecord.UnitConfigId};{relatedUnitConfigRecord.RelatedUnitConfigId};{relatedRecords[key]}");
                data.RelatedUnitConfigs.Add(relatedUnitConfigRecord);
            }

            var status = data.SaveChanges("Coping");
        }

        private static void ConfigAutoMapper()
        {
            //Mapper.CreateMap<Plant, PlantEx>()
            //.ForMember(p => p.NewId, opt => opt.Ignore());
        }

        private static void ProcessingPhdTankData(DataTable oPhdData, Dictionary<int, TankData> tanksData, Dictionary<string, int> tanksPhdConfigs, DateTime targetRecordDateTime, IProductionData data)
        {
            int confedence = 100;
            string tagName = string.Empty;
            decimal tagValue = 0m;

            DataRow row = null;

            var products = data.Products.All().Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            });

            for (int i = 0; i < oPhdData.Rows.Count; i++)
            {
                row = oPhdData.Rows[i];
                int tankId = tanksPhdConfigs[row["TagName"].ToString().Trim()];
                TankData tankData = tanksData[tankId];

                foreach (DataColumn dc in oPhdData.Columns)
                {
                    if (dc.ColumnName.Equals(value: "Tolerance")|| dc.ColumnName.Equals(value: "HostName")|| dc.ColumnName.Equals(value: "Units"))
                    {
                        continue;
                    }
                    else if (dc.ColumnName.Equals(value: "Confidence") /*&& !row[dc].ToString().Equals(value: "100")*/)
                    {
                        if (!row.IsNull(columnName: "Confidence"))
                        {
                            confedence = Convert.ToInt32(row[dc]);
                        }
                        else
                        {
                            confedence = 0;
                        }

                        //break;
                    }
                    else if (dc.ColumnName.Equals(value: "TagName"))
                    {
                        tagName = row[dc].ToString();
                    }
                    else if (dc.ColumnName.Equals(value: "TimeStamp"))
                    {
                        DateTime dt = !row.IsNull(columnName: "TimeStamp") ? Convert.ToDateTime(row[dc]) : DateTime.MinValue;
                        TimeSpan difference = targetRecordDateTime - dt; 
                        string currentTagName = !row.IsNull(columnName: "TagName")? row["TagName"].ToString() : string.Empty;
                        string currentTagValue = !row.IsNull(columnName: "Value")? row["Value"].ToString() : string.Empty;
                        if (difference.TotalMinutes > 120)
                        {
                            confedence = 0;
                        }
                    }
                    else if (dc.ColumnName.Equals(value: "Value"))
                    {
                        if (!row.IsNull(columnName: "Value"))
                        {
                            tagValue = Convert.ToDecimal(row[dc]);
                        }
                    }
                }

                if (confedence != 100)
                {
                    Console.WriteLine(row["TagName"]);
                //    continue;
                }

                if (tagName.Contains(value: ".PROD_ID"))
                {
                    int prId = Convert.ToInt32(tagValue);
                    TankMasterProduct product = data.TankMasterProducts.All().Where(x => x.TankMasterProductCode == prId).FirstOrDefault();
                    if (product != null)
                    {
                        tankData.ProductId = product.Id;
                        var pr = products.Where(p => p.Id == product.Id).FirstOrDefault();
                        if (pr != null)
                        {
                            tankData.ProductName = pr.Name;
                        }
                        else
                        {
                            tankData.ProductName = "N/A";
                        }
                    }
                    else
                    {
                        tankData.ProductId = Convert.ToInt32(tagValue);
                        tankData.ProductName = "N/A";
                    }
                }
                else if (tagName.EndsWith(value: ".LL")|| tagName.EndsWith(value: ".LEVEL_MM"))
                {
                    tankData.LiquidLevel = tagValue;
                }
                else if (tagName.EndsWith(value: ".LL_FWL") || tagName.EndsWith(value: ".LEVEL_FWL"))
                {
                    tankData.ProductLevel = tagValue;
                }
                else if (tagName.EndsWith(value: ".FWL"))
                {
                    tankData.FreeWaterLevel = tagValue;
                }
                else if (tagName.EndsWith(value: ".DREF") || tagName.EndsWith(value: ".REF_DENS"))
                {
                    tankData.ReferenceDensity = tagValue;
                }
                else if (tagName.EndsWith(value: ".NSV"))
                {
                    tankData.NetStandardVolume = tagValue;
                }
                else if (tagName.EndsWith(value: ".WIA"))
                {
                    tankData.WeightInAir = tagValue;
                }
                else if (tagName.EndsWith(value: ".WIV"))
                {
                    tankData.WeightInVacuum = tagValue;
                }

                tankData.Confidence = tankData.Confidence == 100 ? confedence : tankData.Confidence;
                tanksData[tankId] = tankData;
            }
        }

        private static void SetTankPhdConfigValue(string phdTagName, int tankId, Dictionary<string, int> tanksPhdConfigs)
        {
            if (!string.IsNullOrEmpty(phdTagName))
            {
                tanksPhdConfigs.Add(phdTagName, tankId);
            }
        }

        private static void CreateShiftData(IProductionData data, DateTime lastRealDate)
        {
            var shiftsData = data.UnitsData.All().Where(x => x.RecordTimestamp == lastRealDate).ToList();
            var approvedShiftsData = data.UnitsApprovedDatas.All().Where(x => x.RecordDate == lastRealDate).ToList();
            var nextDate = lastRealDate.AddDays(value: 1);
            var daysInMonth = DateTime.DaysInMonth(lastRealDate.Year, lastRealDate.Month);
            while (nextDate.Day <= daysInMonth)
            {
                var unitsDataList = new List<UnitsData>();
                var unitsApprovedDataList = new List<UnitsApprovedData>();

                foreach (var item in shiftsData)
                {

                    var record = new UnitsData
                    {
                        RecordTimestamp = nextDate,
                        ShiftId = item.ShiftId,
                        UnitConfigId = item.UnitConfigId,
                        Value = (decimal) item.RealValue,
                        UnitsManualData = null,
                        UnitConfig = item.UnitConfig,
                        UnitEnteredForCalculationData = null,
                        Confidence = 100
                    };
                    unitsDataList.Add(record);
                }

                foreach (var item in approvedShiftsData)
                {

                    var record = new UnitsApprovedData
                    {
                        ShiftId = item.ShiftId,
                        RecordDate = nextDate,
                        ProcessUnitId = item.ProcessUnitId,
                        Approved = true
                    };
                    unitsApprovedDataList.Add(record);
                }

                data.UnitsData.BulkInsert(unitsDataList, userName: "InitialLoading");
                data.UnitsApprovedDatas.BulkInsert(unitsApprovedDataList, userName: "InitialLoading");

                IEfStatus result = data.SaveChanges(userName: "InitialLoading");
                if (result.IsValid)
                {
                    Console.WriteLine(format: "Successfully added records for {0}", arg0: nextDate);
                }

                nextDate = nextDate.AddDays(value: 1);
                if (nextDate.Month != lastRealDate.Month)
                {
                    break;
                }
            }
        }

        private static void CreateDailyData(IProductionData data, DateTime lastRealDate)
        {
            var dailyData = data.UnitsDailyDatas.All().Where(x => x.RecordTimestamp == lastRealDate).ToList();
            var approvedDaysData = data.UnitsApprovedDailyDatas.All().Where(x => x.RecordDate == lastRealDate).ToList();
            var nextDate = lastRealDate.AddDays(1);
            var daysInMonth = DateTime.DaysInMonth(lastRealDate.Year, lastRealDate.Month);
            while (nextDate.Day < daysInMonth)
            {
                var unitsDailyDataList = new List<UnitsDailyData>();
                var unitsApprovedDailyDataList = new List<UnitsApprovedDailyData>();


                foreach (var item in dailyData)
                {
                    var record = new UnitsDailyData
                    {
                        RecordTimestamp = nextDate,
                        UnitsDailyConfigId = item.UnitsDailyConfigId,
                        Value = (decimal) item.RealValue,
                        UnitsManualDailyData = null,
                        UnitsDailyConfig = item.UnitsDailyConfig
                    };
                    unitsDailyDataList.Add(record);
                }

                foreach (var item in approvedDaysData)
                {
                    var record = new UnitsApprovedDailyData
                    {
                        RecordDate = nextDate,
                        ProcessUnitId = item.ProcessUnitId,
                        Approved = true,
                        EnergyApproved = true
                    };
                    unitsApprovedDailyDataList.Add(record);
                }

                data.UnitsDailyDatas.BulkInsert(unitsDailyDataList, "InitialLoading");
                data.UnitsApprovedDailyDatas.BulkInsert(unitsApprovedDailyDataList, "InitialLoading");
                Console.WriteLine("Successfully added daily records for {0}", nextDate);

                nextDate = nextDate.AddDays(1);
                if (nextDate.Month != lastRealDate.Month)
                {
                    break;
                }
            }
        }

        private static void CreateProductionPlanData(IProductionData data, DateTime lastRealDate)
        {
            var dailyData = data.ProductionPlanDatas.All().Where(x => x.RecordTimestamp == lastRealDate).ToList();
            var nextDate = lastRealDate.AddDays(1);
            var daysInMonth = DateTime.DaysInMonth(lastRealDate.Year, lastRealDate.Month);
            while (nextDate.Day < daysInMonth)
            {
                foreach (var item in dailyData)
                {
                    var record = item;
                    record.RecordTimestamp = nextDate;
                    data.ProductionPlanDatas.Add(record);
                }

                var result = data.SaveChanges(userName: "InitialLoading");
                if (result.IsValid)
                {
                    Console.WriteLine("Successfully added {0} records for {1}", result.ResultRecordsCount, nextDate);
                }
                else
                {
                    Console.WriteLine("error");
                }

                nextDate = nextDate.AddDays(1);
            }
        }

        private static void AddOrUpdateProductionPlanConfigs(ProductionData data)
        {
            //// update all exsisting ProductionPlanData to become daily data
            //UpdateInProductionPlanForExsisting(data);

            //// Insert quantity plan value and update QuantityPlanFormula
            //InsertPlanValueAndUpdateQuantityPlanFormula(data);

            //// Update PlanValues
            //UpdatePlanValues(data);

            //// Update PlanValues
            //UpdatePlanNorms(data);

            //// Second updating of the QuantityPlanFormula
            //UpdatingQuantityPlanFormula(data);

            //// Loading usage rate value for the energy products
            //LoadingUsageRateForEnergyProducts(data);

            //// Thirth updating of the QuantityPlanFormula for the energy products
            //UpdateQuantityPlanFormulaForTheEnergy(data);

            //var p = data.ProductionPlanConfigs.All().ToList();
            //foreach (var productionPlanConfig in p)
            //{
            //    Console.WriteLine("{0}:{1} - > {2}",
            //         productionPlanConfig.Code,
            //         productionPlanConfig.Name,
            //         productionPlanConfig.QuantityPlanFormula);
            //    productionPlanConfig.QuantityPlanFormula = productionPlanConfig.QuantityPlanFormula.Replace("p.PNPNPN", "p.pn");
            //    productionPlanConfig.QuantityPlanFormula = productionPlanConfig.QuantityPlanFormula.Replace("p.PVPVPV", "p.pv");
            //}

            //var status = data.SaveChanges("Initial Loading");
            //Console.WriteLine(status.ResultRecordsCount);
        }

        private static void UpdateQuantityPlanFormulaForTheEnergy(ProductionData data)
        {
            var p = data.ProductionPlanConfigs.All().Where(x => x.MaterialTypeId != 1).ToList();
            foreach (var productionPlanConfig in p)
            {
                var endIndex = productionPlanConfig.QuantityPlanFormula.IndexOf("*");
                if (endIndex == -1)
                {
                    continue;
                }
                var beginIndex = productionPlanConfig.QuantityPlanFormula.StartsWith("(") == false ? 0 : 1;
                var value = productionPlanConfig.QuantityPlanFormula.Substring(beginIndex, endIndex - beginIndex);
                Console.WriteLine("{0}:{1} - > {2} -> {3}",
                    productionPlanConfig.Code,
                    productionPlanConfig.Name,
                    productionPlanConfig.QuantityPlanFormula,
                    value);
                productionPlanConfig.QuantityPlanFormula = productionPlanConfig.QuantityPlanFormula.Replace(value, "p.pn");
            }

            var status = data.SaveChanges("Initial Loading");
            Console.WriteLine(status.ResultRecordsCount);
        }

        private static void LoadingUsageRateForEnergyProducts(ProductionData data)
        {
            var p = data.ProductionPlanConfigs.All().Where(x => x.MaterialTypeId != 1).ToList();
            foreach (var productionPlanConfig in p)
            {
                Console.WriteLine("{0}:{1} - > {2}", productionPlanConfig.Code, productionPlanConfig.Name, productionPlanConfig.Percentages);
                data.PlanNorms.Add(new PlanNorm
                {
                    ProductionPlanConfigId = productionPlanConfig.Id,
                    Month = new DateTime(2016, 6, 1, 0, 0, 0),
                    Value = 0
                });
            }

            var status = data.SaveChanges("Initial Loading");
            Console.WriteLine(status.ResultRecordsCount);
        }

        private static void UpdatingQuantityPlanFormula(ProductionData data)
        {
            var p = data.ProductionPlanConfigs.All().Where(x => x.MaterialTypeId == 1).ToList();
            foreach (var productionPlanConfig in p)
            {
                var beginIndex = productionPlanConfig.QuantityPlanFormula.IndexOf("((");
                if (beginIndex == -1)
                {
                    continue;
                }

                var endIndex = productionPlanConfig.QuantityPlanFormula.IndexOf("/", beginIndex);
                if (endIndex == -1)
                {
                    continue;
                }

                var value = productionPlanConfig.QuantityPlanFormula.Substring(beginIndex + 2, endIndex - beginIndex - 2);
                Console.WriteLine("{0} -> {1}", productionPlanConfig.QuantityPlanFormula, value);
                productionPlanConfig.QuantityPlanFormula = productionPlanConfig.QuantityPlanFormula.Replace(value, "p.pn");
                productionPlanConfig.QuantityPlanFormula = productionPlanConfig.QuantityPlanFormula.Replace("100", "100.00");
            }

            data.SaveChanges("Initial Loading");
        }

        private static void UpdatePlanNorms(ProductionData data)
        {
            var date = new DateTime(2016, 5, 1, 0, 0, 0);
            var productionPlanConfigs = data.ProductionPlanConfigs.All().Where(x => x.MaterialTypeId == 1).ToList();
            foreach (var productionPlanConfig in productionPlanConfigs)
            {
                var productionPlanConfigId = productionPlanConfig.Id;
                var planNorm = data.PlanNorms.All()
                    .Where(x => x.ProductionPlanConfigId == productionPlanConfigId
                        && x.Month == date)
                    .FirstOrDefault();

                if (planNorm != null)
                {
                    planNorm.Value = productionPlanConfig.Percentages;
                    data.PlanNorms.Update(planNorm);
                    data.SaveChanges("Initial Loading");
                }
            }

            data.SaveChanges("Initial Loading");
        }

        private static void UpdatePlanValues(ProductionData data)
        {
            var planValues = new SortedDictionary<int, decimal>();
            var productionPlanConfigs = data.ProductionPlanConfigs.All().Where(x => x.MaterialTypeId == 1).ToList();
            foreach (var productionPlanConfig in productionPlanConfigs)
            {
                var processUnitId = productionPlanConfig.ProcessUnitId;
                if (!planValues.ContainsKey(processUnitId) && productionPlanConfig.QuantityPlan.HasValue)
                {
                    planValues.Add(processUnitId, productionPlanConfig.QuantityPlan.Value);
                }
            }

            var date = new DateTime(2016, 5, 1, 0, 0, 0);
            foreach (var planValue in planValues)
            {
                Console.WriteLine("{0} -> {1}", planValue.Key, planValue.Value);
                var planValueV = data.PlanValues.All().Where(x => x.ProcessUnitId == planValue.Key && x.Month == date).FirstOrDefault();
                if (planValueV != null)
                {
                    planValueV.Value = planValue.Value;
                    data.PlanValues.Update(planValueV);
                }
            }

            data.SaveChanges("Initial Loading");
        }

        private static void InsertPlanValueAndUpdateQuantityPlanFormula(ProductionData data)
        {
            var productionPlanConfigs = data.ProductionPlanConfigs.All().Where(x => x.MaterialTypeId == 1).ToList();
            foreach (var productionPlanConfig in productionPlanConfigs)
            {
                var beginIndex = productionPlanConfig.QuantityPlanFormula.IndexOf("*");
                if (beginIndex == -1)
                {
                    continue;
                }

                var endIndex = productionPlanConfig.QuantityPlanFormula.IndexOf(")", beginIndex);
                if (endIndex == -1)
                {
                    continue;
                }

                var value = productionPlanConfig.QuantityPlanFormula.Substring(beginIndex + 1, endIndex - beginIndex - 1);
                Console.WriteLine("{0} -> {1}", productionPlanConfig.QuantityPlanFormula, value);
                productionPlanConfig.QuantityPlan = decimal.Parse(value);
                productionPlanConfig.QuantityPlanFormula = productionPlanConfig.QuantityPlanFormula.Replace(value, "p.pv");
            }

            data.SaveChanges("Initial Loading");
        }

        private static void UpdateInProductionPlanForExsisting(ProductionData data)
        {
            // update all exsisting ProductionPlanData to become daily data
            var productionPlanConfigs = data.ProductionPlanConfigs.AllWithDeleted().ToList();
            foreach (var productionPlanConfig in productionPlanConfigs)
            {
                productionPlanConfig.IsPropductionPlan = true;
                data.SaveChanges("Initial Loading");
            }

            Console.WriteLine("Successfully UpdateInProductionPlanForExsisting");
        }

        private static void UpdateShiftUnitData(IKernel kernel, ProductionData data)
        {
            var service = kernel.Get<IPhdPrimaryDataService>();

            var unitConfigs = data.UnitConfigs
                                  .All()
                                  .Include(x => x.RelatedUnitConfigs)
                                  .Include(x => x.RelatedUnitConfigs.Select(y => y.UnitConfig))
                                  .Include(x => x.RelatedUnitConfigs.Select(z => z.RelatedUnitConfig).Select(w => w.UnitDatasTemps))
                                  .Where(x => x.CollectingDataMechanism == "C")
                                  .ToList();
            var targerDay = new DateTime(2016, 5, 25);
            var unitsTemp = data.UnitsData.All().Include(x => x.UnitConfig).Where(x => x.RecordTimestamp == targerDay &&
                                                                                       x.ShiftId == 1 &&
                                                                                       x.UnitConfig.CollectingDataMechanism != "C").ToList().Select(x => new UnitDatasTemp
                                                                                       {
                                                                                           RecordTimestamp = x.RecordTimestamp,
                                                                                           UnitConfigId = x.UnitConfigId,
                                                                                           ShiftId = x.ShiftId,
                                                                                           Value = x.Value,
                                                                                           UnitConfig = x.UnitConfig,
                                                                                           Confidence = x.Confidence
                                                                                       }).ToList();

            int test = 0;

            var result = service.ProcessCalculatedUnits(unitConfigs, targerDay, 1, unitsTemp, ref test, true)
                                .Select(unitDataTemp => new UnitsData()
                                {
                                    RecordTimestamp = unitDataTemp.RecordTimestamp,
                                    UnitConfigId = unitDataTemp.UnitConfigId,
                                    ShiftId = unitDataTemp.ShiftId,
                                    Value = unitDataTemp.Value,
                                    Confidence = unitDataTemp.Confidence
                                })
                                .ToDictionary(x => new { RecordTimestamp = x.RecordTimestamp, UnitConfigId = x.UnitConfigId, ShiftId = x.ShiftId });

            var neededUnitData = data.UnitsData
                                     .All()
                                     .Include(x => x.UnitConfig)
                                     .Where(x => x.RecordTimestamp == targerDay &&
                                                 x.ShiftId == 1 &&
                                                 x.UnitConfig.CollectingDataMechanism == "C" &&
                                                 x.UnitConfig.IsDeleted == false);

            foreach (var unitData in neededUnitData)
            {
                unitData.Value = result[new { RecordTimestamp = unitData.RecordTimestamp, UnitConfigId = unitData.UnitConfigId, ShiftId = unitData.ShiftId }].Value;
                unitData.Confidence = result[new { RecordTimestamp = unitData.RecordTimestamp, UnitConfigId = unitData.UnitConfigId, ShiftId = unitData.ShiftId }].Confidence;
            }
            try
            {
                data.SaveChanges("Initial Loading");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //foreach (var item in neededUnitData)
            //{
            //    Console.WriteLine(item.Value + " " + item.Confidence);    
            //}
        }

        private static void WritePositionsConfidence(IProductionData data)
        {
            PHDHistorian phd = new PHDHistorian();
            try
            {
                PHDServer server = new PHDServer("srv-vm-mes-phd", SERVERVERSION.RAPI200);
                phd.DefaultServer = server;
                phd.DefaultServer.Port = 3150;
                phd.Sampletype = SAMPLETYPE.Raw;
                phd.ReductionType = REDUCTIONTYPE.None;
                phd.StartTime = "4/25/2016 5:15:00 AM";
                phd.EndTime = "4/25/2016 5:15:00 AM";
                phd.MaximumRows = 1;

                using (var writer = new StreamWriter(@"C:\Temp\srv-vm-mes-phd-25042016-0515.log"))
                {
                    var unitConfigs = data.UnitConfigs.All().Include(x => x.ProcessUnit).Where(x => x.DataSource == PrimaryDataSourceType.SrvVmMesPhdA).ToList();
                    foreach (var unitConfig in unitConfigs)
                    {
                        if (unitConfig.CollectingDataMechanism == "A")
                        {
                            var tag = unitConfig.PreviousShiftTag;
                            DataSet dsGrid = phd.FetchRowData(tag);
                            foreach (DataRow row in dsGrid.Tables[0].Rows)
                            {
                                string tagHeaderLine = string.Format("{0};{1};{2};{3};{4};",
                                                                     unitConfig.ProcessUnit.FullName,
                                                                     unitConfig.Code,
                                                                     unitConfig.Name,
                                                                     unitConfig.Position,
                                                                     tag);
                                writer.Write(tagHeaderLine);
                                foreach (DataColumn dc in dsGrid.Tables[0].Columns)
                                {
                                    writer.Write(row[dc] + ";");
                                }

                                writer.WriteLine();
                            }
                        }
                        Console.WriteLine(unitConfig.PreviousShiftTag);
                    }
                }

            }
            catch (PHDErrorException phdException)
            {
                Console.WriteLine("PHD ERROR: " + phdException.ToString());
            }
            catch (Exception exception)
            {
                Console.WriteLine("EXCEPTION: " + exception.ToString());
            }
            finally
            {
                phd.Dispose();
            }
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
        //    
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

                //ToDo: On shifts changed to 2 must repair this code 
                var result = data.Select(x => new MultiShift
                {
                    TimeStamp = x.RecordTimestamp,
                    Code = x.UnitConfig.Code,
                    Position = x.UnitConfig.Position,
                    UnitConfigId = x.UnitConfigId,
                    UnitName = x.UnitConfig.Name,
                    Shift1 = data.Where(y => y.RecordTimestamp == dateParam && y.ShiftId == (int)ShiftType.First).Where(u => u.UnitConfigId == x.UnitConfigId).FirstOrDefault(),
                    Shift2 = data.Where(y => y.RecordTimestamp == dateParam && y.ShiftId == (int)ShiftType.Second).Where(u => u.UnitConfigId == x.UnitConfigId).FirstOrDefault(),
                    Shift3 = data.Where(y => y.RecordTimestamp == dateParam && y.ShiftId == (int)ShiftType.Third).Where(u => u.UnitConfigId == x.UnitConfigId).FirstOrDefault(),
                }).Distinct(new MultiShiftComparer()).ToList();

                Console.WriteLine($"Estimated Time For Action: {timer.Elapsed}");

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.TimeStamp} {item.UnitName} {item.Shift1.RealValue} {item.Shift2.RealValue} {item.Shift3.RealValue} {item.Shift1.RealValue + item.Shift2.RealValue + item.Shift3.RealValue}");
                }

                Console.WriteLine($"Estimated Time For Action: {timer.Elapsed}");
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
                if (ts.TotalMinutes > 2 /*&& ts.Hours == 0*/)
                {
                    using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(), new Logger())))
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
            catch (Exception ex)
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
                            if (decimal.TryParse(valueRow["Value"].ToString(), out value))
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
                            if (decimal.TryParse(valueRow["Value"].ToString(), out value))
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
                    if (ts.TotalMinutes > 2 /*&& ts.Hours == 0*/)
                    {
                        var phdValues = new Dictionary<int, long>();

                        using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(), new Logger())))
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }

        internal static void ProcessTransactionsData()
        {
            try
            {
                Console.WriteLine("Begin synchronization!");

                using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(), new Logger())))
                {
                    long max = 573846;
                    {
                        GetTransactionsFromAso(max, context);
                    }

                    Console.WriteLine("End synchronization");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }

        private static List<MeasuringPointsConfigsData> GetTransactionsFromAso(long minSequenceNumber, ProductionData context)
        {
            var transactions = new List<MeasuringPointsConfigsData>();
            Console.WriteLine(string.Format("Last processing SequenceNumber was {0}", minSequenceNumber));
            var maximumLastFetchSequenceNumber = minSequenceNumber;
            var adapter = new AsoDataSetTableAdapters.flow_MeasuringPointsDataTableAdapter();
            var table = new AsoDataSet.flow_MeasuringPointsDataDataTable();
            adapter.Fill(table, maximumLastFetchSequenceNumber);
            if (table.Rows.Count > 0)
            {
                var mesurinpPointsByTransactions = context.MeasuringPointConfigs.All().Where(x => x.IsUsedPhdTotalizers == true).Select(x => x.Id).ToList();

                foreach (AsoDataSet.flow_MeasuringPointsDataRow row in table.Rows)
                {
                    try
                    {
                        if (row.SequenceNumber == 576866)
                        {
                            var x = 0;
                            x++;
                        }


                        if (mesurinpPointsByTransactions.Contains(row.MeasuringPointId))
                        {
                            continue;
                        }

                        if (row.RowId != -1)
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
                            tr.AverageObservableDensity = row.AverageObservableDensity > 999999 ? 0 : row.AverageObservableDensity;
                        }
                        if (!row.IsAverageReferenceDensityNull())
                        {
                            tr.AverageReferenceDensity = row.AverageReferenceDensity > 999999 ? 0 : row.AverageReferenceDensity;
                        }
                        if (!row.IsAverageTemperatureNull())
                        {
                            tr.AverageTemperature = row.AverageTemperature > 999999 ? 0 : row.AverageTemperature;
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

                        var isExsistingTransaction = context.MeasuringPointsConfigsDatas.All()
                            .Any(x => x.MeasuringPointConfigId == tr.MeasuringPointConfigId
                            && x.TransactionBeginTime == tr.TransactionBeginTime
                            && x.TransactionEndTime == tr.TransactionEndTime
                            && x.TransactionNumber == tr.TransactionNumber
                            && x.RowId == tr.RowId);
                        if (!isExsistingTransaction)
                        {
                            context.MeasuringPointsConfigsDatas.Add(tr);
                            var status = context.SaveChanges("Aso2SapoLoader");
                            if (status.IsValid)
                            {
                                Console.WriteLine(string.Format("Processing sequence number {0}", row.SequenceNumber));
                            }
                            else
                            {
                                Console.WriteLine(string.Format("Processing sequence number {0} failed {1}",
                                    row.SequenceNumber, status.EfErrors[0].ErrorMessage));
                            }
                        }

                    }
                    catch (Exception dbException)
                    {
                        Console.WriteLine(dbException.Message);
                    }

                }
            }

            return transactions;
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

                    var fomatedDate = DateTime.Now.ToString("M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    oPhd.StartTime = fomatedDate;
                    oPhd.EndTime = fomatedDate;

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

                    //oPhd.StartTime = endPhdTimestamp;
                    //oPhd.EndTime = endPhdTimestamp;
                    var result = oPhd.FetchRowData(tags[0]);
                    var row = result.Tables[0].Rows[0];
                    var endValue = Convert.ToInt64(row["Value"]);
                    Console.WriteLine(row["Timestamp"]);

                    result = oPhd.FetchRowData(tags[1]);
                    row = result.Tables[0].Rows[0];
                    var pressure = Convert.ToDecimal(row["Value"]);

                    //oPhd.StartTime = beginPhdTimestamp;
                    //oPhd.EndTime = beginPhdTimestamp;
                    result = oPhd.FetchRowData(tags[0]);
                    row = result.Tables[0].Rows[0];
                    var beginValue = Convert.ToInt64(row["Value"]);

                    var value = ((endValue - beginValue) * pressure) / Convert.ToDecimal(tags[2]);
                    Console.WriteLine(value);
                }
            }

        }

        internal static void ProcessProductionReportTransactionsData()
        {
            try
            {
                Console.WriteLine("Begin production report data synchronization!");
                {
                    var now = DateTime.Now;
                    var today = DateTime.Today.AddDays(-3);
                    var fiveOClock = new DateTime(today.Year, today.Month, today.Day, 4, 30, 0);

                    var ts = now - fiveOClock;
                    //if(ts.TotalMinutes > 4 && ts.Hours == 0)
                    {
                        var phdValues = new Dictionary<int, long>();

                        using (var context = new ProductionData(new CollectingDataSystemDbContext(new AuditablePersister(), new Logger())))
                        {
                            //if (!context.MeasurementPointsProductsDatas.All().Where(x => x.RecordTimestamp >= today).Any())
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
                                    .Where(m => m.IsUsedInProductionReport == true)
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
                                        }
                                    }
                                }

                                foreach (KeyValuePair<int, long> entry in phdValues)
                                {
                                    if (entry.Value > 0)
                                    {
                                        var scaleProduct = scaleMeasuringPointProducts.FirstOrDefault(x => x.Id == entry.Key);
                                        var measuringPointProductData = new MeasuringPointProductsData
                                        {
                                            MeasuringPointConfigId = scaleProduct.MeasuringPointConfigId,
                                            RecordTimestamp = today,
                                            Value = entry.Value,
                                            ProductId = scaleProduct.ProductId,
                                            DirectionId = scaleProduct.DirectionId
                                        };

                                        Console.WriteLine(string.Format("Processing virtual transaction for {0} {1} {2} {3}", entry.Key, scaleProduct.PhdProductTotalizerTag, entry.Value, scaleProduct.Direction.Name));
                                        context.MeasurementPointsProductsDatas.Add(measuringPointProductData);
                                    }
                                }

                                context.SaveChanges("Phd2SqlTotalizer");
                            }
                        }
                    }
                }

                Console.WriteLine("End roduction report data synchronization!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class MonthlyTechnicalReportData
    {
        /// <summary>
        /// HEADER
        /// </summary>
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Factory { get; set; }
        public string ProcessUnit { get; set; }
        public string MaterialType { get; set; }
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// PLAN MONTH
        /// </summary>
        public decimal PlanValue { get; set; }
        public decimal PlanPercentage { get; set; }

        /// <summary>
        /// FACT MONTH
        /// </summary>
        public decimal FactValue { get; set; }
        public decimal FactPercentage { get; set; }
        public decimal FactValueDifference { get; set; }
        public decimal FactPercentageDifference { get; set; }

        /// <summary>
        /// FACT YEAR
        /// </summary>
        public decimal YearValue { get; set; }
        public decimal YearPercentage { get; set; }
        public decimal YearValueDifference { get; set; }
        public decimal YearPercentageDifference { get; set; }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};;{7};{8};;{9};{10};{11};{12};;{13};{14};{15};{16}",
                    Id,
                    Code,
                    Name,
                    Factory,
                    ProcessUnit,
                    MaterialType,
                    MeasurementUnit,

                    PlanValue,
                    PlanPercentage,

                    FactValue,
                    FactPercentage,
                    FactValueDifference,
                    FactPercentageDifference,

                    YearValue,
                    YearPercentage,
                    YearValueDifference,
                    YearPercentageDifference
                );
        }
    }

    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T) formatter.Deserialize(stream);
            }
        }
    }

}
