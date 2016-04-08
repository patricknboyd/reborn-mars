﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Boyd.Games.RebornMars.Assets {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Boyd.Games.RebornMars.Assets.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        ///   Looks up a localized string similar to The current level is outside the range of available levels..
        /// </summary>
        public static string CurrentLevelNotInAvailableRange {
            get {
                return ResourceManager.GetString("CurrentLevelNotInAvailableRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dungeon branch {0} does not currently exist in this game..
        /// </summary>
        public static string DungeonBranchNotFound {
            get {
                return ResourceManager.GetString("DungeonBranchNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The map at level {0} of branch {1}, has not been created yet. Create the map level using DungeonBranch.CreateNewLevel() before trying to move the player onto that level..
        /// </summary>
        public static string DungeonLevelDoesNotExist {
            get {
                return ResourceManager.GetString("DungeonLevelDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A service of type {0} has already been registered..
        /// </summary>
        public static string DuplicateServiceMessage {
            get {
                return ResourceManager.GetString("DuplicateServiceMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to find branch of type {0} when creating a branch connection..
        /// </summary>
        public static string MissingBranchForConnection {
            get {
                return ResourceManager.GetString("MissingBranchForConnection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are no stairs to descend here..
        /// </summary>
        public static string MoveDownNotOnStairs {
            get {
                return ResourceManager.GetString("MoveDownNotOnStairs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are no stairs to climb here..
        /// </summary>
        public static string MoveUpNotOnStairs {
            get {
                return ResourceManager.GetString("MoveUpNotOnStairs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type {0} does not implement IMapTile. Only types that implement IMapTile can be used for map tiles..
        /// </summary>
        public static string ProvidedTypeIsNotTile {
            get {
                return ResourceManager.GetString("ProvidedTypeIsNotTile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type {0} does not implement required interface {1}..
        /// </summary>
        public static string TypeDoesNotImplementInterface {
            get {
                return ResourceManager.GetString("TypeDoesNotImplementInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unrecognized enum value {0}..
        /// </summary>
        public static string UnrecognizedEnumValue {
            get {
                return ResourceManager.GetString("UnrecognizedEnumValue", resourceCulture);
            }
        }
    }
}
