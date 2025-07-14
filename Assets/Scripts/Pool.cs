using System.Collections.Generic;
using UnityEngine;

public class Pool : PoolBase
{
	public enum Explosion
	{
		none,
		exp1,
		exp2,
		exp3,
		exp4,
		exp5,
		exp6,
		exp7,
		exp8,
		exp9,
		exp10,
		exp11,
		exp12,
		exp13,
		exp14,
		exp15,
		exp16,
		exp17,
		exp18,
		exp19,
		exp20,
		exp21,
		exp22,
		exp23,
		exp24,
		exp25,
		exp26,
		exp27,
		exp28,
		exp29,
		exp30,
		exp31
	}

	public enum Bombs
	{
		bomb0 = 0,
		bomb1 = 1,
		bomb2 = 2,
		bomb3 = 3,
		bomb4 = 4,
		bomb5 = 5,
		bullet = 6,
		tankrocket = 8,
		ice = 12,
		aibullet = 13,
		bomblip = 14,
		fuelCarBomb = 0xF,
		bombCepel = 0x10,
		bombAirStike = 24,
		smallerBombCivil1 = 17,
		smallerBombEnemy1 = 18,
		smallerBombCivil2 = 19,
		smallerBombCivil3 = 20,
		smallerBombEnemy2 = 21,
		smallerBombEnemy3 = 22,
		freezeRay = 23,
		copterBomb = 25,
		smallerBombCivilEster = 26,
		smallerBombEnemyEster = 27,
		smallerBombEnemyUndeground = 28,
		smallerBombEnemy2Undeground = 29
	}

	public enum Trash
	{
		none,
		Balloon,
		UFO,
		Astroaut,
		AstroSheep,
		Orbiter
	}

	public enum Bonus
	{
		none = 0,
		flip = 1,
		health = 2,
		turbo = 3,
		bomb = 7,
		hp = 8,
		spark = 9,
		bombcar = 10,
		policecar = 11,
		copter = 12,
		cepel = 13,
		boss1 = 14,
		boss2 = 18,
		boss3 = 19,
		bossTriger = 0xF,
		ruby = 17,
		pyro = 0x10,
		bomb_pyro = 20,
		portal = 21,
		enemyairstrike = 23,
		CivilEster = 22,
		boss1Undeground = 24,
		boss2Undeground = 25
	}

	public enum Scraps
	{
		none = 0,
		scrap1 = 1,
		scrap2 = 2,
		scrap3 = 3,
		scrap4 = 4,
		scrap5 = 5,
		scrap6 = 6,
		scrap7 = 7,
		scrap8 = 8,
		scrap9 = 9,
		scrap10 = 10,
		trash_1 = 11,
		trash_2 = 12,
		trash_3 = 13,
		trash_4 = 14,
		trash_5 = 0xF,
		trash_6 = 0x10,
		trash_7 = 17,
		trash_8 = 18,
		trash_9 = 19,
		trash_10 = 20,
		trash_11 = 21,
		trash_12 = 22,
		trash_13 = 23,
		smoke = 24,
		ice = 25,
		phys_scr_car1 = 101,
		phys_scr_car2 = 102,
		phys_scr_car3 = 103,
		phys_scr_car4 = 104,
		phys_scr_car5 = 105,
		scr_car1 = 106,
		scr_car2 = 107,
		scr_car3 = 108,
		scr_car4 = 109,
		scr_car5 = 110,
		scr_fire_bomb = 111,
		scr_cluster_bomb = 112,
		enemyScrap1 = 213,
		enemyScrap2 = 214,
		enemyScrap3 = 215,
		enemyScrap4 = 216,
		CarScrap = 217,
		CarScrapPolice = 224,
		CarScrapPoliceUnder = 233,
		civicScrap1 = 218,
		pitDoor = 219,
		pitDoor1 = 220,
		CageScr1 = 221,
		CageScr2 = 222,
		CageScr3 = 223,
		CageScrPoliceCar1 = 225,
		CageScrPoliceCar2 = 226,
		CageScrPoliceCar3 = 227,
		CageScrPoliceCar4 = 228,
		CageScrPoliceCar5 = 229,
		SpiderScr1 = 230,
		SpiderScr2 = 231,
		SpiderScr3 = 232
	}

	public enum ScrapDynamic
	{
		sc_barrel1 = 301,
		sc_barrel2 = 302,
		sc_barrel3 = 303,
		sc_barrel4 = 304,
		sc_barrel5 = 305,
		sc_barrel6 = 306,
		sc_barrel7 = 307,
		sc_barrel8 = 308,
		sc_barrel9 = 309,
		sc_barrel10 = 310,
		sc_barrel11 = 311,
		sc_barrel12 = 312,
		sc_beams1 = 321,
		sc_beams2 = 322,
		sc_beams3 = 323,
		sc_beams4 = 324,
		sc_beams5 = 325,
		sc_beams6 = 326,
		sc_beams7 = 327,
		sc_beams8 = 328,
		sc_boxes1 = 331,
		sc_boxes2 = 332,
		sc_boxes3 = 333,
		sc_boxes4 = 334,
		sc_boxes5 = 335,
		sc_boxes6 = 336,
		sc_boxes7 = 337,
		sc_boxes8 = 338,
		sc_boxes9 = 339,
		sc_boxes10 = 340,
		sc_boxes11 = 341,
		sc_boxes12 = 342,
		sc_boxes13 = 343,
		sc_boxes14 = 344,
		sc_boxes15 = 345,
		sc_boxes16 = 346,
		sc_boxes17 = 347,
		sc_boxes18 = 348,
		sc_bricks1 = 351,
		sc_bricks2 = 352,
		sc_bricks3 = 353,
		sc_bricks4 = 354,
		sc_bricks5 = 355,
		sc_bricks6 = 356,
		sc_bricks7 = 357,
		sc_bricks8 = 358,
		sc_bricks9 = 359,
		sc_bricks10 = 360,
		sc_bricks11 = 361,
		sc_bricks12 = 362,
		sc_bricks13 = 363,
		sc_bricks14 = 364,
		sc_bricks15 = 365,
		sc_circles1 = 371,
		sc_circles2 = 372,
		sc_circles3 = 373,
		sc_circles4 = 374,
		sc_circles5 = 375,
		sc_circles6 = 376,
		sc_circles7 = 377,
		sc_circles8 = 378,
		sc_circles9 = 379,
		sc_circles10 = 380,
		sc_circles11 = 381,
		sc_circles12 = 382,
		sc_skull1 = 383,
		sc_skull2 = 384,
		duct01 = 391,
		duct02 = 392,
		duct03 = 393,
		obj_tania_barrier1_scrp_001 = 394,
		obj_tania_barrier1_scrp_002 = 395,
		obj_tania_barrier1_scrp_003 = 396,
		obj_tania_barrier1_scrp_004 = 397,
		obj_tania_barrier2_scrp_001 = 398,
		obj_tania_barrier2_scrp_002 = 399,
		obj_tania_barrier2_scrp_003 = 400,
		obj_tania_barrier2_scrp_004 = 401,
		obj_tania_barrier3_scrp_001 = 402,
		obj_tania_barrier3_scrp_002 = 403,
		obj_tania_barrier3_scrp_003 = 404,
		obj_tania_barrier4_scrp_001 = 405,
		obj_tania_barrier4_scrp_002 = 406,
		obj_tania_barrier4_scrp_003 = 407,
		valentine_olya_obj_003 = 408,
		valentine_olya_obj_004 = 409,
		valentine_olya_obj_005 = 410,
		valentine_olya_obj_006 = 411,
		valentine_olya_obj_007 = 412,
		valentine_olya_obj_008 = 413,
		valentine_column_01_scrap_olya = 414,
		valentine_column_02_scrap_olya = 415,
		valentine_column_03_scrap_olya = 416,
		easter_column_01_scrap_olya = 417,
		easter_column_02_scrap_olya = 418,
		easter_column_03_scrap_olya = 419,
		easter_olya_obj_003 = 420,
		easter_olya_obj_004 = 421,
		easter_olya_obj_005 = 422,
		easter_olya_obj_006 = 423,
		easter_olya_obj_007 = 423,
		easter_olya_obj_008 = 424,
		tania_easter_obj_log1_scrp_002 = 425,
		tania_easter_obj_log1_scrp_003 = 426,
		tania_easter_obj_log2_scrp_002 = 427,
		dcr_olya_canaliz_scrap_06_1 = 428,
		dcr_olya_canaliz_scrap_06_2 = 429,
		dcr_olya_canaliz_scrap_06_3 = 430,
		dcr_olya_canaliz_scrap_06_4 = 431,
		dcr_olya_canaliz_scrap_07_1 = 432,
		dcr_olya_canaliz_scrap_07_2 = 433,
		dcr_olya_canaliz_scrap_07_3 = 434,
		dcr_olya_canaliz_scrap_07_4 = 435,
		obj_igor_canaliz_01_scp1 = 436,
		obj_igor_canaliz_01_scp2 = 437,
		obj_igor_canaliz_01_scp3 = 438,
		obj_igor_canaliz_01_scp4 = 439,
		obj_igor_canaliz_02_scp1 = 440,
		obj_igor_canaliz_02_scp2 = 441,
		obj_igor_canaliz_02_scp3 = 442,
		obj_igor_canaliz_02_scp4 = 443,
		obj_igor_canaliz_02_scp5 = 444,
		obj_igor_canaliz_03_scp1 = 445,
		obj_igor_canaliz_03_scp2 = 446,
		obj_igor_canaliz_03_scp3 = 447,
		obj_igor_canaliz_03_scp4 = 448,
		obj_igor_canaliz_04_scp1 = 449,
		obj_igor_canaliz_04_scp2 = 450,
		obj_igor_canaliz_04_scp3 = 451,
		obj_igor_canaliz_04_scp4 = 452,
		obj_igor_canaliz_05_scp1 = 453,
		obj_igor_canaliz_05_scp2 = 454,
		obj_igor_canaliz_06_scp1 = 455,
		obj_igor_canaliz_06_scp2 = 456,
		obj_igor_canaliz_07_scp1 = 457,
		obj_igor_canaliz_07_scp10 = 458,
		obj_olya_canaliz_scrap_08_1 = 459,
		obj_olya_canaliz_scrap_08_2 = 460,
		obj_olya_canaliz_scrap_08_3 = 461,
		obj_olya_canaliz_scrap_08_4 = 462,
		obj_olya_canaliz_scrap_08_5 = 463,
		obj_olya_canaliz_scrap_08_6 = 464,
		obj_olya_canaliz_scrap_08_7 = 465,
		obj_olya_canaliz_scrap_09_1 = 466,
		obj_olya_canaliz_scrap_09_2 = 467,
		obj_olya_canaliz_scrap_09_3 = 468,
		obj_olya_canaliz_scrap_09_4 = 469,
		obj_olya_canaliz_scrap_10_1 = 470,
		obj_olya_canaliz_scrap_10_2 = 471,
		obj_olya_canaliz_scrap_10_3 = 472,
		obj_olya_canaliz_scrap_10_4 = 473,
		obj_olya_canaliz_scrap_10_5 = 474,
		obj_olya_canaliz_scrap_11_1 = 475,
		obj_olya_canaliz_scrap_11_2 = 476,
		obj_olya_canaliz_scrap_11_3 = 477,
		obj_olya_canaliz_scrap_11_4 = 478,
		obj_olya_canaliz_scrap_12_1 = 479,
		obj_olya_canaliz_scrap_12_2 = 480,
		obj_olya_canaliz_scrap_12_3 = 481,
		obj_olya_canaliz_scrap_12_4 = 482
	}

	private static Pool _instance;

	public bool init;

	private bool isCached;

	private string defaultLayer = "Particles";

	private static string strEmpty = string.Empty;

	public static Pool instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType<Pool>();
				if (_instance != null)
				{
					_instance.isCached = true;
				}
				else
				{
					GameObject gameObject = new GameObject("Pool");
					_instance = gameObject.AddComponent<Pool>();
					_instance.CreatePool(gameObject);
				}
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public static string Name(Explosion exp)
	{
		switch (exp)
		{
		case Explosion.none:
			UnityEngine.Debug.Log("none!");
			return null;
		case Explosion.exp1:
			return "CFXM_ElectricGround";
		case Explosion.exp2:
			return "CFXM_ElectricityBall";
		case Explosion.exp3:
			return "CFXM_ElectricityBolt";
		case Explosion.exp4:
			return "CFXM_Explosion";
		case Explosion.exp5:
			return "smoke_new";
		case Explosion.exp6:
			return "CFXM_SmokeExplosion";
		case Explosion.exp7:
			return "pyro_flames";
		case Explosion.exp8:
			return "CFXM_DoubleFlameA";
		case Explosion.exp9:
			return "CFXM3_Hit_Electric_A_Air";
		case Explosion.exp10:
			return "CFXM_CircularLightWall";
		case Explosion.exp11:
			return "CFXM_LightWall";
		case Explosion.exp12:
			return "CFXM_Flash";
		case Explosion.exp13:
			return "CFXM_Poof";
		case Explosion.exp14:
			return "CFXM_Virus";
		case Explosion.exp15:
			return "CFXM_SmokePuffs";
		case Explosion.exp16:
			return "CFXM_Slash";
		case Explosion.exp17:
			return "CFXM_Splash";
		case Explosion.exp18:
			return "CFXM_Fountain";
		case Explosion.exp19:
			return "CFXM_SoftStar";
		case Explosion.exp20:
			return "CFXM_MagicPoof";
		case Explosion.exp21:
			return "CFXM_Thunderbolt";
		case Explosion.exp22:
			return "CFXM_SoftStar";
		case Explosion.exp23:
			return "CFXM_SpikyAura_Sphere";
		case Explosion.exp24:
			return "CFXM_Firework";
		case Explosion.exp25:
			return "CFXM_GroundExplosion";
		case Explosion.exp26:
			return "CFXM_GroundExplosion_Text";
		case Explosion.exp27:
			return "CFXM_GroundHit";
		case Explosion.exp28:
			return "CFXM4 Explosion SoftEdge Air";
		case Explosion.exp29:
			return "CFXM4 Wave Explosion (Blue)";
		case Explosion.exp30:
			return "CFX2_DoubleFireWall A";
		case Explosion.exp31:
			return "CFX4 Firework A (Orange)";
		default:
			UnityEngine.Debug.Log("invalid name!");
			return null;
		}
	}

	public static string Name(Bombs bomb)
	{
		switch (bomb)
		{
		case Bombs.bomb0:
			return "Bomb_0";
		case Bombs.bomb1:
			return "Bomb_1";
		case Bombs.bomb2:
			return "Bomb_2";
		case Bombs.bomb3:
			return "Bomb_3";
		case Bombs.bomb4:
			return "Bomb_4";
		case Bombs.bomb5:
			return "Bomb_5";
		case Bombs.bullet:
			return "Bullet";
		case Bombs.tankrocket:
			return "TankRocket";
		case Bombs.ice:
			return "Ice";
		case Bombs.aibullet:
			return "AIBullet";
		case Bombs.bomblip:
			return "Bomb_lipucka";
		case Bombs.fuelCarBomb:
			return "TransferCarFireBomb";
		case Bombs.copterBomb:
			return "CopterBomb";
		case Bombs.bombCepel:
			return "bombCepel";
		case Bombs.bombAirStike:
			return "bombAirStike";
		case Bombs.smallerBombCivil1:
			return "Civilians_1_small";
		case Bombs.smallerBombEnemy1:
			return "Enemy_1_small";
		case Bombs.smallerBombCivil2:
			return "Civilians_2_small";
		case Bombs.smallerBombCivil3:
			return "Civilians_3_small";
		case Bombs.smallerBombEnemy2:
			return "Enemy_2_small";
		case Bombs.smallerBombEnemy3:
			return "Enemy_3_small";
		case Bombs.freezeRay:
			return "freeze_projectile";
		case Bombs.smallerBombEnemyEster:
			return "Enemy_Ester_small";
		case Bombs.smallerBombCivilEster:
			return "Civilians_Ester_small";
		case Bombs.smallerBombEnemyUndeground:
			return "EnemyUndeground_1_small";
		case Bombs.smallerBombEnemy2Undeground:
			return "Enemy_5_small";
		default:
			UnityEngine.Debug.Log("invalid name!");
			return null;
		}
	}

	public static string Name(Bonus bonus)
	{
		switch (bonus)
		{
		case Bonus.none:
			UnityEngine.Debug.Log("none!");
			return null;
		case Bonus.flip:
			return "FlipEffect";
		case Bonus.health:
			return "HealthEffect";
		case Bonus.turbo:
			return "TurboEffect";
		case Bonus.bomb:
			return "BombEffect";
		case Bonus.hp:
			return "AIHealthBar";
		case Bonus.spark:
			return "spark";
		case Bonus.bombcar:
			return "bomb_car";
		case Bonus.policecar:
			return "Police_car";
		case Bonus.copter:
			return "enemy_Copter";
		case Bonus.cepel:
			return "enemy_Cepelin";
		case Bonus.boss1:
			return "Police_car_Boss_1";
		case Bonus.boss2:
			return "Police_car_Boss_2";
		case Bonus.boss3:
			return "Police_car_Boss_3";
		case Bonus.bossTriger:
			return "TrigerForBoss";
		case Bonus.ruby:
			return "Collectible_Object_ruby";
		case Bonus.pyro:
			return "Enemy_pyro";
		case Bonus.bomb_pyro:
			return "Bomb_fire_for_pyro";
		case Bonus.portal:
			return "portalReviwe";
		case Bonus.enemyairstrike:
			return "Enemy_jetFighter";
		case Bonus.CivilEster:
			return "Civilian_easter";
		case Bonus.boss1Undeground:
			return "Undergound_car_boss";
		case Bonus.boss2Undeground:
			return "Undergound2_car_boss";
		default:
			UnityEngine.Debug.Log("invalid name!");
			return null;
		}
	}

	public static string Name(Trash trash)
	{
		switch (trash)
		{
		case Trash.none:
			UnityEngine.Debug.Log("none!");
			return null;
		case Trash.Astroaut:
			return "Astronaut";
		case Trash.AstroSheep:
			return "AstroSheep";
		case Trash.Balloon:
			return "Balloon";
		case Trash.Orbiter:
			return "Orbiter";
		case Trash.UFO:
			return "UFO";
		default:
			UnityEngine.Debug.Log("invalid name!");
			return null;
		}
	}

	public static string Name(Scraps scrup)
	{
		switch (scrup)
		{
		case Scraps.none:
			return null;
		case Scraps.scrap1:
			return "Obj_1";
		case Scraps.scrap2:
			return "Obj_2";
		case Scraps.scrap3:
			return "Obj_3";
		case Scraps.scrap4:
			return "Obj_4";
		case Scraps.scrap5:
			return "Obj_5";
		case Scraps.scrap6:
			return "Obj_6";
		case Scraps.scrap7:
			return "Obj_7";
		case Scraps.scrap8:
			return "Obj_8";
		case Scraps.scrap9:
			return "Obj_9";
		case Scraps.scrap10:
			return "Obj_10";
		case Scraps.trash_1:
			return "trash_1";
		case Scraps.trash_2:
			return "trash_2";
		case Scraps.trash_3:
			return "trash_3";
		case Scraps.trash_4:
			return "trash_4";
		case Scraps.trash_5:
			return "trash_5";
		case Scraps.trash_6:
			return "trash_6";
		case Scraps.trash_7:
			return "trash_7";
		case Scraps.trash_8:
			return "trash_8";
		case Scraps.trash_9:
			return "trash_9";
		case Scraps.trash_10:
			return "trash_10";
		case Scraps.trash_11:
			return "trash_11";
		case Scraps.trash_12:
			return "trash_12";
		case Scraps.trash_13:
			return "trash_13";
		case Scraps.ice:
			return "IceScrap";
		case Scraps.phys_scr_car1:
			return "phys_scr_car1";
		case Scraps.phys_scr_car2:
			return "phys_scr_car2";
		case Scraps.phys_scr_car3:
			return "phys_scr_car3";
		case Scraps.phys_scr_car4:
			return "phys_scr_car4";
		case Scraps.phys_scr_car5:
			return "phys_scr_car5";
		case Scraps.scr_car1:
			return "scr_car1";
		case Scraps.scr_car2:
			return "scr_car2";
		case Scraps.scr_car3:
			return "scr_car3";
		case Scraps.scr_car4:
			return "scr_car4";
		case Scraps.scr_car5:
			return "scr_car5";
		case Scraps.scr_fire_bomb:
			return "fire_bomb_scrap";
		case Scraps.scr_cluster_bomb:
			return "cluster_bomb_scrap";
		case Scraps.enemyScrap1:
			return "Scrap_Enemy_Deco 1";
		case Scraps.enemyScrap2:
			return "Scrap_Enemy_Deco 2";
		case Scraps.enemyScrap3:
			return "Scrap_Enemy_Deco 3";
		case Scraps.enemyScrap4:
			return "Scrap_Enemy_Deco 4";
		case Scraps.civicScrap1:
			return "Scrap_civic_Deco";
		case Scraps.CarScrap:
			return "Scrap_Car_Deco";
		case Scraps.CarScrapPolice:
			return "Scrap_Car_Deco_Police";
		case Scraps.CarScrapPoliceUnder:
			return "Scrap_Car_Deco_Police_Under";
		case Scraps.pitDoor:
			return "PitDoorScrap";
		case Scraps.pitDoor1:
			return "PitDoorScrap_1";
		case Scraps.CageScr1:
			return "CageScrap_1";
		case Scraps.CageScr2:
			return "CageScrap_2";
		case Scraps.CageScr3:
			return "CageScrap_3";
		case Scraps.SpiderScr1:
			return "SpiderScrap_1";
		case Scraps.SpiderScr2:
			return "SpiderScrap_2";
		case Scraps.SpiderScr3:
			return "SpiderScrap_3";
		case Scraps.CageScrPoliceCar1:
			return "cage_scp01";
		case Scraps.CageScrPoliceCar2:
			return "cage_scp02";
		case Scraps.CageScrPoliceCar3:
			return "cage_scp03";
		case Scraps.CageScrPoliceCar4:
			return "cage_scp04";
		case Scraps.CageScrPoliceCar5:
			return "cage_scp05";
		default:
			UnityEngine.Debug.Log("invalid name!");
			return null;
		}
	}

	public static void Init()
	{
		if (instance.isCached && !instance.init)
		{
			instance.ReadFromSerializedList();
			instance.init = true;
		}
		if (!instance.init)
		{
			instance.Add(Name(Explosion.exp2), 5);
			instance.Add(Name(Explosion.exp7), 5);
			instance.Add(Name(Explosion.exp9), 5);
			instance.Add(Name(Explosion.exp25), 5);
			instance.Add(Name(Explosion.exp26), 5);
			instance.Add(Name(Explosion.exp28), 5);
			instance.Add(Name(Bonus.flip), 2);
			instance.Add(Name(Bonus.health), 2);
			instance.Add(Name(Bonus.turbo), 2);
			instance.Add(Name(Bonus.bomb), 2);
			instance.Add(Name(Bonus.bomb_pyro), 30);
			instance.Add(Name(Bombs.bomb0), 5);
			instance.Add(Name(Bombs.bomb1), 5);
			instance.Add(Name(Bombs.bomb2), 5);
			instance.Add(Name(Bombs.bomb3), 5);
			instance.Add(Name(Bombs.bomb4), 5);
			instance.Add(Name(Bombs.bomb5), 5);
			instance.Add(Name(Bombs.bomblip), 7);
			instance.Add(Name(Bombs.freezeRay), 2);
			instance.Add(Name(Bonus.portal), 2);
			instance.Add(Name(Bombs.bombAirStike), 10);
			instance.Add(Name(Bonus.enemyairstrike));
			instance.Add(Name(Scraps.scrap1), 10);
			instance.Add(Name(Scraps.scrap2), 10);
			instance.Add(Name(Scraps.scrap3), 10);
			instance.Add(Name(Scraps.scrap4), 10);
			instance.Add(Name(Scraps.scrap5), 10);
			instance.Add(Name(Scraps.scrap6), 10);
			instance.Add(Name(Scraps.scrap7), 10);
			instance.Add(Name(Scraps.scrap8), 10);
			instance.Add(Name(Scraps.scrap9), 10);
			instance.Add(Name(Scraps.scrap10), 10);
			instance.Add(Name(Bonus.pyro), 2);
			instance.Add(Name(Scraps.trash_8), 10);
			instance.Add(Name(Scraps.ice), 30);
			instance.Add(Name(Scraps.pitDoor), 10);
			instance.Add(Name(Scraps.pitDoor1), 10);
			instance.Add(Name(Scraps.CageScr1), 7);
			instance.Add(Name(Scraps.CageScr2), 7);
			instance.Add(Name(Scraps.CageScr3), 7);
			instance.Add(Name(Scraps.SpiderScr1), 5);
			instance.Add(Name(Scraps.SpiderScr2), 5);
			instance.Add(Name(Scraps.SpiderScr3), 5);
			instance.Add(Name(Scraps.phys_scr_car1));
			instance.Add(Name(Scraps.phys_scr_car2));
			instance.Add(Name(Scraps.phys_scr_car3));
			instance.Add(Name(Scraps.phys_scr_car4));
			instance.Add(Name(Scraps.phys_scr_car5));
			instance.Add(Name(Scraps.scr_car1));
			instance.Add(Name(Scraps.scr_car2));
			instance.Add(Name(Scraps.scr_car3));
			instance.Add(Name(Scraps.scr_car4));
			instance.Add(Name(Scraps.scr_car5));
			instance.Add(Name(Scraps.scr_fire_bomb), 10);
			instance.Add(Name(Scraps.scr_cluster_bomb), 10);
			instance.Add(Name(Bombs.bullet), 10);
			instance.Add(Name(Bombs.ice), 15);
			instance.Add(Name(Bombs.aibullet), 5);
			instance.Add(Name(Scraps.enemyScrap1), 20);
			instance.Add(Name(Scraps.enemyScrap2), 20);
			instance.Add(Name(Scraps.enemyScrap3), 20);
			instance.Add(Name(Scraps.enemyScrap4), 20);
			instance.Add(Name(Scraps.civicScrap1), 20);
			instance.Add(Name(Scraps.CarScrap), 10);
			instance.Add("Scrap_Dynamic_Deco", 20);
			instance.Add(Name(Bonus.hp), 10);
			instance.Add(Name(Bonus.spark), 20);
			instance.Add(Name(Bombs.fuelCarBomb), 20);
			instance.Add(Name(Bombs.copterBomb), 10);
			instance.Add(Name(Bonus.bombcar), 7);
			instance.Add(Name(Bonus.policecar), 5);
			instance.Add(Name(Bonus.copter));
			instance.Add(Name(Bombs.bombCepel), 10);
			instance.Add(Name(Bonus.cepel));
			instance.Add(Name(Bonus.boss1));
			instance.Add(Name(Bonus.boss2));
			instance.Add(Name(Bonus.boss3));
			instance.Add(Name(Bonus.bossTriger));
			instance.Add(Name(Bombs.smallerBombCivil1), 5);
			instance.Add(Name(Bombs.smallerBombEnemy1), 5);
			instance.Add(Name(Bombs.smallerBombCivil2), 5);
			instance.Add(Name(Bombs.smallerBombCivil3), 5);
			instance.Add(Name(Bombs.smallerBombEnemy2), 5);
			instance.Add(Name(Bombs.smallerBombEnemy3), 5);
			instance.Add(Name(Bonus.ruby), 50);
			instance.Add(Name(Bombs.smallerBombCivilEster), 5);
			instance.Add(Name(Bombs.smallerBombEnemyEster), 5);
			instance.Add(Name(Bombs.smallerBombEnemyUndeground), 5);
			instance.Add(Name(Bombs.smallerBombEnemy2Undeground), 5);
			instance.Add(Name(Bonus.boss1Undeground));
			instance.Add(Name(Bonus.boss2Undeground));
			instance.Add("Scrap_Dynamic_Deco_Undeground2", 20);
			instance.init = true;
		}
	}

	public static GameObject GetFuelBomb(Vector3 vec)
	{
		GameObject @object = instance.GetObject(Name(Bombs.fuelCarBomb));
		@object.SetActive(value: true);
		@object.transform.position = vec;
		return @object;
	}

	public static GameObject GetCopterBomb(Vector3 vec)
	{
		GameObject @object = instance.GetObject(Name(Bombs.copterBomb));
		@object.SetActive(value: true);
		@object.transform.position = vec;
		return @object;
	}

	public static GameObject GetSmaller(bool civil)
	{
		if (Progress.shop.TestFor9)
		{
			switch (Progress.levels.active_pack)
			{
			case 1:
				return instance.GetObject(Name(Bombs.smallerBombEnemyUndeground));
			case 2:
				return instance.GetObject(Name(Bombs.smallerBombEnemy2Undeground));
			}
		}
		if (Progress.shop.EsterLevelPlay)
		{
			if (civil)
			{
				return instance.GetObject(Name(Bombs.smallerBombCivilEster));
			}
			return instance.GetObject(Name(Bombs.smallerBombEnemyEster));
		}
		switch (Progress.levels.active_pack)
		{
		case 1:
			if (civil)
			{
				return instance.GetObject(Name(Bombs.smallerBombCivil1));
			}
			return instance.GetObject(Name(Bombs.smallerBombEnemy1));
		case 2:
			if (civil)
			{
				return instance.GetObject(Name(Bombs.smallerBombCivil2));
			}
			return instance.GetObject(Name(Bombs.smallerBombEnemy2));
		case 3:
			if (civil)
			{
				return instance.GetObject(Name(Bombs.smallerBombCivil3));
			}
			return instance.GetObject(Name(Bombs.smallerBombEnemy3));
		default:
			return null;
		}
	}

	public static GameObject GetHPbar()
	{
		GameObject @object = instance.GetObject(Name(Bonus.hp));
		@object.SetActive(value: true);
		return @object;
	}

	public static GameObject GameOBJECT(Bonus bon, Vector3 position)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(bon), position);
		gameObject.SetActive(value: true);
		return gameObject;
	}

	public static GameObject GameOBJECT(Bombs bon, Vector3 position)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(bon), position);
		gameObject.SetActive(value: true);
		return gameObject;
	}

	public static GameObject GameOBJECT(Explosion bon, Vector3 position)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(bon), position);
		gameObject.SetActive(value: true);
		return gameObject;
	}

	public static GameObject GameOBJECT(Scraps bon, Vector3 position)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(bon), position);
		gameObject.SetActive(value: true);
		return gameObject;
	}

	public static void SetParticleSystemSortingLayer(GameObject g, string layer)
	{
		if (g != null && g.GetComponent<Renderer>() != null && g.GetComponent<Renderer>().sortingLayerName != layer)
		{
			g.GetComponent<Renderer>().sortingLayerName = ((layer != null) ? layer : instance.defaultLayer);
			Renderer[] componentsInChildren = g.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].sortingLayerName = ((layer != null) ? layer : instance.defaultLayer);
			}
		}
	}

	public static GameObject Animate(string exp, Vector3 position, string layer = null)
	{
		GameObject gameObject = instance.spawnAtPoint(exp, position);
		SetParticleSystemSortingLayer(gameObject, layer);
		return gameObject;
	}

	public static GameObject Animate(Explosion exp, Vector3 position, string layer = null)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(exp), position);
		SetParticleSystemSortingLayer(gameObject, layer);
		return gameObject;
	}

	public static GameObject Animate(Trash exp, Vector3 position, string layer = null)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(exp), position);
		SetParticleSystemSortingLayer(gameObject, layer);
		return gameObject;
	}

	public static GameObject Animate(Explosion exp, Transform transform, string layer = null)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(exp), transform);
		SetParticleSystemSortingLayer(gameObject, layer);
		return gameObject;
	}

	public static GameObject Animate(Bonus bon, Transform transform, string layer = null)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(bon), transform);
		SetParticleSystemSortingLayer(gameObject, layer);
		return gameObject;
	}

	public static GameObject Animate(Bombs bon, Vector3 transform, string layer = null)
	{
		GameObject gameObject = instance.spawnAtPoint(Name(bon), transform);
		SetParticleSystemSortingLayer(gameObject, layer);
		return gameObject;
	}

	public static void ScrapExample(Scraps scrap)
	{
		if (scrap != 0)
		{
			for (int i = 0; i < 8; i++)
			{
				Scrap(scrap, Vector3.one, -90 + i * 22 + Random.Range(-9, 9), 5 + Random.Range(-1, 2), 2f);
			}
		}
	}

	public static GameObject Scrap(Scraps scrap, Vector2 startPosition, float angle, float force, float lifeTime = 10f, bool rotate = true, float gravity = 0.1f, string layer = null)
	{
		GameObject @object = instance.GetObject(Name(scrap));
		return ScrapCommonLogic.instance.animateScrap(@object, startPosition, angle, force, lifeTime, rotate, gravity);
	}

	public static GameObject Scrap(Scraps scrap, Vector2 startPosition, Vector2 force, float lifeTime = 10f)
	{
		GameObject @object = instance.GetObject(Name(scrap));
		return ScrapCommonLogic.instance.animateScrap(@object, startPosition, force, lifeTime);
	}

	public static void ScrapEnemy(Vector2 position, int enemyNum, int location, bool isCivic, string scrap1, string scrap2, string scrap3, string scrap4, string scrap5, Vector3 RGB)
	{
		if (Progress.shop.EsterLevelPlay)
		{
			location = 2;
		}
		List<string> list = new List<string>();
		if (scrap1 != string.Empty && scrap1 != null && scrap1 != strEmpty)
		{
			list.Add(scrap1);
		}
		if (scrap2 != string.Empty && scrap2 != null && scrap2 != strEmpty)
		{
			list.Add(scrap2);
		}
		if (scrap3 != string.Empty && scrap3 != null && scrap3 != strEmpty)
		{
			list.Add(scrap3);
		}
		if (scrap4 != string.Empty && scrap4 != null && scrap4 != strEmpty)
		{
			list.Add(scrap4);
		}
		if (scrap5 != string.Empty && scrap5 != null && scrap5 != strEmpty)
		{
			list.Add(scrap5);
		}
		for (int i = 1; i <= list.Count; i++)
		{
			switch (location)
			{
			case 1:
				if (!isCivic)
				{
					GameObject object5 = instance.GetObject(Name(Scraps.enemyScrap1));
					if (object5 != null)
					{
						tk2dSprite component = object5.GetComponent<tk2dSprite>();
						string sprite5 = list[i - 1];
						if (Progress.levels.InUndeground)
						{
							tk2dSpriteCollectionData tk2dSpriteCollectionData6 = component.Collection = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>("undergroundEnemycars");
						}
						component.SetSprite(sprite5);
						ScrapCommonLogic.instance.animateScrap(object5, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
				}
				else
				{
					GameObject object6 = instance.GetObject(Name(Scraps.civicScrap1));
					if (object6 != null)
					{
						tk2dSprite component = object6.GetComponent<tk2dSprite>();
						string sprite6 = list[i - 1];
						component.SetSprite(sprite6);
						component.color = new Color(RGB.x, RGB.y, RGB.z);
						ScrapCommonLogic.instance.animateScrap(object6, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
				}
				break;
			case 2:
				if (!isCivic)
				{
					GameObject object3 = instance.GetObject(Name(Scraps.enemyScrap2));
					if (object3 != null)
					{
						tk2dSprite component = object3.GetComponent<tk2dSprite>();
						string sprite3 = list[i - 1];
						if (Progress.shop.EsterLevelPlay)
						{
							tk2dSpriteCollectionData tk2dSpriteCollectionData2 = component.Collection = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>("event_objects");
						}
						if (Progress.levels.InUndeground)
						{
							tk2dSpriteCollectionData tk2dSpriteCollectionData4 = component.Collection = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>("undergroundEnemycars");
						}
						component.SetSprite(sprite3);
						ScrapCommonLogic.instance.animateScrap(object3, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
				}
				else
				{
					GameObject object4 = instance.GetObject(Name(Scraps.civicScrap1));
					if (object4 != null)
					{
						tk2dSprite component = object4.GetComponent<tk2dSprite>();
						string sprite4 = list[i - 1];
						component.SetSprite(sprite4);
						component.color = new Color(RGB.x, RGB.y, RGB.z);
						ScrapCommonLogic.instance.animateScrap(object4, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
				}
				break;
			case 3:
			{
				if (!isCivic)
				{
					GameObject object7 = instance.GetObject(Name(Scraps.enemyScrap3));
					if (object7 != null)
					{
						tk2dSprite component = object7.GetComponent<tk2dSprite>();
						string sprite7 = list[i - 1];
						component.SetSprite(sprite7);
						ScrapCommonLogic.instance.animateScrap(object7, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
					break;
				}
				GameObject object8 = instance.GetObject(Name(Scraps.civicScrap1));
				if (object8 != null)
				{
					tk2dSprite component = object8.GetComponent<tk2dSprite>();
					string sprite8 = list[i - 1];
					component.SetSprite(sprite8);
					component.color = new Color(RGB.x, RGB.y, RGB.z);
					ScrapCommonLogic.instance.animateScrap(object8, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
				break;
			}
			case 4:
			{
				if (!isCivic)
				{
					GameObject @object = instance.GetObject(Name(Scraps.enemyScrap4));
					if (@object != null)
					{
						tk2dSprite component = @object.GetComponent<tk2dSprite>();
						string sprite = list[i - 1];
						component.SetSprite(sprite);
						ScrapCommonLogic.instance.animateScrap(@object, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
					break;
				}
				GameObject object2 = instance.GetObject(Name(Scraps.civicScrap1));
				if (object2 != null)
				{
					tk2dSprite component = object2.GetComponent<tk2dSprite>();
					string sprite2 = list[i - 1];
					component.SetSprite(sprite2);
					component.color = new Color(RGB.x, RGB.y, RGB.z);
					ScrapCommonLogic.instance.animateScrap(object2, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
				break;
			}
			}
		}
	}

	public static void ScrapEnemyNumNum(Vector2 position, int enemyNum, bool isCivic, int location, string scrap1, string scrap2, string scrap3, string scrap4, string scrap5, Vector3 RGB)
	{
		if (Progress.shop.EsterLevelPlay)
		{
			location = 2;
			enemyNum = 2;
		}
		List<string> list = new List<string>();
		if (scrap1 != string.Empty && scrap1 != null && scrap1 != strEmpty)
		{
			list.Add(scrap1);
		}
		if (scrap2 != string.Empty && scrap2 != null && scrap2 != strEmpty)
		{
			list.Add(scrap2);
		}
		if (scrap3 != string.Empty && scrap3 != null && scrap3 != strEmpty)
		{
			list.Add(scrap3);
		}
		if (scrap4 != string.Empty && scrap4 != null && scrap4 != strEmpty)
		{
			list.Add(scrap4);
		}
		if (scrap5 != string.Empty && scrap5 != null && scrap5 != strEmpty)
		{
			list.Add(scrap5);
		}
		if (list.Count <= 0)
		{
			return;
		}
		int num = Random.Range(1, list.Count);
		switch (location)
		{
		case 1:
			if (!isCivic)
			{
				GameObject object5 = instance.GetObject(Name(Scraps.enemyScrap1));
				if (object5 != null)
				{
					tk2dSprite component = object5.GetComponent<tk2dSprite>();
					string sprite5 = list[num - 1];
					if (Progress.levels.InUndeground)
					{
						tk2dSpriteCollectionData tk2dSpriteCollectionData6 = component.Collection = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>("undergroundEnemycars");
					}
					component.SetSprite(sprite5);
					ScrapCommonLogic.instance.animateScrap(object5, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
			}
			else
			{
				GameObject object6 = instance.GetObject(Name(Scraps.civicScrap1));
				if (object6 != null)
				{
					tk2dSprite component = object6.GetComponent<tk2dSprite>();
					string sprite6 = list[num - 1];
					component.SetSprite(sprite6);
					component.color = new Color(RGB.x, RGB.y, RGB.z);
					ScrapCommonLogic.instance.animateScrap(object6, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
			}
			break;
		case 2:
			if (!isCivic)
			{
				GameObject object3 = instance.GetObject(Name(Scraps.enemyScrap2));
				if (object3 != null)
				{
					tk2dSprite component = object3.GetComponent<tk2dSprite>();
					string sprite3 = list[num - 1];
					if (Progress.shop.EsterLevelPlay)
					{
						tk2dSpriteCollectionData tk2dSpriteCollectionData2 = component.Collection = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>("event_objects");
					}
					if (Progress.levels.InUndeground)
					{
						tk2dSpriteCollectionData tk2dSpriteCollectionData4 = component.Collection = tk2dSystem.LoadResourceByName<tk2dSpriteCollectionData>("undergroundEnemycars");
					}
					component.SetSprite(sprite3);
					ScrapCommonLogic.instance.animateScrap(object3, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
			}
			else
			{
				GameObject object4 = instance.GetObject(Name(Scraps.civicScrap1));
				if (object4 != null)
				{
					tk2dSprite component = object4.GetComponent<tk2dSprite>();
					string sprite4 = list[num - 1];
					component.SetSprite(sprite4);
					component.color = new Color(RGB.x, RGB.y, RGB.z);
					ScrapCommonLogic.instance.animateScrap(object4, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
			}
			break;
		case 3:
		{
			if (!isCivic)
			{
				GameObject object7 = instance.GetObject(Name(Scraps.enemyScrap3));
				if (object7 != null)
				{
					tk2dSprite component = object7.GetComponent<tk2dSprite>();
					string sprite7 = list[num - 1];
					component.SetSprite(sprite7);
					ScrapCommonLogic.instance.animateScrap(object7, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
				break;
			}
			GameObject object8 = instance.GetObject(Name(Scraps.civicScrap1));
			if (object8 != null)
			{
				tk2dSprite component = object8.GetComponent<tk2dSprite>();
				string sprite8 = list[num - 1];
				component.SetSprite(sprite8);
				component.color = new Color(RGB.x, RGB.y, RGB.z);
				ScrapCommonLogic.instance.animateScrap(object8, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
			}
			break;
		}
		case 4:
		{
			if (!isCivic)
			{
				GameObject @object = instance.GetObject(Name(Scraps.enemyScrap4));
				if (@object != null)
				{
					tk2dSprite component = @object.GetComponent<tk2dSprite>();
					string sprite = list[num - 1];
					component.SetSprite(sprite);
					ScrapCommonLogic.instance.animateScrap(@object, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
				break;
			}
			GameObject object2 = instance.GetObject(Name(Scraps.civicScrap1));
			if (object2 != null)
			{
				tk2dSprite component = object2.GetComponent<tk2dSprite>();
				string sprite2 = list[num - 1];
				component.SetSprite(sprite2);
				component.color = new Color(RGB.x, RGB.y, RGB.z);
				ScrapCommonLogic.instance.animateScrap(object2, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
			}
			break;
		}
		}
	}

	public static void ScrapForMainCar(Vector2 position, int enemyNum, string scrap1, string scrap2, string scrap3, string scrap4, string scrap5, string scrap6, string scrap7, string scrap8)
	{
		List<string> list = new List<string>();
		list.Add(scrap1);
		list.Add(scrap2);
		list.Add(scrap3);
		list.Add(scrap4);
		list.Add(scrap5);
		list.Add(scrap6);
		list.Add(scrap7);
		list.Add(scrap8);
		for (int i = 1; i <= 8; i++)
		{
			GameObject gameObject = null;
			gameObject = ((Progress.shop.activeCar != 6 && Progress.shop.activeCar != 7 && Progress.shop.activeCar != 8 && Progress.shop.activeCar != 9 && Progress.shop.activeCar != 10 && Progress.shop.activeCar != 11 && Progress.shop.activeCar != 12 && Progress.shop.activeCar != 13) ? instance.GetObject(Name(Scraps.CarScrap)) : ((Progress.shop.activeCar == 13) ? instance.GetObject(Name(Scraps.CarScrapPoliceUnder)) : instance.GetObject(Name(Scraps.CarScrapPolice))));
			if (gameObject != null)
			{
				tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
				string sprite = list[i - 1];
				component.SetSprite(sprite);
				ScrapCommonLogic.instance.animateScrap(gameObject, position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
			}
		}
	}

	public static void ScrapDynamicDeco(ObjectActor actor)
	{
		for (int i = 0; i < actor.scrapsDynamicObject.Count; i++)
		{
			for (int j = 0; j < actor.scrapsDynamicObject[i].num; j++)
			{
				GameObject gameObject = null;
				gameObject = ((!Progress.shop.Undeground2) ? instance.GetObject("Scrap_Dynamic_Deco") : instance.GetObject("Scrap_Dynamic_Deco_Undeground2"));
				tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
				if (component.SetSprite(actor.scrapsDynamicObject[i].scrap.ToString()))
				{
					tk2dSprite component2 = actor.gameObject.GetComponent<tk2dSprite>();
					if ((bool)component2)
					{
						component.scale = component2.scale;
					}
					ScrapCommonLogic.instance.animateScrap(gameObject, actor.transform.position, Random.Range(-60, 120), Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
				else
				{
					UnityEngine.Debug.LogWarning(actor.gameObject.name + " not found scrap: " + actor.scrapsDynamicObject[i].scrap);
				}
			}
		}
	}
}
