using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Equipment : PowerSystemResource
	{
		private bool aggregate;
		private bool normallyInService;

		public Equipment(long globalId) : base(globalId)
		{

		}

		public bool Aggregate { get => aggregate; set => aggregate = value; }
		public bool NormallyInService { get => normallyInService; set => normallyInService = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Equipment x = (Equipment)obj;
				return (
						(x.aggregate == this.aggregate) &&
						(x.normallyInService == this.normallyInService)
					  );
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
				case ModelCode.EQUIPMENT_AGREGATE:
				case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.EQUIPMENT_AGREGATE:
					property.SetValue(aggregate);
					break;

				case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
					property.SetValue(normallyInService);
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
				case ModelCode.EQUIPMENT_AGREGATE:
					aggregate = property.AsBool();
					break;

				case ModelCode.EQUIPMENT_NORMALLYINSERVICE:
					normallyInService = property.AsBool();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation




	}
}
