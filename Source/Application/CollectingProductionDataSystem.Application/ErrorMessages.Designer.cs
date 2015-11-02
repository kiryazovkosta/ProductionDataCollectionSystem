﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App_Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CollectingProductionDataSystem.Application.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid e-mail.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file {0} not found..
        /// </summary>
        public static string FileNotFound {
            get {
                return ResourceManager.GetString("FileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There  is an error durring file processing {0}..
        /// </summary>
        public static string FileProcessError {
            get {
                return ResourceManager.GetString("FileProcessError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid parameter {0}. Value must be between {1} and {2}..
        /// </summary>
        public static string InvalidParameter {
            get {
                return ResourceManager.GetString("InvalidParameter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Password.
        /// </summary>
        public static string InvalidPassword {
            get {
                return ResourceManager.GetString("InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Record on line {0}..
        /// </summary>
        public static string InvalidRecord {
            get {
                return ResourceManager.GetString("InvalidRecord", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Record Type {0}..
        /// </summary>
        public static string InvalidRecordType {
            get {
                return ResourceManager.GetString("InvalidRecordType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user you are trying to update doesn&apos;t exist..
        /// </summary>
        public static string InvalidUser {
            get {
                return ResourceManager.GetString("InvalidUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Id of user must be 0. Actual Id is {0}..
        /// </summary>
        public static string InvalidUserId {
            get {
                return ResourceManager.GetString("InvalidUserId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Modification of UserName is not permitted.
        /// </summary>
        public static string InvalidUserNameModification {
            get {
                return ResourceManager.GetString("InvalidUserNameModification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The data for {0} are not approved..
        /// </summary>
        public static string ShiftNotReady {
            get {
                return ResourceManager.GetString("ShiftNotReady", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user {0} cannot be created. The user with the same UserName allready exists..
        /// </summary>
        public static string UserAllreadyExists {
            get {
                return ResourceManager.GetString("UserAllreadyExists", resourceCulture);
            }
        }
    }
}
