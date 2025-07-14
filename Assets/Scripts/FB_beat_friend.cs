using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FB_beat_friend : MonoBehaviour
{
	[Serializable]
	public class FriendTexture
	{
		public Texture2D tex;

		public Rect reck = default(Rect);
	}

	[Serializable]
	public class MeTexture
	{
		public Texture2D tex;

		public Rect reck = default(Rect);
	}

	private AtlasCreator.Atlas[] atlases;

	public RawImage friendAvatar;

	public RawImage MeAvatar;

	public Button fbShare;

	public Button Close;

	public Animator anim;

	public Text MyName;

	public Text FriendName;

	public CameraPrerender Render;

	private List<FriendTexture> friendsTextures = new List<FriendTexture>();

	private List<MeTexture> MeTextures = new List<MeTexture>();

	private IEnumerator cor()
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
	}

	private void OnEnable()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		Close.onClick.AddListener(Closes);
		fbShare.onClick.AddListener(Share);
	}

	private void OnDisable()
	{
		Close.onClick.RemoveAllListeners();
		fbShare.onClick.RemoveAllListeners();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Closes();
		}
	}

	private void Closes()
	{
		StartCoroutine(CloseWindow());
	}

	private IEnumerator CloseWindow()
	{
		fbShare.interactable = false;
		Game.OnStateChange(Game.gameState.Menu);
		base.gameObject.SetActive(value: false);
		yield return 0;
	}

	private void Share()
	{
		fbShare.interactable = false;
		StartCoroutine(ForShare());
	}

	private IEnumerator ForShare()
	{
		Render.disable = true;
		yield return new WaitForSeconds(1f);
		Closes();
	}
}
