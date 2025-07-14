using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ImageAplhaUpdater : MonoBehaviour
{
	private MaskableGraphic m_image;

	private const int UPDATE_FRAMES = 4;

	private static int totalImages;

	private int count;

	public MaskableGraphic image
	{
		get
		{
			if (m_image == null)
			{
				m_image = GetComponent<MaskableGraphic>();
			}
			return m_image;
		}
	}

	private void Awake()
	{
		count = totalImages++;
	}

	private void Update()
	{
		if (count++ % 4 == 0)
		{
			image.SetAllDirty();
		}
	}
}
