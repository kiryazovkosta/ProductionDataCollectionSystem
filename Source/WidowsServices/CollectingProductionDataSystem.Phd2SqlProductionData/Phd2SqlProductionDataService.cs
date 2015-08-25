﻿

namespace CollectingProductionDataSystem.Phd2SqlProductionData
{
    using log4net;
    using System;
    using System.ServiceProcess;
    using System.Threading;

    public partial class Phd2SqlProductionDataService : ServiceBase
    {
        private Timer inspectionDataTimer = null;

        private Timer primaryDataTimer = null;

        private Timer inventoryDataTimer = null;

        private Timer measurementDataTimer = null;

        private static readonly object lockObjectInspectionPointsData = new object();

        private static readonly object lockObjectPrimaryData = new object();

        private static readonly object lockObjectInventoryData = new object();

        private static readonly object lockObjectMeasurementData = new object();

        private readonly ILog logger;

        public Phd2SqlProductionDataService(ILog loggerParam)
        {
            InitializeComponent();
            this.logger = loggerParam;
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("Service is started!");
            try
            {
                if (Properties.Settings.Default.SYNC_INSPECTION_POINTS)
                {
                    this.inspectionDataTimer = new Timer(TimerHandlerInspectionPoints, null, 0, Timeout.Infinite);
                }

                if (Properties.Settings.Default.SYNC_PRIMARY)
                {
                    this.primaryDataTimer = new Timer(TimerHandlerPrimary, null, 0, Timeout.Infinite);
                }

                if (Properties.Settings.Default.SYNC_INVENTORY)
                {
                    this.inventoryDataTimer = new Timer(TimerHandlerInventory, null, 0, Timeout.Infinite);
                }

                if (Properties.Settings.Default.SYNC_MEASUREMENTS_POINTS)
                {
                    this.measurementDataTimer = new Timer(TimerHandlerMeasurement, null, 0, Timeout.Infinite); 
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }
        }

        protected override void OnStop()
        {
            logger.Info("Service is stopped!");
            try
            {

            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }
        }

        private void TimerHandlerInspectionPoints(object state)
        {
            lock (lockObjectInspectionPointsData)
            {
                try
                {
                    Utility.SetRegionalSettings();
                    this.inspectionDataTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    Phd2SqlProductionDataMain.ProcessInspectionPointsData();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                }
                finally
                {
                    this.inspectionDataTimer.Change(Convert.ToInt64(Properties.Settings.Default.IDLE_TIMER_INSPECTION_POINTS.TotalMilliseconds), 
                        System.Threading.Timeout.Infinite);
                }
            }
        }

        private void TimerHandlerPrimary(object state)
        {
            lock (lockObjectPrimaryData)
            {
                try
                {
                    Utility.SetRegionalSettings();
                    this.primaryDataTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    Phd2SqlProductionDataMain.ProcessPrimaryProductionData();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                }
                finally
                {
                    this.primaryDataTimer.Change(Convert.ToInt64(Properties.Settings.Default.IDLE_TIMER_PRIMARY.TotalMilliseconds), 
                        System.Threading.Timeout.Infinite);
                }
            }
        }

        private void TimerHandlerInventory(object state)
        {
            lock (lockObjectInventoryData)
            {
                try
                {
                    Utility.SetRegionalSettings();
                    this.inventoryDataTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    Phd2SqlProductionDataMain.ProcessInventoryTanksData();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                }
                finally
                {
                    this.inventoryDataTimer.Change(Convert.ToInt64(Properties.Settings.Default.IDLE_TIMER_INVENTORY.TotalMilliseconds), 
                        System.Threading.Timeout.Infinite);
                }
            }
        }

        private void TimerHandlerMeasurement(object state)
        {
            lock (lockObjectInspectionPointsData)
            {
                try
                {
                    Utility.SetRegionalSettings();
                    this.measurementDataTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    Phd2SqlProductionDataMain.ProcessMeasuringPointsData();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                }
                finally
                {
                    this.measurementDataTimer.Change(Convert.ToInt64(Properties.Settings.Default.IDLE_TIMER_MEASUREMENTS_POINTS.TotalMilliseconds), 
                        System.Threading.Timeout.Infinite);
                }
            }
        }
    }
}
