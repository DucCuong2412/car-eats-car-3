using SMKK;
using System.Collections;
using UnityEngine;

public class Avalanche_Controller : MonoBehaviour
{
	public GameObject AvalancheObj;

	public ParticleSystem Particle1;

	public ParticleSystem Particle2;

	public ParticleSystem Particle3;

	public float SpeedCoef = 4f;

	public float MinPos = -1200f;

	public float MaxDmgToFrame = 1f;

	public int PosXtoStartDmg = -500;

	public int PosXtoAudio = -1000;

	public float CoefToChange = 3f;

	private Transform _car;

	private float maxSpeed;

	private float _maxCounterSpeed;

	private float _lastX;

	private Vector3 _vectorSpeed;

	private int _counterToDmg;

	private float _dmgSum;

	private bool _go;

	private bool _tryGo = true;

	private static string stone_new = "stone_new";

	private void Start()
	{
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator SetCarCorut()
	{
		_car = null;
		while (_car == null)
		{
			if (RaceLogic.instance.car != null)
			{
				_car = RaceLogic.instance.car.transform;
				maxSpeed = RaceLogic.instance.car.EngineModule.MaxSpeed / 1500f;
				Audio.Play(stone_new, 0.2f, loop: true);
			}
			yield return null;
		}
	}

	private void Update()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		if (_tryGo && Game.currentState == Game.gameState.Race)
		{
			_tryGo = false;
			StartCoroutine(DelayToStart());
		}
		if (RaceLogic.instance.car != null && RaceLogic.instance.car.HealthModule._barrel.Value <= 0f && _go)
		{
			_go = false;
			Particle1.emission.rateOverTime = 0f;
			Particle2.emission.rateOverTime = 0f;
			Particle3.emission.rateOverTime = 0f;
			StartCoroutine(DelayToOff());
		}
		if (_car != null && _go)
		{
			if (_lastX == 0f)
			{
				Vector3 localPosition = _car.localPosition;
				_lastX = localPosition.x;
			}
			ref Vector3 vectorSpeed = ref _vectorSpeed;
			Vector3 localPosition2 = _car.localPosition;
			vectorSpeed.x = localPosition2.x - _lastX;
			Vector3 localPosition3 = _car.localPosition;
			_lastX = localPosition3.x;
			if (_maxCounterSpeed < _vectorSpeed.x)
			{
				_maxCounterSpeed = _vectorSpeed.x;
			}
			_vectorSpeed.x = maxSpeed / 2f - _vectorSpeed.x;
			Vector3 localPosition4 = AvalancheObj.transform.localPosition;
			if (localPosition4.x > MinPos || _vectorSpeed.x > 0f)
			{
				AvalancheObj.transform.localPosition += _vectorSpeed * SpeedCoef;
			}
			float num = PosXtoAudio;
			Vector3 localPosition5 = AvalancheObj.transform.localPosition;
			if (num < localPosition5.x)
			{
				AudioManager instance = AudioManager.instance;
				string id = stone_new;
				Vector3 localPosition6 = AvalancheObj.transform.localPosition;
				instance.ChengValue(id, 0.2f + (1f - localPosition6.x / (float)PosXtoAudio) * CoefToChange);
			}
			float num2 = PosXtoStartDmg;
			Vector3 localPosition7 = AvalancheObj.transform.localPosition;
			if (num2 < localPosition7.x)
			{
				float dmgSum = _dmgSum;
				Vector3 localPosition8 = AvalancheObj.transform.localPosition;
				_dmgSum = dmgSum + (1f - localPosition8.x / (float)PosXtoStartDmg) * MaxDmgToFrame;
			}
			_counterToDmg++;
			if (_counterToDmg >= 60 && _dmgSum >= MaxDmgToFrame)
			{
				RaceLogic.instance.EatMainCar(_dmgSum);
				_counterToDmg = 0;
				_dmgSum = 0f;
			}
		}
	}

	private IEnumerator DelayToOff()
	{
		Vector3 localPosition = AvalancheObj.transform.localPosition;
		float startVolume = 0.2f + (1f - localPosition.x / (float)PosXtoAudio) * CoefToChange;
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime / 2f;
			AudioManager.instance.ChengValue(stone_new, startVolume * t);
			yield return null;
		}
		Audio.Stop(stone_new);
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator DelayToStart()
	{
		float t = 4f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		_go = true;
	}

	private void OnDisable()
	{
		Audio.Stop(stone_new);
	}
}
