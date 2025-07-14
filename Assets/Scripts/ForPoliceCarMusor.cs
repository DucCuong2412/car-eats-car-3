using System.Collections;
using UnityEngine;

public class ForPoliceCarMusor : MonoBehaviour
{
	public Car2DController C2DC;

	public Animator Anim;

	public float CollUcusForDrop;

	public GameObject TochkaSpavna;

	private int _isFill1 = Animator.StringToHash("isFill1");

	private int _isFill2 = Animator.StringToHash("isFill2");

	private int _isFill3 = Animator.StringToHash("isFill3");

	private int _isFill4 = Animator.StringToHash("isFill4");

	private int _isFill5 = Animator.StringToHash("isFill5");

	private int _isDrop = Animator.StringToHash("isDrop");

	private float temp;

	private void Update()
	{
		temp = C2DC.collCus / CollUcusForDrop;
		if (temp >= 0.2f)
		{
			Anim.SetBool(_isFill1, value: true);
			Anim.SetBool(_isDrop, value: false);
		}
		if (temp >= 0.35f)
		{
			Anim.SetBool(_isFill2, value: true);
		}
		if (temp >= 0.55f)
		{
			Anim.SetBool(_isFill3, value: true);
		}
		if (temp >= 0.7f)
		{
			Anim.SetBool(_isFill4, value: true);
		}
		if (temp >= 0.9f)
		{
			Anim.SetBool(_isFill5, value: true);
		}
		if (C2DC.collCus == CollUcusForDrop)
		{
			StartCoroutine(Drop());
			C2DC.collCus = 0f;
		}
	}

	private IEnumerator Drop()
	{
		Anim.SetBool(_isDrop, value: true);
		Anim.SetBool(_isFill1, value: false);
		Anim.SetBool(_isFill2, value: false);
		Anim.SetBool(_isFill3, value: false);
		Anim.SetBool(_isFill4, value: false);
		Anim.SetBool(_isFill5, value: false);
		yield return new WaitForSeconds(0.33f);
		Pool.GameOBJECT(Pool.Scraps.CageScrPoliceCar1, TochkaSpavna.transform.position);
		Pool.GameOBJECT(Pool.Scraps.CageScrPoliceCar2, TochkaSpavna.transform.position);
		Pool.GameOBJECT(Pool.Scraps.CageScrPoliceCar3, TochkaSpavna.transform.position);
		Pool.GameOBJECT(Pool.Scraps.CageScrPoliceCar4, TochkaSpavna.transform.position);
		Pool.GameOBJECT(Pool.Scraps.CageScrPoliceCar5, TochkaSpavna.transform.position);
	}
}
