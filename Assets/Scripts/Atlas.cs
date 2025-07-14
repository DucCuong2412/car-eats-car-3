using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atlas : MonoBehaviour
{
	private static Atlas _atlas;

	public AtlasCreator.Atlas[] atlasesCircle;

	public AtlasCreator.Atlas[] atlasesSquare;

	public AtlasCreator.Atlas[] atlasesNoFriens;

	private List<Texture2D> textures = new List<Texture2D>();

	public Action loadAtlass = delegate
	{
	};

	public static Atlas instance
	{
		get
		{
			if (_atlas == null)
			{
				GameObject gameObject = new GameObject("_atlas");
				_atlas = gameObject.AddComponent<Atlas>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return _atlas;
		}
	}

	private void OnEnable()
	{
		StartCoroutine(SetAtlas());
	}

	private IEnumerator SetAtlas()
	{
		yield return 0;
		loadAtlass();
	}
}
