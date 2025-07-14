using System.Collections.Generic;
using UnityEngine;

public class IncubatorMG_Pool : MonoBehaviour
{
	public List<IncubatorMG_InterectObj> InterectObjs;

	public List<IncubatorMG_InterectObj> DecorsObjs;

	public IncubatorMG_InterectObj GetInterectObj()
	{
		int count = InterectObjs.Count;
		for (int i = 0; i < count; i++)
		{
			if (!InterectObjs[i].gameObject.activeSelf && (InterectObjs[i].Tupe == IncubatorMG_Controller.Tupe.CarCivil || InterectObjs[i].Tupe == IncubatorMG_Controller.Tupe.Stone || InterectObjs[i].Tupe == IncubatorMG_Controller.Tupe.CarCop || InterectObjs[i].Tupe == IncubatorMG_Controller.Tupe.Star))
			{
				return InterectObjs[i];
			}
		}
		UnityEngine.Debug.Log("IncubatorMG - not enath GetInterectObj!!!!!!");
		return null;
	}

	public IncubatorMG_InterectObj GetDecor()
	{
		int count = DecorsObjs.Count;
		for (int i = 0; i < count; i++)
		{
			if (!DecorsObjs[i].gameObject.activeSelf)
			{
				return DecorsObjs[i];
			}
		}
		UnityEngine.Debug.Log("IncubatorMG - not enath GetDecor!!!!!!");
		return null;
	}
}
