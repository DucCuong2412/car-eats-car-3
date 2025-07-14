using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour
{
	public float _Time;

	public Transform _Line;

	public int ParticlesNumber = 5;

	public float Speed;

	public Transform _Particle;

	private List<Transform> Particles = new List<Transform>();

	private Vector2 lineMaxScale;

	private Vector2 lineMinScale;

	private float rate;

	private float t;

	private void Start()
	{
		lineMaxScale = _Line.localScale;
		Vector3 localScale = _Line.localScale;
		lineMinScale = new Vector2(0f, localScale.y);
		_Line.localScale = lineMinScale;
		if (!(_Particle == null))
		{
			for (int i = 0; i < ParticlesNumber; i++)
			{
				Particles.Add(UnityEngine.Object.Instantiate(_Particle.gameObject).transform);
				Particles[i].transform.parent = base.transform;
				float num = UnityEngine.Random.Range(1f, 3f);
				Particles[i].localScale = new Vector2(num, num);
				Transform transform = Particles[i].transform;
				Vector3 position = _Line.transform.position;
				float x = position.x + UnityEngine.Random.Range((0f - lineMaxScale.x) / 3f, lineMaxScale.x / 3f);
				Vector3 position2 = _Line.position;
				float y = position2.y;
				Vector3 localScale2 = _Line.localScale;
				transform.position = new Vector2(x, y + UnityEngine.Random.Range(localScale2.y / 2f, 0f));
			}
			Particles.Add(_Particle);
		}
	}

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			StartCoroutine(Effect());
		}
	}

	private IEnumerator Effect()
	{
		for (int i = 0; i < Particles.Count; i++)
		{
			Particles[i].gameObject.SetActive(value: true);
		}
		rate = 1f / (_Time / 3f);
		t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * rate;
			_Line.localScale = Vector2.Lerp(_Line.localScale, lineMaxScale, t);
			ParticleMove();
			yield return null;
		}
		t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * rate;
			ParticleMove();
			yield return null;
		}
		for (int j = 0; j < Particles.Count; j++)
		{
			Particles[j].gameObject.SetActive(value: false);
		}
		t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * rate;
			_Line.localScale = Vector2.Lerp(_Line.localScale, lineMinScale, t);
			yield return null;
		}
		base.gameObject.SetActive(value: false);
	}

	private void ParticleMove()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		for (int i = 0; i < Particles.Count; i++)
		{
			Vector3 position = Particles[i].position;
			float y = position.y;
			Vector3 position2 = _Line.position;
			if (y <= position2.y)
			{
				Transform transform = Particles[i];
				Vector3 position3 = _Line.transform.position;
				float x = position3.x + UnityEngine.Random.Range((0f - lineMaxScale.x) / 3f, lineMaxScale.x / 3f);
				Vector3 position4 = _Line.position;
				float y2 = position4.y;
				Vector3 localScale = _Line.localScale;
				transform.position = new Vector2(x, y2 + localScale.y / 2f);
				float num = UnityEngine.Random.Range(1f, 3f);
				Particles[i].localScale = new Vector2(num, num);
			}
			Transform transform2 = Particles[i];
			Vector2 a = Particles[i].position;
			Vector3 position5 = Particles[i].position;
			float x2 = position5.x;
			Vector3 position6 = _Line.position;
			transform2.position = Vector2.Lerp(a, new Vector2(x2, position6.y - 4f), UnityEngine.Random.Range(Speed / 100f, Speed / 10f));
		}
	}
}
