using UnityEngine;

[RequireComponent(typeof(Car2DController))]
public class CarsValues : MonoBehaviour
{
	public class Health
	{
		public float BaseValue;
	}

	public class Turbo
	{
		public float Power;

		public float Angle;

		public float MaxValue;

		public float Value;

		public float UsageValue;

		public float RestoreTime;
	}

	public class Engine
	{
		public float MaxSpeed;

		public float Torque;
	}

	public class Suspansion
	{
		public float BodyMass;

		public float WheelMass;

		public float SpringMass;

		public float Distance;

		public float Frequency;

		public float Damping;

		public float WheelsFriction;

		public float RotationAcceleration;
	}

	public class Rockets
	{
		public float Distance;

		public float MaxValue;

		public float Value;

		public float StartPower;

		public float PauseTime;
	}

	public class AISettings
	{
		public float Reward;

		public float EatDamage;

		public float eat_delay;

		public float Explosive;

		public float InvisibleSpeed;
	}

	public class RamDamage
	{
		public float Damage;
	}

	public class StartData
	{
		public Health _health = new Health();

		public Turbo _turbo = new Turbo();

		public Engine _engine = new Engine();

		public Suspansion _suspansion = new Suspansion();

		public Rockets _rockets = new Rockets();

		public AISettings _ai = new AISettings();

		public RamDamage _ramDamage = new RamDamage();

		private static StartData hashAIStartValue;

		private static StartData hashCarStartValue_0;

		private static StartData hashCarStartValue_1;

		private static StartData hashCarStartValue_2;

		private static StartData hashCarStartValue_3;

		private static StartData hashCarStartValue_4;

		private static StartData hashCarStartValue_5;

		private static StartData hashCarStartValue_6;

		private static StartData hashCarStartValue_7;

		private static StartData hashCarStartValue_8;

		private static StartData hashCarStartValue_9;

		private static StartData hashCarStartValue_10;

		private static StartData hashCarStartValue_11;

		private static StartData hashCarStartValue_12;

		private static StartData hashCarStartValue_13;

		private static StartData hashAISettings_0;

		private static StartData hashAISettings_1;

		private static StartData hashAISettings_2;

		private static StartData hashAISettings_3;

		private static StartData hashAISettings_4;

		private static StartData hashAISettings_5;

		private static StartData hashAISettings_6;

		private static StartData hashAISettings_7;

		private static StartData hashAISettings_8;

		private static StartData hashAISettings_9;

		private static StartData hashAISettings_10;

		private static StartData hashAISettings_11;

		private static StartData hashAISettings_12;

		private static StartData hashAISettings_13;

		private static StartData hashAISettings_14;

		private static StartData hashAISettings_15;

		private static StartData hashAISettings_16;

		private static StartData hashAISettings_17;

		private static StartData hashAISettings_18;

		private static StartData hashAISettings_19;

		private static StartData hashAISettings_20;

		private static StartData hashAISettings_21;

		private static StartData hashAISettings_22;

		private static StartData hashAISettings_23;

		public static void HashAllConfigsStartData()
		{
			if (hashAISettings_0 == null)
			{
				hashAIStartValue = LoadDataByFileName("AIStartValue");
				hashCarStartValue_0 = LoadDataByFileName("CarStartValue_0");
				hashCarStartValue_1 = LoadDataByFileName("CarStartValue_1");
				hashCarStartValue_2 = LoadDataByFileName("CarStartValue_2");
				hashCarStartValue_3 = LoadDataByFileName("CarStartValue_3");
				hashCarStartValue_4 = LoadDataByFileName("CarStartValue_4");
				hashCarStartValue_5 = LoadDataByFileName("CarStartValue_5");
				hashCarStartValue_6 = LoadDataByFileName("CarStartValue_6");
				hashCarStartValue_7 = LoadDataByFileName("CarStartValue_7");
				hashCarStartValue_8 = LoadDataByFileName("CarStartValue_8");
				hashCarStartValue_9 = LoadDataByFileName("CarStartValue_9");
				hashCarStartValue_10 = LoadDataByFileName("CarStartValue_10");
				hashCarStartValue_11 = LoadDataByFileName("CarStartValue_11");
				hashCarStartValue_12 = LoadDataByFileName("CarStartValue_12_und_05");
				hashCarStartValue_13 = LoadDataByFileName("CarStartValue_13_und_06");
				hashAISettings_0 = LoadDataByFileName("AISettings_0");
				hashAISettings_1 = LoadDataByFileName("AISettings_1");
				hashAISettings_2 = LoadDataByFileName("AISettings_2");
				hashAISettings_3 = LoadDataByFileName("AISettings_3");
				hashAISettings_4 = LoadDataByFileName("AISettings_4");
				hashAISettings_5 = LoadDataByFileName("AISettings_5");
				hashAISettings_6 = LoadDataByFileName("AISettings_6");
				hashAISettings_7 = LoadDataByFileName("AISettings_7");
				hashAISettings_8 = LoadDataByFileName("AISettings_8");
				hashAISettings_9 = LoadDataByFileName("AISettings_9");
				hashAISettings_10 = LoadDataByFileName("AISettings_10");
				hashAISettings_11 = LoadDataByFileName("AISettings_11");
				hashAISettings_12 = LoadDataByFileName("AISettings_12");
				hashAISettings_13 = LoadDataByFileName("AISettings_13");
				hashAISettings_14 = LoadDataByFileName("AISettings_14");
				hashAISettings_15 = LoadDataByFileName("AISettings_15");
				hashAISettings_16 = LoadDataByFileName("AISettings_9_1");
				hashAISettings_17 = LoadDataByFileName("AISettings_9_2");
				hashAISettings_18 = LoadDataByFileName("AISettings_9_3");
				hashAISettings_19 = LoadDataByFileName("AISettings_9_4");
				hashAISettings_20 = LoadDataByFileName("AISettings_11_1");
				hashAISettings_21 = LoadDataByFileName("AISettings_11_2");
				hashAISettings_22 = LoadDataByFileName("AISettings_11_3");
				hashAISettings_23 = LoadDataByFileName("AISettings_11_4");
			}
		}

		public static StartData GetCarSettings(bool isAi = false, int num = 0)
		{
			if (isAi)
			{
				if (hashAIStartValue == null)
				{
					return LoadDataByFileName("AIStartValue");
				}
				return hashAIStartValue;
			}
			switch (num)
			{
			case 0:
				if (hashCarStartValue_0 == null)
				{
					return LoadDataByFileName("CarStartValue_0");
				}
				return hashCarStartValue_0;
			case 1:
				if (hashCarStartValue_1 == null)
				{
					return LoadDataByFileName("CarStartValue_1");
				}
				return hashCarStartValue_1;
			case 2:
				if (hashCarStartValue_2 == null)
				{
					return LoadDataByFileName("CarStartValue_2");
				}
				return hashCarStartValue_2;
			case 3:
				if (hashCarStartValue_3 == null)
				{
					return LoadDataByFileName("CarStartValue_3");
				}
				return hashCarStartValue_3;
			case 4:
				if (hashCarStartValue_4 == null)
				{
					return LoadDataByFileName("CarStartValue_4");
				}
				return hashCarStartValue_4;
			case 5:
				if (hashCarStartValue_5 == null)
				{
					return LoadDataByFileName("CarStartValue_5");
				}
				return hashCarStartValue_5;
			case 6:
				if (hashCarStartValue_6 == null)
				{
					return LoadDataByFileName("CarStartValue_6");
				}
				return hashCarStartValue_6;
			case 7:
				if (hashCarStartValue_7 == null)
				{
					return LoadDataByFileName("CarStartValue_7");
				}
				return hashCarStartValue_7;
			case 8:
				if (hashCarStartValue_8 == null)
				{
					return LoadDataByFileName("CarStartValue_8");
				}
				return hashCarStartValue_8;
			case 9:
				if (hashCarStartValue_9 == null)
				{
					return LoadDataByFileName("CarStartValue_9");
				}
				return hashCarStartValue_9;
			case 10:
				if (hashCarStartValue_10 == null)
				{
					return LoadDataByFileName("CarStartValue_10");
				}
				return hashCarStartValue_10;
			case 11:
				if (hashCarStartValue_11 == null)
				{
					return LoadDataByFileName("CarStartValue_11");
				}
				return hashCarStartValue_11;
			case 12:
				if (hashCarStartValue_11 == null)
				{
					return LoadDataByFileName("CarStartValue_12_und_05");
				}
				return hashCarStartValue_12;
			case 13:
				if (hashCarStartValue_11 == null)
				{
					return LoadDataByFileName("CarStartValue_13_und_06");
				}
				return hashCarStartValue_13;
			default:
				return null;
			}
		}

		public static StartData GetCurrentAISettings(int num)
		{
			switch (num)
			{
			case 0:
				return hashAISettings_0;
			case 1:
				return hashAISettings_1;
			case 2:
				return hashAISettings_2;
			case 3:
				return hashAISettings_3;
			case 4:
				return hashAISettings_4;
			case 5:
				return hashAISettings_5;
			case 6:
				return hashAISettings_6;
			case 7:
				return hashAISettings_7;
			case 8:
				return hashAISettings_8;
			case 9:
				return hashAISettings_9;
			case 10:
				return hashAISettings_10;
			case 11:
				return hashAISettings_11;
			case 12:
				return hashAISettings_12;
			case 13:
				return hashAISettings_13;
			case 14:
				return hashAISettings_14;
			case 15:
				return hashAISettings_15;
			case 16:
				return hashAISettings_16;
			case 17:
				return hashAISettings_17;
			case 18:
				return hashAISettings_18;
			case 19:
				return hashAISettings_19;
			case 20:
				return hashAISettings_20;
			case 21:
				return hashAISettings_21;
			case 22:
				return hashAISettings_22;
			case 23:
				return hashAISettings_23;
			default:
				return null;
			}
		}

		public static StartData LoadDataByFileName(string filename)
		{
			return Configs.Load<StartData>(filename);
		}
	}

	public class Data
	{
		public float[] Health = new float[5];

		public TurboUp TurboUp = new TurboUp();

		public float[] Engine = new float[5];

		public float[] Wheels = new float[5];

		public float[] Rockets = new float[5];

		public float[] RamDamage = new float[3];

		private static Data hashCarSettings_0;

		private static Data hashCarSettings_1;

		private static Data hashCarSettings_2;

		private static Data hashCarSettings_3;

		private static Data hashCarSettings_4;

		private static Data hashCarSettings_5;

		private static Data hashCarSettings_6;

		private static Data hashCarSettings_7;

		private static Data hashCarSettings_8;

		private static Data hashCarSettings_9;

		private static Data hashCarSettings_10;

		private static Data hashCarSettings_11;

		private static Data hashCarSettings_12;

		private static Data hashCarSettings_13;

		public static void HashAllConfigsStartData()
		{
			if (hashCarSettings_0 == null)
			{
				hashCarSettings_0 = Configs.Load<Data>("CarSettings_0_gator");
				hashCarSettings_1 = Configs.Load<Data>("CarSettings_1_harvester");
				hashCarSettings_2 = Configs.Load<Data>("CarSettings_2_archiver");
				hashCarSettings_3 = Configs.Load<Data>("CarSettings_3_locomachine");
				hashCarSettings_4 = Configs.Load<Data>("CarSettings_4_berserker");
				hashCarSettings_5 = Configs.Load<Data>("CarSettings_5_beetlee");
				hashCarSettings_6 = Configs.Load<Data>("CarSettings_6_francopstein");
				hashCarSettings_7 = Configs.Load<Data>("CarSettings_7_carocop");
				hashCarSettings_8 = Configs.Load<Data>("CarSettings_8_rebbister");
				hashCarSettings_9 = Configs.Load<Data>("CarSettings_9_scorpion");
				hashCarSettings_10 = Configs.Load<Data>("CarSettings_10_cockchafer");
				hashCarSettings_11 = Configs.Load<Data>("CarSettings_11_tankominator");
				hashCarSettings_12 = Configs.Load<Data>("CarSettings_12_und_05_aligator");
				hashCarSettings_13 = Configs.Load<Data>("CarSettings_13_und_06_turtle");
			}
		}

		public static Data GetCarSettings(int num)
		{
			if (hashCarSettings_0 == null)
			{
				HashAllConfigsStartData();
			}
			switch (num)
			{
			case 0:
				return hashCarSettings_0;
			case 1:
				return hashCarSettings_1;
			case 2:
				return hashCarSettings_2;
			case 3:
				return hashCarSettings_3;
			case 4:
				return hashCarSettings_4;
			case 5:
				return hashCarSettings_5;
			case 6:
				return hashCarSettings_6;
			case 7:
				return hashCarSettings_7;
			case 8:
				return hashCarSettings_8;
			case 9:
				return hashCarSettings_9;
			case 10:
				return hashCarSettings_10;
			case 11:
				return hashCarSettings_11;
			case 12:
				return hashCarSettings_12;
			case 13:
				return hashCarSettings_13;
			default:
				return null;
			}
		}
	}

	public class TurboUp
	{
		public float[] Value = new float[5];

		public float[] Power = new float[5];
	}

	public static void HashAllConfigs()
	{
		StartData.HashAllConfigsStartData();
		Data.HashAllConfigsStartData();
	}
}
