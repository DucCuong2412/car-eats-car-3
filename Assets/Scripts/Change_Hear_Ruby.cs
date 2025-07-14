using UnityEngine;

public class Change_Hear_Ruby : MonoBehaviour
{
	public tk2dSprite TK2DS;

	private void Update()
	{
		if (Progress.shop.EsterLevelPlay)
		{
			TK2DS.SetSprite("easter_egg_olya_002");
		}
		else
		{
			TK2DS.SetSprite("ruby_0");
		}
	}
}
