//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    
    
    /// A pre-established pattern over time for a controlled variable, e.g., busbar voltage.
    public class RegulationSchedule : SeasonDayTypeSchedule {
        
        /// Regulating controls that have this Schedule.
        private RegulatingControl cim_RegulatingControl;
        
        private const bool isRegulatingControlMandatory = false;
        
        private const string _RegulatingControlPrefix = "cim";
        
        public virtual RegulatingControl RegulatingControl {
            get {
                return this.cim_RegulatingControl;
            }
            set {
                this.cim_RegulatingControl = value;
            }
        }
        
        public virtual bool RegulatingControlHasValue {
            get {
                return this.cim_RegulatingControl != null;
            }
        }
        
        public static bool IsRegulatingControlMandatory {
            get {
                return isRegulatingControlMandatory;
            }
        }
        
        public static string RegulatingControlPrefix {
            get {
                return _RegulatingControlPrefix;
            }
        }
    }
}
