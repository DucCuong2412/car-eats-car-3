using UnityEngine;

public class Car2dEventListener : MonoBehaviour
{
	public delegate void OnTurboDelegate(bool Use);

	public delegate void ChangeTurboDelegate(int progress);

	public delegate void ChangeEngineDelegate(int progress);

	public event OnTurboDelegate OnTurboEvent;

	public event ChangeTurboDelegate ChangeTurboEvent;

	public event ChangeEngineDelegate ChangeEngineEvent;

	public void OnTurbo(bool Use)
	{
		if (this.OnTurboEvent != null)
		{
			this.OnTurboEvent(Use);
		}
	}

	public void ChangeTurbo(int _progress)
	{
		if (this.ChangeTurboEvent != null)
		{
			this.ChangeTurboEvent(_progress);
		}
	}

	public void ChangeEngine(int _progress)
	{
		if (this.ChangeEngineEvent != null)
		{
			this.ChangeEngineEvent(_progress);
		}
	}
}
