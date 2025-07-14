using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapFriendContainer : MonoBehaviour
{
	[Serializable]
	public class FriendTexture
	{
		public Texture2D tex;

		public Rect reck = default(Rect);
	}

	public GameObject playerPlaceLogged;

	public GameObject playerPlaceUnlogged;

	public RawImage friendAvatar;

	[Header("For Boxes")]
	public GameObject IcoFB;

	private List<FriendTexture> friendsTextures = new List<FriendTexture>();

	public int key = 5;

	public void Off()
	{
		base.gameObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		StartCoroutine(SwitchIcons());
	}

	private IEnumerator SwitchIcons()
	{
		yield return new WaitForSeconds(1f);
		if (friendsTextures.Count == 0)
		{
			yield break;
		}
		int index = -1;
		while (true)
		{
			int num = index + 1;
			index = num % friendsTextures.Count;
			friendAvatar.texture = friendsTextures[index].tex;
			friendAvatar.uvRect = friendsTextures[index].reck;
			yield return new WaitForSeconds(5f);
		}
	}
}
