using Smokoko.Social;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookFriendsLevelLogic : MonoBehaviour
{
	public List<CellContainer> containers = new List<CellContainer>();

	private AtlasCreator.Atlas[] atlases;

	private void Update()
	{
	}

	public IEnumerator WaitUserImage()
	{
		yield return null;
	}

	public void PaclAtlas()
	{
		StartCoroutine(Pack());
	}

	private IEnumerator Pack()
	{
		while (Atlas.instance.atlasesSquare == null)
		{
			yield return 0;
		}
		atlases = Atlas.instance.atlasesSquare;
		SetAvatars();
	}

	public void SetAvatars()
	{
		StartCoroutine(avatars());
	}

	private IEnumerator avatars()
	{
		while (FBLeaderboard.CurrentUser == null)
		{
			yield return 0;
		}
		yield return new WaitForSeconds(1f);
	}

	private void OnEnable()
	{
		foreach (CellContainer container in containers)
		{
			container.FacebookFriendContainer.gameObject.SetActive(value: false);
		}
		StartCoroutine(WaitUserImage());
		SetAvatars();
	}
}
