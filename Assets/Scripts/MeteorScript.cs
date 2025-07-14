using UnityEngine;

public class MeteorScript : BombCommonLogic
{
	public float Damage = 300f;

	public float Radius = 5f;

	public float Power = 150f;

	private GameObject Exp;

	private void OnCollisionEnter2D(Collision2D coll)
	{
		Audio.Play("gfx_earthquake_02_sn", Audio.soundVolume, loop: false);
		Explosion(base.transform.position, Radius, Power, Damage);
		Pool.Animate(Pool.Explosion.exp26, base.transform.position);
		base.gameObject.SetActive(value: false);
	}
}
