using System;
using UnityEngine;

public class BombItem : MonoBehaviour
{
	[Serializable]
	public class Object
	{
		public BombLogic.types type;

		public float radius;

		public float impulse;

		public float maxDamage;

		public float lifeTime;

		public bool onCollisionEnter = true;

		public int fragmentCount;

		public Pool.Scraps fragmentSprite;

		public int fragmentSpeed;

		public float fragmentGravityScale = 0.05f;

		[Range(0f, 1f)]
		public float fragmentCloudWidth = 1f;

		public int fragmentLifeTime;

		public int fragmentDamage;

		public float efectDamage;

		public float efectDuration;

		public bool mainCarCollision;
	}

	[SerializeField]
	public Object parameters = new Object();

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision != null && RaceManager.instance != null && 
			RaceManager.instance.isStarted && parameters.onCollisionEnter)
		{
			BombLogic.instance.OnCollision(transform);
		}
	}
}
