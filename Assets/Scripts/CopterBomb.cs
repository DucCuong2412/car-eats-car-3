using UnityEngine;

public class CopterBomb : MonoBehaviour
{
	public int numFires = 3;

	private float counter;

	private bool check;

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			base.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			counter = 0f;
			check = false;
		}
	}

	private void Update()
	{
		if (base.transform.parent.gameObject.activeSelf && counter < 90f)
		{
			counter += 1.5f;
			if (counter > 90f)
			{
				counter = 90f;
			}
			base.transform.parent.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, counter);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (RaceManager.instance.isStarted && !check)
		{
			check = true;
			Audio.PlayAsync("exp_tnt_05_sn");
			Pool.Animate(Pool.Explosion.exp25, base.transform.position);
			for (int i = 0; i < numFires; i++)
			{
				Vector3 position = base.transform.position;
				float x = position.x + (float)i;
				Vector3 position2 = base.transform.position;
				float y = position2.y;
				Vector3 position3 = base.transform.position;
				GameObject fuelBomb = Pool.GetFuelBomb(new Vector3(x, y, position3.z));
				Rigidbody2D component = fuelBomb.GetComponent<Rigidbody2D>();
				component.AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), 10f), ForceMode2D.Impulse);
			}
			base.transform.parent.gameObject.SetActive(value: false);
		}
	}
}
