using System.Collections.Generic;
using UnityEngine;

public class RaceManagerBase : MonoBehaviour
{
	private class PositionComparer : IComparer<Transform>
	{
		public int Compare(Transform t1, Transform t2)
		{
			Vector3 position = t1.position;
			ref float x = ref position.x;
			Vector3 position2 = t2.position;
			return -x.CompareTo(position2.x);
		}
	}

	public delegate void FinishEvent();

	private bool p_isStarted;

	private float p_timeElapsed;

	private Transform p_car;

	private List<Transform> p_carTransforms = new List<Transform>();

	private Transform p_finish;

	private Transform p_start;

	private int p_position;

	private int p_totalCars;

	private PositionComparer posComparer = new PositionComparer();

	public bool isStarted
	{
		get
		{
			return p_isStarted;
		}
		private set
		{
			p_isStarted = value;
		}
	}

	public float timeElapsed
	{
		get
		{
			return p_timeElapsed;
		}
		private set
		{
			p_timeElapsed = value;
		}
	}

	public Transform car
	{
		get
		{
			return p_car;
		}
		private set
		{
			p_car = value;
		}
	}

	public List<Transform> carTransforms
	{
		get
		{
			return p_carTransforms;
		}
		private set
		{
			p_carTransforms = value;
		}
	}

	public Transform finish
	{
		get
		{
			return p_finish;
		}
		private set
		{
			p_finish = value;
		}
	}

	public Transform start
	{
		get
		{
			return p_start;
		}
		set
		{
			p_start = value;
		}
	}

	public int position
	{
		get
		{
			return p_position;
		}
		set
		{
			p_position = value;
		}
	}

	public int totalCars
	{
		get
		{
			return p_totalCars;
		}
		private set
		{
			p_totalCars = value;
		}
	}

	public event FinishEvent onFinish;

	public virtual void Init(Transform tCar, Transform tStart, Transform tFinish, params Transform[] tCars)
	{
		car = tCar;
		start = tStart;
		this.finish = tFinish;
		if (tCars != null)
		{
			foreach (Transform item in tCars)
			{
				carTransforms.Add(item);
				totalCars++;
			}
		}
		Transform finish = this.finish;
		Transform transform = finish;
		Vector3 position = this.finish.position;
		float x = position.x + 15f;
		Vector3 position2 = this.finish.position;
		transform.position = new Vector3(x, position2.y);
		carTransforms.Add(finish);
		carTransforms.Add(car);
		totalCars++;
	}

	public virtual void StartRace()
	{
		isStarted = true;
	}

	public virtual void Restart()
	{
	}

	public virtual void Update()
	{
		if (isStarted)
		{
			timeElapsed += Time.deltaTime;
			checkPosition();
		}
	}

	public virtual void OnPositionChanged()
	{
	}

	public virtual void OnLose()
	{
		isStarted = false;
	}

	public virtual void OnFinish()
	{
		isStarted = false;
		if (this.onFinish != null)
		{
			this.onFinish();
		}
	}

	private void checkPosition(bool _started = false)
	{
		int num = carTransforms.RemoveAll(NullTransform);
		if (num > 0 && car == null)
		{
			position = 0;
		}
		carTransforms.Sort(posComparer);
		int num2 = carTransforms.IndexOf(car);
		if (num2 != position)
		{
			if (num2 < 0)
			{
				OnLose();
				return;
			}
			if ((float)num2 < (float)carTransforms.IndexOf(finish) + 1f)
			{
				OnFinish();
				return;
			}
			position = num2;
			OnPositionChanged();
		}
	}

	private bool NullTransform(Transform t)
	{
		return (t == null) ? true : false;
	}
}
