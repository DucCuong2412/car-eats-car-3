using UnityEngine;

public class ParticlePoolExampleCall : MonoBehaviour
{
	private void Start()
	{
		ParticlePoolExample.instance.Init();
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0f, 0f, 100f, 75f), "Animate"))
		{
			ParticlePoolExample.instance.Animate(ParticlePoolExample.Explosion.exp1, Vector2.zero);
		}
		if (GUI.Button(new Rect(0f, 100f, 100f, 75f), "Animate"))
		{
			ParticlePoolExample.instance.Scrup(ParticlePoolExample.Scrups.scrup1, Vector2.zero, 90f, 5f, 2f);
		}
	}
}
