using System.Collections;
using UnityEngine;

public class SpiderEnemyController : MonoBehaviour
{
	public GameObject Hull;

	public Animator Anim;

	public int DistToEnd = 500;

	public int AttackDelay = 120;

	public float Dmg = 5f;

	public float SpeedUp;

	public float SpeedDown;

	private int _counter;

	private int _counterStay;

	private Vector3 _vec;

	private Vector3 _tVec;

	private Vector3 _startPos;

	private Vector3 _hullStartPos;

	private bool _down = true;

	private bool _startCorut = true;

	private int is_attack = Animator.StringToHash("is_attack");

	private int is_take = Animator.StringToHash("is_take");

	private void OnEnable()
	{
		_vec = base.transform.position;
		_tVec = Hull.transform.position;
		_startPos = _vec;
		_hullStartPos = Hull.transform.position;
		_down = true;
		_startCorut = true;
		_counter = AttackDelay;
	}

	private void Update()
	{
		if (RaceLogic.instance == null || RaceLogic.instance.car == null)
		{
			return;
		}
		float x = _startPos.x;
		Vector3 position = RaceLogic.instance.car.transform.position;
		if (!(x <= position.x))
		{
			return;
		}
		float num = _startPos.x + (float)DistToEnd;
		Vector3 position2 = base.transform.position;
		if (num > position2.x)
		{
			_counter++;
			if (_counter >= AttackDelay)
			{
				if (_startCorut)
				{
					ref Vector3 vec = ref _vec;
					Vector3 position3 = RaceLogic.instance.car.transform.position;
					vec.x = position3.x + 10f;
					base.transform.position = _vec;
					StartCoroutine(DelayToFlag(isAttack: true));
					_startCorut = false;
					Audio.PlayAsync("spider");
				}
				_tVec = Hull.transform.position;
				if (_down)
				{
					_tVec.y -= SpeedDown;
					Hull.transform.position = _tVec;
					float y = _tVec.y;
					Vector3 position4 = RaceLogic.instance.car.transform.position;
					if (!(y <= position4.y + 5f))
					{
						return;
					}
					float x2 = _tVec.x;
					Vector3 position5 = RaceLogic.instance.car.transform.position;
					if (x2 >= position5.x - 4f)
					{
						float x3 = _tVec.x;
						Vector3 position6 = RaceLogic.instance.car.transform.position;
						if (x3 <= position6.x + 4f)
						{
							UnityEngine.Debug.Log("damage spider");
						}
					}
					_down = false;
					StartCoroutine(DelayToFlag(isAttack: false));
					_counterStay = 0;
				}
				else if (_counterStay < 30)
				{
					_counterStay++;
				}
				else
				{
					_tVec.y += SpeedUp;
					Hull.transform.position = _tVec;
					if (_tVec.y >= _hullStartPos.y)
					{
						_tVec.y = _hullStartPos.y;
						_down = true;
						_startCorut = true;
						_counter = 0;
					}
				}
			}
			else
			{
				ref Vector3 vec2 = ref _vec;
				Vector3 position7 = RaceLogic.instance.car.transform.position;
				vec2.x = position7.x + 10f;
				base.transform.position = _vec;
			}
		}
		else
		{
			float num2 = _startPos.x + (float)DistToEnd + 50f;
			Vector3 position8 = base.transform.position;
			if (num2 > position8.x)
			{
				base.transform.parent.gameObject.SetActive(value: false);
			}
		}
	}

	private IEnumerator DelayToFlag(bool isAttack)
	{
		if (isAttack)
		{
			Anim.SetBool(is_attack, value: true);
		}
		else
		{
			Anim.SetBool(is_take, value: true);
		}
		int t = 5;
		while (t > 0)
		{
			t--;
			yield return null;
		}
		if (!isAttack)
		{
			Anim.SetBool(is_take, value: false);
			Anim.SetBool(is_attack, value: false);
		}
	}
}
