using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
    public class SeasonDayTypeChedule : RegularIntervalSchedule
    {
        private long dayType = 0;
        private long season = 0;
        public SeasonDayTypeChedule(long globalId)
      : base(globalId)
        {
        }

        public long DayType { get => dayType; set => dayType = value; }
        public long Season { get => season; set => season = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeasonDayTypeChedule x = (SeasonDayTypeChedule)obj;
                return (x.dayType == this.dayType && x.season == this.season);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
                case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
                    property.SetValue(dayType);
                    break;

                case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:
                    property.SetValue(season);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {

                case ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE:
                    dayType = property.AsReference();
                    break;

                case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:
                    season = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (season != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASONDAYTYPESCHEDULE_SEASON] = new List<long>();
                references[ModelCode.SEASONDAYTYPESCHEDULE_SEASON].Add(season);
            }
            if (dayType != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE] = new List<long>();
                references[ModelCode.SEASONDAYTYPESCHEDULE_DAYTYPE].Add(dayType);
            }
           


            base.GetReferences(references, refType);
        }

        #endregion IReference implementation		
    }
}
