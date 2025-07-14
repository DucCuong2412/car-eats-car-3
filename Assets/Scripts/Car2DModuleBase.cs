using UnityEngine;

public abstract class Car2DModuleBase : MonoBehaviour
{
	private bool _enabled;

	private bool _inited;

	public bool moduleEnabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			if (!_enabled && value)
			{
				_enabled = value;
				onModuleEnable();
			}
			else
			{
				_enabled = value;
				onModuleDisable();
			}
		}
	}

	public bool moduleInited
	{
		get
		{
			return _inited;
		}
		set
		{
			if (!_inited && value)
			{
				_inited = value;
				onModuleInited();
			}
			else
			{
				_inited = value;
			}
		}
	}

	public abstract void onModuleEnable();

	public abstract void onModuleDisable();

	public abstract void onModuleInited();
}
