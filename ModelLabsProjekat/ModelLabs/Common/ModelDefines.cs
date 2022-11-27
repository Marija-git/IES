using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

		TERMINAL							= 0x0001,
		DAYTYPE								= 0x0002,
		SEASON							    = 0x0003,
		REGULATINGCONTROL					= 0x0004,
		REGULATIONSCHEDULE					= 0x0005,
		ASYNCHRONOUSMACHINE					= 0x0006,
	}

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ									= 0x1000000000000000,
		IDOBJ_GID								= 0x1000000000000104,
		IDOBJ_MRID								= 0x1000000000000207,
		IDOBJ_NAME								= 0x1000000000000307,

		PSR										= 0x1100000000000000,

		TERMINAL								= 0x1200000000010000,
		TERMINAL_REGULATINGCONTROLS				= 0x1200000000010119,

		DAYTYPE									= 0x1300000000020000,
		DAYTYPE_SEASONDAYTYPESCHEDULES			= 0x1300000000020119,

		SEASON									= 0x1400000000030000,
		SEASON_ENDDATE							= 0x1400000000030108,
		SEASON_START_DATE						= 0x1400000000030208,
		SEASON_SEASONDAYTYPESCHEDULES			= 0x1400000000030319,

		BASICINTERVALSCHEDULE					= 0x1500000000000000,
		BASICINTERVALSCHEDULE_STARTTIME			= 0x1500000000000108,
		BASICINTERVALSCHEDULE_VALUE1MULTIPLIER	= 0x150000000000020a,
		BASICINTERVALSCHEDULE_VALUE1UNIT		= 0x150000000000030a,
		BASICINTERVALSCHEDULE_VALUE2MULTIPLIER	= 0x150000000000040a,
		BASICINTERVALSCHEDULE_VALUE2UNIT		= 0x150000000000050a,

		EQUIPMENT								= 0x1110000000000000,
		EQUIPMENT_AGREGATE						= 0x1110000000000101,
		EQUIPMENT_NORMALLYINSERVICE				= 0x1110000000000201,

		CONDEQ									= 0x1111000000000000,

		REGULATINGCONDEQ						= 0x1111100000000000,
		REGULATINGCONDEQ_REGULATINGCONTROL		= 0x1111100000000109,

		ROTATINGMACHINE							= 0x1111110000000000,

		ASYNCHRONOUSMACHINE						= 0x1111111000060000,

		REGULATINGCONTROL						= 0x1120000000040000,
		REGULATINGCONTROL_DISCRETE				= 0x1120000000040101,
		REGULATINGCONTROL_MODE					= 0x112000000004020a,
		REGULATINGCONTROL_MONITOREDPHASE		= 0x112000000004030a,
		REGULATINGCONTROL_TARGETRANGE			= 0x1120000000040405,
		REGULATINGCONTROL_TARGETVALUE			= 0x1120000000040505,
		REGULATINGCONTROL_REGULATIONSCHEDULES	= 0x1120000000040619,
		REGULATINGCONTROL_REGULATINGCONDEQS		= 0x1120000000040719,
		REGULATINGCONTROL_TERMINAL				= 0x1120000000040809,

		REGULARINTERVALCHEDULE					= 0x1510000000000000,

		SEASONDAYTYPESCHEDULE					= 0x1511000000000000,
		SEASONDAYTYPESCHEDULE_SEASON			= 0x1511000000000109,
		SEASONDAYTYPESCHEDULE_DAYTYPE			= 0x1511000000000209,

		REGULATIONSCHEDULE						= 0x1511100000050000,
		REGULATIONSCHEDULE_REGULATINGCONTROL	= 0x1511100000050109,
	}

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}

