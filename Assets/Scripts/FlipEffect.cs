using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEffect : MonoBehaviour
{
	public float _Time;

	public float CircleSpeed;

	public int CirclesNumber = 3;

	public Transform _Circle;

	public int ParticlesNumber = 5;

	public float ParticleSpeed;

	public Transform _Particle;

	private List<Transform> Particles = new List<Transform>();

	private List<Transform> Circles = new List<Transform>();

	private List<tk2dSprite> CircleSprites = new List<tk2dSprite>();

	private List<tk2dSprite> ParticleSprites = new List<tk2dSprite>();

	private Vector2 circleMaxScale;

	private Vector2 circleMinScale;

	private float rate;

	private float t;

	private void Start()
	{
		circleMaxScale = _Circle.localScale;
		circleMinScale = new Vector2(0f, 0f);
		for (int i = 0; i < CirclesNumber; i++)
		{
			Circles.Add(UnityEngine.Object.Instantiate(_Circle.gameObject).transform);
			CircleSprites.Add(Circles[i].GetComponent<tk2dSprite>());
			CircleSprites[i].color = new Color(1f, 1f, 1f, 0f);
			Circles[i].position = _Circle.position;
			Circles[i].localScale = new Vector2(circleMaxScale.x - 0.3f * (float)(i + 1), circleMaxScale.y - 0.3f * (float)(i + 1));
			Circles[i].parent = base.transform;
		}
		Circles.Add(_Circle);
		CircleSprites.Add(_Circle.GetComponent<tk2dSprite>());
		CircleSprites[CircleSprites.Count - 1].color = new Color(1f, 1f, 1f, 0f);
		if (!(_Particle == null))
		{
			for (int j = 0; j < ParticlesNumber; j++)
			{
				Particles.Add(UnityEngine.Object.Instantiate(_Particle.gameObject).transform);
				ParticleSprites.Add(Particles[j].GetComponent<tk2dSprite>());
				ParticleSprites[j].color = new Color(1f, 1f, 1f, 0f);
				Particles[j].transform.parent = base.transform;
				float num = UnityEngine.Random.Range(1f, 3f);
				Particles[j].localScale = new Vector2(num, num);
				Transform transform = Particles[j].transform;
				Vector3 position = base.transform.position;
				float x = position.x + UnityEngine.Random.Range((0f - circleMaxScale.x) * 5f, circleMaxScale.x * 5f);
				Vector3 position2 = base.transform.position;
				transform.position = new Vector2(x, position2.y + UnityEngine.Random.Range((0f - circleMaxScale.y) * 5f, circleMaxScale.y * 5f));
			}
			Particles.Add(_Particle);
			ParticleSprites.Add(_Particle.GetComponent<tk2dSprite>());
			ParticleSprites[ParticleSprites.Count - 1].color = new Color(1f, 1f, 1f, 0f);
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
		rate = 1f / _Time;
		t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * rate;
			CirclesMove();
			ParticlesMove();
			yield return null;
		}
		t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 2f;
			for (int i = 0; i < CircleSprites.Count; i++)
			{
				CircleSprites[i].color = Color.Lerp(CircleSprites[i].color, new Color(1f, 1f, 1f, 0f), t);
			}
			for (int j = 0; j < ParticleSprites.Count; j++)
			{
				ParticleSprites[j].color = Color.Lerp(ParticleSprites[j].color, new Color(1f, 1f, 1f, 0f), t);
			}
			yield return null;
		}
		base.gameObject.SetActive(value: false);
	}

	private void CirclesMove()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		for (int i = 0; i < Circles.Count; i++)
		{
			Vector3 localScale = Circles[i].localScale;
			if (localScale.x <= 0.3f)
			{
				Circles[i].localScale = circleMaxScale;
			}
			CircleSprites[i].color = Color.Lerp(CircleSprites[i].color, new Color(1f, 1f, 1f, 1f), t);
			Circles[i].localScale = Vector2.Lerp(Circles[i].localScale, circleMinScale, Time.deltaTime * CircleSpeed);
		}
	}

	private void ParticlesMove()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		for (int i = 0; i < Particles.Count; i++)
		{
			if (Vector2.Distance(Particles[i].position, base.transform.position) <= 1f)
			{
				Transform transform = Particles[i];
				Vector3 position = base.transform.position;
				float x = position.x + UnityEngine.Random.Range((0f - circleMaxScale.x) * 5f, circleMaxScale.x * 5f);
				Vector3 position2 = base.transform.position;
				transform.position = new Vector2(x, position2.y + UnityEngine.Random.Range((0f - circleMaxScale.y) * 5f, circleMaxScale.y * 5f));
			}
			ParticleSprites[i].color = Color.Lerp(ParticleSprites[i].color, new Color(1f, 1f, 1f, 1f), t);
			Particles[i].position = Vector2.Lerp(Particles[i].position, base.transform.position + (base.transform.position - Particles[i].position), Time.deltaTime * ParticleSpeed);
		}
	}
}
