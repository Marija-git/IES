﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class Terminal : IdentifiedObject
	{
		private List<long> regulatingControls = new List<long>();

		public Terminal(long globalId) : base(globalId)
		{

		}

		public List<long> RegulatingControls { get => regulatingControls; set => regulatingControls = value; }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Terminal x = (Terminal)obj;
				return ((CompareHelper.CompareLists(x.regulatingControls, this.regulatingControls)));
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
				case ModelCode.TERMINAL_REGULATINGCONTROLS:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.TERMINAL_REGULATINGCONTROLS:
					property.SetValue(regulatingControls);
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
				return regulatingControls.Count > 0 || base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (regulatingControls != null && regulatingControls.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				//referenca na terminal
				references[ModelCode.REGULATINGCONTROL_TERMINAL] = regulatingControls.GetRange(0, regulatingControls.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.REGULATINGCONTROL_TERMINAL:
					regulatingControls.Add(globalId);
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
				case ModelCode.REGULATINGCONTROL_TERMINAL:

					if (regulatingControls.Contains(globalId))
					{
						regulatingControls.Remove(globalId);
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
