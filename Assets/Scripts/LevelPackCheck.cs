using UnityEngine;

public class LevelPackCheck : MonoBehaviour
{
	public LevelIconCheck[] _iconCheck = new LevelIconCheck[10];

	public LevelIconCheck IconCheck(int i)
	{
		if (_iconCheck[i] == null)
		{
			InitIcons();
		}
		return _iconCheck[i];
	}

	public void InitIcons()
	{
		LevelIconCheck[] componentsInChildren = base.gameObject.GetComponentsInChildren<LevelIconCheck>();
		foreach (LevelIconCheck levelIconCheck in componentsInChildren)
		{
			_iconCheck[levelIconCheck.Number] = levelIconCheck;
		}
	}
}
