using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.LoadModel
{
	public class Season : IdentifiedObject
	{
		private List<long> seasonDayTypeSchedules = new List<long>();
		private DateTime endDate;
		private DateTime startDate;

		public Season(long globalId) : base(globalId)
		{

		}

		public List<long> SeasonDayTypeSchedules { get => seasonDayTypeSchedules; set => seasonDayTypeSchedules = value; }
		public DateTime EndDate { get => endDate; set => endDate = value; }
		public DateTime StartDate { get => startDate; set => startDate = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Season x = (Season)obj;
				return (x.endDate == this.endDate && x.startDate == this.startDate &&
						(CompareHelper.CompareLists(x.seasonDayTypeSchedules, this.seasonDayTypeSchedules)));
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
				//case ModelCode.SEASON_SEASONDAYTYPESCHEDULES:
				case ModelCode.SEASON_ENDDATE:
				case ModelCode.SEASON_START_DATE:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SEASON_ENDDATE:
					property.SetValue(endDate);
					break;

				case ModelCode.SEASON_START_DATE:
					property.SetValue(startDate);
					break;

				case ModelCode.SEASON_SEASONDAYTYPESCHEDULES:
					property.SetValue(seasonDayTypeSchedules);
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
				case ModelCode.SEASON_ENDDATE:
					endDate = property.AsDateTime();
					break;

				case ModelCode.SEASON_START_DATE:
					startDate = property.AsDateTime();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return seasonDayTypeSchedules.Count > 0 || base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (seasonDayTypeSchedules != null && seasonDayTypeSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				//referenca na day-type
				//references[ModelCode.DAYTYPE_SEASONDAYTYPESCHEDULES] = seasonDayTypeSchedules.GetRange(0, seasonDayTypeSchedules.Count);
				references[ModelCode.SEASONDAYTYPESCHEDULE_SEASON] = seasonDayTypeSchedules.GetRange(0, seasonDayTypeSchedules.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:
					seasonDayTypeSchedules.Add(globalId);
					break;

				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.SEASONDAYTYPESCHEDULE_SEASON:

					if (seasonDayTypeSchedules.Contains(globalId))
					{
						seasonDayTypeSchedules.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;

				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}

		#endregion IReference implementation		
	}
}
