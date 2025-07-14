using SmartLocalization;
using UnityEngine;

public class LocalizedWords : MonoBehaviour
{
	public string armor;

	public string turbo;

	public string wheels;

	public string engine;

	public string bombs;

	public string armorDescription;

	public string turboDescription;

	public string wheelsDescription;

	public string engineDescription;

	public string bombsDescription;

	public string magnet;

	public string ram;

	public string recharger;

	public string shield;

	public string regenerator;

	public string magnetDescription;

	public string ramDescription;

	public string rechargerDescription;

	public string shieldDescription;

	public string regeneratorDescription;

	public string tank;

	public string supergun;

	public string antigravs;

	public string megaturbo;

	public string godmode;

	public string tankDescription;

	public string supergunDescription;

	public string antigravsDescription;

	public string megaturboDescription;

	public string godmodeDescription;

	public string close;

	public string buy;

	public string bought;

	public string equip;

	public string unequip;

	public string maximum;

	public string KetTextValue(string key)
	{
		return LanguageManager.Instance.GetTextValue(key);
	}
}
