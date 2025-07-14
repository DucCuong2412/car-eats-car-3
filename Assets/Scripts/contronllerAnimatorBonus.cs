using UnityEngine;

public class contronllerAnimatorBonus : MonoBehaviour
{
	public Animator anim;

	public bool Green;

	private int _isGreen = Animator.StringToHash("isGreen");

	private void OnEnable()
	{
		anim.SetBool(_isGreen, Green);
	}
}
