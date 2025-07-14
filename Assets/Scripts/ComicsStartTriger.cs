using UnityEngine;
using UnityEngine.UI;

public class ComicsStartTriger : MonoBehaviour
{
	public Animator Anim;

	public Button But;

	public GameObject Flag;

	public GameObject FlagEnd;

	public GameObject Shade;

	private NewBehaviourScript contr;

	private bool startMap;

	private int _EndMap = Animator.StringToHash("EndMap");

	private void Start()
	{
		contr = UnityEngine.Object.FindObjectOfType<NewBehaviourScript>();
		startMap = false;
		But.onClick.AddListener(pressBut);
	}

	private void pressBut()
	{
		FlagEnd.SetActive(value: true);
	}

	public void SetPlay()
	{
		Anim.SetBool(_EndMap, value: true);
	}

	private void Update()
	{
		if (!startMap && Flag.activeSelf)
		{
			startMap = true;
			contr.goMap(this);
		}
		if (FlagEnd.activeSelf)
		{
			Progress.shop.StartComixShow = true;
			contr.OffAll();
			UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorials");
		}
	}
}
