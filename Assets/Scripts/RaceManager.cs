using System.Collections.Generic;
using UnityEngine;

public class RaceManager : RaceManagerBase
{
	private static RaceManager _instance;

	private int _rubins;

	private int _rubinsAI;

	private int _rubinsBonus;

	public List<Car2DAIController> activeAIs = new List<Car2DAIController>();

	public static RaceManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (RaceManager)Object.FindObjectOfType(typeof(RaceManager));
				if (_instance == null)
				{
					GameObject gameObject = new GameObject();
					gameObject.name = "_RaceManager";
					_instance = gameObject.AddComponent<RaceManager>();
				}
			}
			return _instance;
		}
	}

	public int Rubins
	{
		get
		{
			return _rubins;
		}
		set
		{
			_rubins = value;
		}
	}

	public int RubinsAI
	{
		get
		{
			return _rubinsAI;
		}
		set
		{
			_rubinsAI = value;
		}
	}

	public int RubinsBonus
	{
		get
		{
			return _rubinsBonus;
		}
		set
		{
			_rubinsBonus = value;
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public void Reset()
	{
		activeAIs.Clear();
		_rubins = 0;
		_rubinsAI = 0;
		_rubinsBonus = 0;
	}

	public override void Update()
	{
		base.Update();
	}

	public override void OnPositionChanged()
	{
		base.OnPositionChanged();
	}
}
