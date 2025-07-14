using Smokoko.Social;
using System;
using UnityEngine;

public class FBuserImage : MonoBehaviour
{
	private FBUser user;

	private string url;

	private Texture2D _imageSquare;

	private Texture2D _imageLarge;

	public event Action<FBuserImage> OnProfileImageLoaded = delegate
	{
	};

	public FBuserImage(FBUser _user, string _url)
	{
		user = _user;
		url = _url;
	}

	private void OnSquareImageLoaded(Texture2D image)
	{
		this.OnProfileImageLoaded(this);
	}

	private void OnLargeImageLoaded(Texture2D image)
	{
		this.OnProfileImageLoaded(this);
	}

	private void OnNormalImageLoaded(Texture2D image)
	{
		this.OnProfileImageLoaded(this);
	}

	private void OnSmallImageLoaded(Texture2D image)
	{
		this.OnProfileImageLoaded(this);
	}

	public void LoadedImage(FBuserImage fbimage)
	{
	}
}
