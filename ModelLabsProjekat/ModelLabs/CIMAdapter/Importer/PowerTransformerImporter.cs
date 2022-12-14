using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)
			ImportTerminals();
			ImportDayTypes();
			ImportSeasons();
			ImportRegulatingControls();
			ImportRegulationSchedules();
			ImportAsynchronousMachines();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import
		private void ImportTerminals()
		{
			SortedDictionary<string, object> cinTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");
			if (cinTerminals != null)
			{
				foreach (KeyValuePair<string, object> cimTerminalPair in cinTerminals)
				{
					FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

					ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal cimTerminal)
		{
			ResourceDescription rd = null;
			if (cimTerminal != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimTerminal.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateTerminalProperties(cimTerminal, rd);
			}
			return rd;
		}

		private void ImportDayTypes()
		{
			SortedDictionary<string, object> cimDayTypes = concreteModel.GetAllObjectsOfType("FTN.DayType");
			if (cimDayTypes != null)
			{
				foreach (KeyValuePair<string, object> cimDayTypePair in cimDayTypes)
				{
					FTN.DayType cimDayType = cimDayTypePair.Value as FTN.DayType;

					ResourceDescription rd = CreateDayTypeResourceDescription(cimDayType);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("DayType ID = ").Append(cimDayType.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("DayType ID = ").Append(cimDayType.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateDayTypeResourceDescription(FTN.DayType cimDayType)
		{
			ResourceDescription rd = null;
			if (cimDayType != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DAYTYPE, importHelper.CheckOutIndexForDMSType(DMSType.DAYTYPE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimDayType.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateDayTypeProperties(cimDayType, rd);
			}
			return rd;
		}

		private void ImportSeasons()
		{
			SortedDictionary<string, object> cimSeasons = concreteModel.GetAllObjectsOfType("FTN.Season");
			if (cimSeasons != null)
			{
				foreach (KeyValuePair<string, object> cimSeasonPair in cimSeasons)
				{
					FTN.Season cimSeason = cimSeasonPair.Value as FTN.Season;

					ResourceDescription rd = CreateSeasonResourceDescription(cimSeason);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Season ID = ").Append(cimSeason.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Season ID = ").Append(cimSeason.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateSeasonResourceDescription(FTN.Season cimSeason)
		{
			ResourceDescription rd = null;
			if (cimSeason != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SEASON, importHelper.CheckOutIndexForDMSType(DMSType.SEASON));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimSeason.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateSeasonProperties(cimSeason, rd);
			}
			return rd;
		}

		private void ImportRegulatingControls()
		{
			SortedDictionary<string, object> cimRegulatingControls = concreteModel.GetAllObjectsOfType("FTN.RegulatingControl");
			if (cimRegulatingControls != null)
			{
				foreach (KeyValuePair<string, object> cimTRegulatingControlPair in cimRegulatingControls)
				{
					FTN.RegulatingControl cimRegulatingControl = cimTRegulatingControlPair.Value as FTN.RegulatingControl;

					ResourceDescription rd = CreateRegulatingControlResourceDescription(cimRegulatingControl);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegulatingControl ID = ").Append(cimRegulatingControl.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("RegulatingControl ID = ").Append(cimRegulatingControl.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateRegulatingControlResourceDescription(FTN.RegulatingControl cimRegulatingControl)
		{
			ResourceDescription rd = null;
			if (cimRegulatingControl != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATINGCONTROL, importHelper.CheckOutIndexForDMSType(DMSType.REGULATINGCONTROL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegulatingControl.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateRegulatingControlProperties(cimRegulatingControl, rd, importHelper, report);
			}
			return rd;

		}

		private void ImportRegulationSchedules()
		{
			SortedDictionary<string, object> cimRegulationSchedules = concreteModel.GetAllObjectsOfType("FTN.RegulationSchedule");
			if (cimRegulationSchedules != null)
			{
				foreach (KeyValuePair<string, object> RegulationSchedulePair in cimRegulationSchedules)
				{
					FTN.RegulationSchedule cimRegulationSchedule = RegulationSchedulePair.Value as FTN.RegulationSchedule;

					ResourceDescription rd = CreateRegulationScheduleResourceDescription(cimRegulationSchedule);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("RegulationSchedule ID = ").Append(cimRegulationSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("RegulationSchedule ID = ").Append(cimRegulationSchedule.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateRegulationScheduleResourceDescription(FTN.RegulationSchedule cimRegulationSchedule)
		{
			ResourceDescription rd = null;
			if (cimRegulationSchedule != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULATIONSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULATIONSCHEDULE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimRegulationSchedule.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateRegulationScheduleProperties(cimRegulationSchedule, rd, importHelper, report);
			}
			return rd;
		}


		private void ImportAsynchronousMachines()
		{
			SortedDictionary<string, object> cimAsynchronousMachines = concreteModel.GetAllObjectsOfType("FTN.AsynchronousMachine");
			if (cimAsynchronousMachines != null)
			{
				foreach (KeyValuePair<string, object> AsynchronousMachinePair in cimAsynchronousMachines)
				{
					FTN.AsynchronousMachine cimAsynchronousMachine = AsynchronousMachinePair.Value as FTN.AsynchronousMachine;

					ResourceDescription rd = CreateAsynchronousMachineResourceDescription(cimAsynchronousMachine);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AsynchronousMachine ID = ").Append(cimAsynchronousMachine.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AsynchronousMachine ID = ").Append(cimAsynchronousMachine.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAsynchronousMachineResourceDescription(FTN.AsynchronousMachine cimAsynchronousMachine)
		{
			ResourceDescription rd = null;
			if (cimAsynchronousMachine != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASYNCHRONOUSMACHINE, importHelper.CheckOutIndexForDMSType(DMSType.ASYNCHRONOUSMACHINE));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimAsynchronousMachine.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAsynchronousMachineProperties(cimAsynchronousMachine, rd, importHelper, report);
			}
			return rd;
		}
		#endregion Import
	}
}

