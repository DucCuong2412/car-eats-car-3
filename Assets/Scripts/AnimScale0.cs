using AnimationOrTween;
using System.Collections;
using UnityEngine;

public class AnimScale0 : MonoBehaviour
{
	public Animation anim;

	private void OnEnable()
	{
		ActiveAnimation.Play(anim, Direction.Forward);
	}

	private IEnumerator anima()
	{
		while (true)
		{
			ActiveAnimation a = ActiveAnimation.Play(anim, Direction.Forward);
			if (a.isPlaying)
			{
				yield return 0;
			}
		}
	}
}
