using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
	public float Damage = 300f;

	public float PauseTime = 0.1f;

	public float _Time = 5f;

	public float CloudMoveSpeed = 4f;

	public Vector3 DeltaPos = new Vector3(0f, 13f);

	public GameObject Lightning;

	public float Radius = 20f;

	private Transform _target;

	private List<Car2DAIController> Targets;

	private Transform Car;

	private string soundEffectID;

	[HideInInspector]
	public float t;

	private Transform Target
	{
		get
		{
			if (!(_target == null) && _target.gameObject.activeSelf && !(_target == Car))
			{
				Vector3 position = _target.transform.position;
				float x = position.x;
				Vector3 position2 = Car.transform.position;
				if (!(Mathf.Abs(x - position2.x) > Radius))
				{
					goto IL_0125;
				}
			}
			bool flag = false;
			if (Targets.Count > 0)
			{
				for (int i = 0; i < Targets.Count; i++)
				{
					Vector3 position3 = Targets[i].transform.position;
					float x2 = position3.x;
					Vector3 position4 = Car.transform.position;
					if (Mathf.Abs(x2 - position4.x) <= Radius)
					{
						_target = Targets[i].transform;
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				_target = Car;
			}
			goto IL_0125;
			IL_0125:
			return _target;
		}
		set
		{
			_target = value;
		}
	}

	public void Activate(Transform _car, List<Car2DAIController> _targets)
	{
		for (int i = 0; i < Lightning.GetComponentsInChildren<Renderer>().Length; i++)
		{
			Lightning.GetComponentsInChildren<Renderer>()[i].sortingLayerName = "Particles";
		}
		Car = _car;
		Targets = _targets;
		StartCoroutine(CloudMove());
		StartCoroutine(PlayMusic());
	}

	private IEnumerator PlayMusic()
	{
		switch (UnityEngine.Random.Range(0, 2))
		{
		case 0:
			Audio.Play("gfx_storm_00_sn", Audio.soundVolume);
			break;
		case 1:
			Audio.Play("gfx_storm_01_sn", Audio.soundVolume);
			break;
		default:
			Audio.Play("gfx_storm_02_sn", Audio.soundVolume);
			break;
		}
		yield return new WaitForSeconds(0.3f);
		StartCoroutine(PlayMusic());
	}

	private IEnumerator CloudMove()
	{
		float pausetime = PauseTime;
		t = _Time;
		while (t > 0f)
		{
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = Target.position;
			if (Mathf.Abs(x - position2.x) < 5f && Target != Car)
			{
				pausetime -= Time.deltaTime;
				if (pausetime <= 0f)
				{
					LightningShoot();
					pausetime = PauseTime;
				}
			}
			base.transform.position = Vector2.Lerp(base.transform.position, Target.position + DeltaPos, Time.deltaTime * CloudMoveSpeed);
			t -= Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		t = 0f;
		Vector2 endpos = base.transform.position + new Vector3(0f, 35f);
		while (t < 1f)
		{
			t += Time.deltaTime / 3f;
			base.transform.position = Vector2.Lerp(base.transform.position, endpos, t);
			yield return new WaitForFixedUpdate();
		}
		base.gameObject.SetActive(value: false);
	}

	private void LightningShoot()
	{
		Lightning.SetActive(value: true);
		Target.gameObject.SendMessageUpwards("ChangeHealth", 0f - Damage, SendMessageOptions.DontRequireReceiver);
	}

	private void OnDisable()
	{
		if (soundEffectID != null)
		{
			Audio.Stop(soundEffectID);
			soundEffectID = null;
		}
	}
}
