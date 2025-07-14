using UnityEngine;
using xmlClassTemplate;

public class ShopSettings : MonoBehaviour
{
	public class ShopData
	{
		public int[] health = new int[6];

		public int[] turbo = new int[6];

		public int[] wheels = new int[6];

		public int[] engine = new int[6];

		public int[] weapons = new int[6];

		public int[] gadgetsPrices = new int[5];

		public int[] boostersPrices = new int[3];

		public int[] boostersValue = new int[3];
	}

	public static ShopData GetShopData()
	{
		TextAsset textAsset = Resources.Load("shopData") as TextAsset;
		return XML.Deserialize<ShopData>(textAsset.text);
	}
}
