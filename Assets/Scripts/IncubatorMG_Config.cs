using System.Collections.Generic;
using UnityEngine;

public class IncubatorMG_Config : MonoBehaviour
{
	public List<ConfigMGIncubator.Evo.Stage> GetStage()
	{
		return ConfigMGIncubator.instance.ForEvo[Progress.shop.Incubator_EvoStage - 1].Stages;
	}

	public int GetStarsToWin()
	{
		return ConfigMGIncubator.instance.ForEvo[Progress.shop.Incubator_EvoStage - 1].StarsToWin;
	}

	public int GetCopDmgStars()
	{
		return ConfigMGIncubator.instance.ForEvo[Progress.shop.Incubator_EvoStage - 1].CopDmgStars;
	}

	public int GetCivilDmgStars()
	{
		return ConfigMGIncubator.instance.ForEvo[Progress.shop.Incubator_EvoStage - 1].CivilDmgStars;
	}

	public int GetStoneDmgStars()
	{
		return ConfigMGIncubator.instance.ForEvo[Progress.shop.Incubator_EvoStage - 1].StoneDmgStars;
	}
}
