using System.Collections.Generic;
using UnityEngine;

public interface IGhostModule
{
	void Init(List<Transform> _wheels, Transform _body = null, string _ghostRecName = "", int engine = 0, int turbo = 0, int body = 0, int wheels = 0, int AbsorsType = 0, int[] TuningType = null, int PaintType = -1, int color = -1);

	void SaveRecord();
}
