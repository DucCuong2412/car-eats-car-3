using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFX_Demo_New : MonoBehaviour
{
	public Text EffectLabel;

	public Text EffectIndexLabel;

	public Renderer groundRenderer;

	public Collider groundCollider;

	private GameObject[] ParticleExamples;

	private int exampleIndex;

	private bool slowMo;

	private Vector3 defaultCamPosition;

	private Quaternion defaultCamRotation;

	private List<GameObject> onScreenParticles = new List<GameObject>();

	private void Awake()
	{
		List<GameObject> list = new List<GameObject>();
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			list.Add(gameObject);
		}
		ParticleExamples = list.ToArray();
		defaultCamPosition = Camera.main.transform.position;
		defaultCamRotation = Camera.main.transform.rotation;
		StartCoroutine("CheckForDeletedParticles");
		UpdateUI();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
		{
			prevParticle();
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
		{
			nextParticle();
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Delete))
		{
			destroyParticles();
		}
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = default(RaycastHit);
			if (groundCollider.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hitInfo, 9999f))
			{
				GameObject gameObject = spawnParticle();
				gameObject.transform.position = hitInfo.point + gameObject.transform.position;
			}
		}
		float axis = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
		if (axis != 0f)
		{
			Camera.main.transform.Translate(Vector3.forward * ((!(axis < 0f)) ? 1f : (-1f)), Space.Self);
		}
		if (Input.GetMouseButtonDown(2))
		{
			Camera.main.transform.position = defaultCamPosition;
			Camera.main.transform.rotation = defaultCamRotation;
		}
	}

	private void OnToggleGround()
	{
		groundRenderer.enabled = !groundRenderer.enabled;
	}

	private void OnToggleCamera()
	{
		CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
	}

	private void OnToggleSlowMo()
	{
		slowMo = !slowMo;
		if (slowMo)
		{
			Time.timeScale = 0.33f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	private void OnPreviousEffect()
	{
		prevParticle();
	}

	private void OnNextEffect()
	{
		nextParticle();
	}

	private void UpdateUI()
	{
		EffectLabel.text = ParticleExamples[exampleIndex].name;
		EffectIndexLabel.text = string.Format("{0}/{1}", (exampleIndex + 1).ToString("00"), ParticleExamples.Length.ToString("00"));
	}

	private GameObject spawnParticle()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(ParticleExamples[exampleIndex]);
		Transform transform = gameObject.transform;
		Vector3 position = gameObject.transform.position;
		transform.position = new Vector3(0f, position.y, 0f);
		gameObject.SetActive(value: true);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		if (component != null && component.loop)
		{
			component.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			component.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
		onScreenParticles.Add(gameObject);
		return gameObject;
	}

	private IEnumerator CheckForDeletedParticles()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			for (int num = onScreenParticles.Count - 1; num >= 0; num--)
			{
				if (onScreenParticles[num] == null)
				{
					onScreenParticles.RemoveAt(num);
				}
			}
		}
	}

	private void prevParticle()
	{
		exampleIndex--;
		if (exampleIndex < 0)
		{
			exampleIndex = ParticleExamples.Length - 1;
		}
		UpdateUI();
	}

	private void nextParticle()
	{
		exampleIndex++;
		if (exampleIndex >= ParticleExamples.Length)
		{
			exampleIndex = 0;
		}
		UpdateUI();
	}

	private void destroyParticles()
	{
		for (int num = onScreenParticles.Count - 1; num >= 0; num--)
		{
			if (onScreenParticles[num] != null)
			{
				UnityEngine.Object.Destroy(onScreenParticles[num]);
			}
			onScreenParticles.RemoveAt(num);
		}
	}
}
