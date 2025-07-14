using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicsController : MonoBehaviour
{
	public List<GameObject> contentList = new List<GameObject>();

	private bool isShownToEnd;

	private void Start()
	{
		Game.OnStateChange(Game.gameState.Comics);
		StartCoroutine(InitComics());
	}

	private IEnumerator InitComics()
	{
		yield return Utilities.WaitForRealSeconds(1f);
		Utilities.RunActor(contentList[0].GetComponentInChildren<Animation>(), isForward: true);
		yield return Utilities.WaitForRealSeconds(2f);
		Utilities.RunActor(contentList[1].GetComponentInChildren<Animation>(), isForward: true);
		yield return Utilities.WaitForRealSeconds(2f);
		StartCoroutine(SetComicsContent());
	}

	private IEnumerator SetComicsContent()
	{
		Utilities.RunActor(contentList[2].GetComponentInChildren<Animation>(), isForward: true);
		yield return Utilities.WaitForRealSeconds(2f);
		Utilities.RunActor(contentList[3].GetComponentInChildren<Animation>(), isForward: true);
		yield return Utilities.WaitForRealSeconds(1f);
		Utilities.RunActor(contentList[4].GetComponentInChildren<Animation>(), isForward: true);
		contentList[4].GetComponentInChildren<BoxCollider2D>().enabled = true;
		isShownToEnd = true;
	}

	public void OnBtnClick()
	{
		Progress.settings.showComics = false;
		Progress.Save(Progress.SaveType.Settings);
		Game.LoadLevel("map_new");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && isShownToEnd)
		{
			OnBtnClick();
		}
	}
}
