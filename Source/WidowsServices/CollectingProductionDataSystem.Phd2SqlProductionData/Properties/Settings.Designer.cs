﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CollectingProductionDataSystem.Phd2SqlProductionData.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("srv-vm-mes-phd.neftochim.bg")]
        public string PHD_HOST {
            get {
                return ((string)(this["PHD_HOST"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3150")]
        public int PHD_PORT {
            get {
                return ((int)(this["PHD_PORT"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("01:00:00")]
        public global::System.TimeSpan IDLE_TIMER_PRIMARY {
            get {
                return ((global::System.TimeSpan)(this["IDLE_TIMER_PRIMARY"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SYNC_PRIMARY {
            get {
                return ((bool)(this["SYNC_PRIMARY"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:20:00")]
        public global::System.TimeSpan IDLE_TIMER_INVENTORY {
            get {
                return ((global::System.TimeSpan)(this["IDLE_TIMER_INVENTORY"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SYNC_INVENTORY {
            get {
                return ((bool)(this["SYNC_INVENTORY"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool FORCE_REGIONAL_SETTINGS {
            get {
                return ((bool)(this["FORCE_REGIONAL_SETTINGS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("bg-BG")]
        public string CULTURE_INFO {
            get {
                return ((string)(this["CULTURE_INFO"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("49")]
        public int INSPECTION_DATA_MINIMUM_CONFIDENCE {
            get {
                return ((int)(this["INSPECTION_DATA_MINIMUM_CONFIDENCE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public uint INSPECTION_DATA_MAX_ROWS {
            get {
                return ((uint)(this["INSPECTION_DATA_MAX_ROWS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Snapshot")]
        public global::Uniformance.PHD.SAMPLETYPE INSPECTION_DATA_SAMPLETYPE {
            get {
                return ((global::Uniformance.PHD.SAMPLETYPE)(this["INSPECTION_DATA_SAMPLETYPE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int MIN_GET_INVENTORY_HOURS_INTERVAL {
            get {
                return ((int)(this["MIN_GET_INVENTORY_HOURS_INTERVAL"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SYNC_MEASUREMENTS_POINTS {
            get {
                return ((bool)(this["SYNC_MEASUREMENTS_POINTS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("01:00:00")]
        public global::System.TimeSpan IDLE_TIMER_MEASUREMENTS_POINTS {
            get {
                return ((global::System.TimeSpan)(this["IDLE_TIMER_MEASUREMENTS_POINTS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int MAX_GET_INVENTORY_HOURS_INTERVAL {
            get {
                return ((int)(this["MAX_GET_INVENTORY_HOURS_INTERVAL"]));
            }
        }
    }
}
