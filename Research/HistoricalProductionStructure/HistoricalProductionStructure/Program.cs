﻿
using System.Collections.Generic;
using Domain.Models;

namespace HistoricalProductionStructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Transactions;
    using DataAccess;
    using DataAccess.Common;
    using DataAccess.Concrete;
    using DataAccess.Contracts;
    using Domain.Models;

    class Program
    {
        static void Main(string[] args)
        {
            using (var data = new ProductionData(new AppDbContext(new AuditablePersister())))
            {
                var timer = new Stopwatch();
                var times = new TimeSpan[4];
               // TransferHo1AndHo3(data);

                timer.Start();
                var factories = GetActualFactories(data, new DateTime(2017, 1, 12)).ToList();
                PrintFactories(factories, new DateTime(2017, 1, 12));
                timer.Stop();
                times[0] = timer.Elapsed;
                timer.Reset();
                Console.WriteLine();

                timer.Start();
                factories = GetActualFactories(data, new DateTime(2017, 3, 2)).ToList();
                PrintFactories(factories, new DateTime(2017, 3, 2));
                timer.Stop();
                times[1] = timer.Elapsed;
                timer.Reset();
                Console.WriteLine();

           //     CreateProcessUnitHistoryLoad(data);

                timer.Start();
                factories = GetActualFactories(data, new DateTime(2017, 1, 12)).ToList();
                PrintFactories(factories, new DateTime(2017, 1, 12));
                timer.Stop();
                times[2] = timer.Elapsed;
                timer.Reset();

                Console.WriteLine();
                timer.Start();
                factories = GetActualFactories(data, new DateTime(2017, 3, 2)).ToList();
                PrintFactories(factories, new DateTime(2017, 3, 2));
                timer.Start();
                times[3] = timer.Elapsed;

                Console.WriteLine();

                Console.WriteLine($"Test summary:\n{"".PadLeft(20,'-')}");

                for (int i = 0; i < times.Length; i++)
                {
                    Console.WriteLine($"Estimated time for test #{i + 1}:{times[i]}"); 
                }


                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

        }

        private static void CreateProcessUnitHistoryLoad(ProductionData data)
        {
            var puHistory =
                data.ProcessUnitToFactoryHistory.All().Include(x=>x.Factory.Plant).Where(x => x.ActivationDate == new DateTime(2017, 1, 1)).ToList();

            var date = new DateTime(1977,1,1);
            var records = new List<ProcessUnitToFactoryHistory>();
            for (int i = 0; i < 480; i++)
            {
                foreach (var historyRecord in puHistory)
                {
                    var newRecord = new ProcessUnitToFactoryHistory()
                    {
                        ProcessUnitId = historyRecord.ProcessUnitId,
                        ActivationDate = date,
                        ProcessUnitShortName = historyRecord.ProcessUnitShortName,
                        ProcessUnitFullName = historyRecord.ProcessUnitFullName,
                        FactoryId = historyRecord.FactoryId,
                        FactoryShortName = historyRecord.FactoryShortName,
                        FactoryFullName = historyRecord.FactoryFullName,
                        PlantId = historyRecord.PlantId,
                        PlantShortName = historyRecord.PlantShortName,
                        PlantFullName = historyRecord.PlantFullName,

                    };

                    records.Add(newRecord);
                }

                date = date.AddMonths(1);
            }

            data.ProcessUnitToFactoryHistory.BulkInsert(records, "Load Test");
            data.SaveChanges("Load Test");
        }

        private static IEnumerable<Factory> GetActualFactories(ProductionData data, DateTime targetDate)
        {
            var records = data.ProcessUnitToFactoryHistory.All().Include(x=>x.Factory).Include(x=>x.ProcessUnit)
                .Where(x => x.ActivationDate <= targetDate)
                .GroupBy(x => x.ProcessUnitId)
                .SelectMany(x => x.Where(y => y.ActivationDate == x.Max(z => z.ActivationDate))).OrderBy(x=>x.ProcessUnitId);

            var jRecords = records.Join(data.ProcessUnits.All().Include(self=>self.FactoryHistories),
                recs => recs.ProcessUnitId,
                pu => pu.Id,
                (recs, pu) => new { recs, pu});


            var factories = new Dictionary<int, Factory>();


            foreach (var historyRecord in jRecords)
            {
                var newHistoryProcessUnit = historyRecord.pu;
                newHistoryProcessUnit.ShortName = historyRecord.recs.ProcessUnitShortName;
                newHistoryProcessUnit.FullName = historyRecord.recs.ProcessUnitFullName;

                if (!factories.ContainsKey(historyRecord.recs.FactoryId))
                {
                    var newFactory = new Factory()
                    {
                        Id = historyRecord.recs.FactoryId,
                        ShortName = historyRecord.recs.FactoryShortName,
                        FullName = historyRecord.recs.FactoryFullName
                    };
                    newFactory.ProcessUnits.Add(newHistoryProcessUnit);

                    factories.Add(newFactory.Id, newFactory);
                }
                else
                {
                    factories[historyRecord.recs.FactoryId].ProcessUnits.Add(newHistoryProcessUnit);
                }
            }

            return factories.Select(x => x.Value).OrderBy(x=>x.Id);
            //new ProcessUnit()
            //{
            //    Id = x.Id,
            //    ShortName = x.FactoryHistories. ShortName,
            //    FullName = x.FullName,
            //    FactoryHistories = new List<ProcessUnitToFactoryHistory>(),
            //    Factory = x.FactoryHistories
            //        .Where(z => z.ActivationDate <= targetDate && z.IsDeleted == false)
            //        .OrderByDescending(y => y.ActivationDate)
            //        .FirstOrDefault()?.Factory ?? new Factory()
            //}
            //);



        }

        private static void PrintProcessUnits(IEnumerable<ProcessUnit> processUnits, DateTime date)
        {
            Console.WriteLine($"Инсталаци в ЛНХБ към: {date:dd-MM-yyyy} г.");
            Console.WriteLine("".PadRight(30, '-'));
            var pU = processUnits.OrderBy(x => x.Factory.Id).ToList();
            var factory = pU.FirstOrDefault()?.Factory;
            var factoryName = factory?.ShortName;
            Console.WriteLine();
            Console.WriteLine($"{factory?.Plant?.Id:d2} - {factory?.Plant?.ShortName.PadRight(5)} - {factory?.Plant?.FullName}");
            Console.WriteLine();
            Console.WriteLine($"\t{factory?.Id:d2} - {factory?.ShortName.PadRight(11)} - {factory?.FullName}");
            foreach (var processUnit in pU)
            {
                if (processUnit.Factory.ShortName != factoryName)
                {
                    factoryName = processUnit.Factory.ShortName;
                    Console.WriteLine();
                    Console.WriteLine($"\t{ processUnit.Factory?.Id:d2} - { processUnit.Factory?.ShortName.PadRight(11)} - { processUnit.Factory?.FullName}");
                }
                Console.WriteLine(
                    $"\t\t{processUnit.Id:d2} - {processUnit.ShortName.PadRight(16)} - {processUnit.FullName}");
            }
        }

        private static void PrintFactories(List<Factory> factories, DateTime targeTime)
        {
            Console.WriteLine($"Инсталаци в ЛНХБ към: {targeTime:dd-MM-yyyy} г.");
            Console.WriteLine("".PadRight(30, '-'));

            foreach (var factory in factories)
            {
                Console.WriteLine();
                Console.WriteLine($"\t{ factory.Id:d2} - { factory.ShortName.PadRight(11)} - { factory.FullName}");

                foreach (var processUnit in factory.ProcessUnits)
                {
                    Console.WriteLine($"\t\t{processUnit.Id:d2} - {processUnit.ShortName.PadRight(16)} - {processUnit.FullName}"); 
                }
            }
        }

        private static IEnumerable<ProcessUnit> GetActualProcessUnits(ProductionData data, DateTime targetDate)
        {
            return data.ProcessUnits.All().Include(x => x.FactoryHistories).ToList()
            .Select(x => FromProcessUnitHistory(x.FactoryHistories.AsQueryable(), targetDate));
            //new ProcessUnit()
            //{
            //    Id = x.Id,
            //    ShortName = x.FactoryHistories. ShortName,
            //    FullName = x.FullName,
            //    FactoryHistories = new List<ProcessUnitToFactoryHistory>(),
            //    Factory = x.FactoryHistories
            //        .Where(z => z.ActivationDate <= targetDate && z.IsDeleted == false)
            //        .OrderByDescending(y => y.ActivationDate)
            //        .FirstOrDefault()?.Factory ?? new Factory()
            //}
            //);
        }

        public static ProcessUnit FromProcessUnitHistory(IQueryable<ProcessUnitToFactoryHistory> history, DateTime targetDate)
        {
            var historicalRecord = history.Where(z => z.ActivationDate <= targetDate && z.IsDeleted == false)
                .OrderByDescending(y => y.ActivationDate)
                .FirstOrDefault();
            if (historicalRecord != null)
            {
                return new ProcessUnit()
                {
                    Id = historicalRecord.ProcessUnit.Id,
                    ShortName = historicalRecord.ProcessUnit.ShortName,
                    FullName = historicalRecord.ProcessUnit.FullName,
                    Factory = new Factory()
                    {
                        Id = historicalRecord.FactoryId,
                        ShortName = historicalRecord.FactoryShortName,
                        FullName = historicalRecord.FactoryFullName,
                        Plant = new Plant()
                        {
                            Id = historicalRecord.PlantId,
                            ShortName = historicalRecord.PlantShortName,
                            FullName = historicalRecord.FactoryFullName
                        }
                    },
                    FactoryHistories = new List<ProcessUnitToFactoryHistory>(),
                };
            }
            else
            {
                return new ProcessUnit();
            }
        }

        private static void TransferHo1AndHo3(ProductionData data)
        {
            var processUnits = data.ProcessUnits.All().Where(x => x.ShortName == "ХО-1" || x.ShortName == "ХО-3").ToList();
            var newFactory = data.Factories.All().Include(x => x.Plant).FirstOrDefault(x => x.Id == 4);
            if (processUnits.Any() && newFactory != null)
            {
                var activationData = new DateTime(2017, 2, 1);
                foreach (var processUnit in processUnits)
                {
                    using (
                        var transaction = new TransactionScope(TransactionScopeOption.Required,
                            DefaultTransactionOptions.Instance.TransactionOptions))
                    {
                        processUnit.FactoryId = 4;
                        data.ProcessUnitToFactoryHistory.Add(new ProcessUnitToFactoryHistory()
                        {
                            ProcessUnitId = processUnit.Id,
                            ProcessUnitShortName = processUnit.ShortName,
                            ProcessUnitFullName = processUnit.FullName,
                            FactoryId = newFactory.Id,
                            FactoryShortName = newFactory.ShortName,
                            FactoryFullName = newFactory.FullName,
                            PlantId = newFactory.Plant.Id,
                            PlantShortName = newFactory.Plant.ShortName,
                            PlantFullName = newFactory.Plant.FullName,
                            ActivationDate = activationData
                        });
                        data.SaveChanges("ProcessUnit Transfer");
                        transaction.Complete();
                    }
                }
            }
        }


        private static void Print(IEfStatus result)
        {
            foreach (var error in result.EfErrors)
            {
                Console.WriteLine($"Error: {error.MemberNames.FirstOrDefault()} - {error.ErrorMessage}");
            }
        }
    }
}
