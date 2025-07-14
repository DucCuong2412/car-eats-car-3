using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AtlasCreator
{
	public class AtlasNode
	{
		public AtlasNode[] child;

		public Rect rc = new Rect(0f, 0f, 0f, 0f);

		public Texture2D imageRef;

		public bool hasImage;

		public int sortIndex;

		public string name = "Unknown";

		private static readonly int TEXTURE_PADDING;

		private static readonly bool BLEED;

		public AtlasNode Insert(Texture2D image, int index)
		{
			if (image == null)
			{
				return null;
			}
			if (child != null)
			{
				AtlasNode atlasNode = child[0].Insert(image, index);
				if (atlasNode != null)
				{
					return atlasNode;
				}
				return child[1].Insert(image, index);
			}
			if (hasImage)
			{
				return null;
			}
			if (!ImageFits(image, rc))
			{
				return null;
			}
			if (PerfectFit(image, rc))
			{
				hasImage = true;
				imageRef = image;
				name = imageRef.name;
				sortIndex = index;
				return this;
			}
			child = new AtlasNode[2];
			child[0] = new AtlasNode();
			child[1] = new AtlasNode();
			float num = rc.width - (float)image.width;
			float num2 = rc.height - (float)image.height;
			if (num > num2)
			{
				child[0].rc = new Rect(rc.xMin, rc.yMin, image.width, rc.height);
				child[1].rc = new Rect(rc.xMin + (float)image.width + (float)TEXTURE_PADDING, rc.yMin, rc.width - (float)(image.width + TEXTURE_PADDING), rc.height);
			}
			else
			{
				child[0].rc = new Rect(rc.xMin, rc.yMin, rc.width, image.height);
				child[1].rc = new Rect(rc.xMin, rc.yMin + (float)image.height + (float)TEXTURE_PADDING, rc.width, rc.height - (float)(image.height + TEXTURE_PADDING));
			}
			return child[0].Insert(image, index);
		}

		public bool Contains(string search)
		{
			if (name == search)
			{
				return true;
			}
			if (child != null)
			{
				if (child[0].Contains(search))
				{
					return true;
				}
				if (child[1].Contains(search))
				{
					return true;
				}
			}
			return false;
		}

		private static bool ImageFits(Texture2D image, Rect rect)
		{
			return rect.width >= (float)image.width && rect.height >= (float)image.height;
		}

		private static bool PerfectFit(Texture2D image, Rect rect)
		{
			return rect.width == (float)image.width && rect.height == (float)image.height;
		}

		public void GetNames(ref List<string> result)
		{
			if (hasImage)
			{
				result.Add(name);
			}
			if (child != null)
			{
				if (child[0] != null)
				{
					child[0].GetNames(ref result);
				}
				if (child[1] != null)
				{
					child[1].GetNames(ref result);
				}
			}
		}

		public void GetBounds(ref List<AtlasNode> result)
		{
			if (hasImage)
			{
				result.Add(this);
			}
			if (child != null)
			{
				if (child[0] != null)
				{
					child[0].GetBounds(ref result);
				}
				if (child[1] != null)
				{
					child[1].GetBounds(ref result);
				}
			}
		}

		public void Clear()
		{
			if (child != null)
			{
				if (child[0] != null)
				{
					child[0].Clear();
				}
				if (child[1] != null)
				{
					child[1].Clear();
				}
			}
			if (imageRef != null)
			{
				imageRef = null;
			}
		}

		public void Build(Texture2D target)
		{
			if (child != null)
			{
				if (child[0] != null)
				{
					child[0].Build(target);
				}
				if (child[1] != null)
				{
					child[1].Build(target);
				}
			}
			if (!(imageRef != null))
			{
				return;
			}
			Color[] pixels = imageRef.GetPixels(0);
			for (int i = 0; i < imageRef.width; i++)
			{
				for (int j = 0; j < imageRef.height; j++)
				{
					target.SetPixel(i + (int)rc.x, j + (int)rc.y, pixels[i + j * imageRef.width]);
				}
			}
			if (TEXTURE_PADDING > 0 && BLEED)
			{
				for (int k = 0; k < imageRef.height; k++)
				{
					int num = imageRef.width - 1;
					target.SetPixel(num + (int)rc.x + TEXTURE_PADDING, k + (int)rc.y, pixels[num + k * imageRef.width]);
				}
				for (int l = 0; l < imageRef.width; l++)
				{
					int num2 = imageRef.height - 1;
					target.SetPixel(l + (int)rc.x, num2 + (int)rc.y + TEXTURE_PADDING, pixels[l + num2 * imageRef.width]);
				}
			}
		}
	}

	public class AtlasDescriptor
	{
		public string name;

		public Rect uvRect;

		public int width;

		public int height;
	}

	public class Atlas
	{
		public Texture2D texture;

		public AtlasNode root;

		public AtlasDescriptor[] uvRects;
	}

	public static int AtlasSize = 1024;

	public static Atlas[] atlesesC;

	public static void setCircleAtlases()
	{
	}

	public static void SaveAtlas(Atlas atlas, string name)
	{
		if (atlas != null && !(atlas.texture == null))
		{
			byte[] bytes = atlas.texture.EncodeToPNG();
			if (!Directory.Exists(Application.persistentDataPath + "/Debug/"))
			{
				Directory.CreateDirectory(Application.persistentDataPath + "/Debug/");
			}
			string path = Application.persistentDataPath + "/Debug/" + name + ".png";
			File.WriteAllBytes(path, bytes);
		}
	}

	public static Atlas[] CreateAtlas(string name, Texture2D[] textures, Atlas startWith = null)
	{
		List<Texture2D> list = new List<Texture2D>();
		list.AddRange(textures);
		int num = list.Count - 1;
		list.Reverse();
		List<Atlas> list2 = new List<Atlas>();
		int num2 = 0;
		if (startWith != null)
		{
			num2 = startWith.root.sortIndex;
		}
		while (num >= 0)
		{
			Atlas atlas = startWith;
			if (atlas == null)
			{
				atlas = new Atlas();
				atlas.texture = new Texture2D(AtlasSize, AtlasSize, TextureFormat.RGBA32, mipChain: false);
				atlas.root = new AtlasNode();
				atlas.root.rc = new Rect(0f, 0f, AtlasSize, AtlasSize);
			}
			startWith = null;
			while (num >= 0 && (atlas.root.Contains(list[num].name) || atlas.root.Insert(list[num], num2++) != null))
			{
				num--;
			}
			list2.Add(atlas);
			atlas.root.sortIndex = num2;
			num2 = 0;
			atlas = null;
		}
		foreach (Atlas item2 in list2)
		{
			item2.root.Build(item2.texture);
			List<AtlasNode> result = new List<AtlasNode>();
			item2.root.GetBounds(ref result);
			result.Sort(delegate(AtlasNode x, AtlasNode y)
			{
				if (x.sortIndex == y.sortIndex)
				{
					return 0;
				}
				return (y.sortIndex > x.sortIndex) ? (-1) : 1;
			});
			List<Rect> list3 = new List<Rect>();
			foreach (AtlasNode item3 in result)
			{
				Rect item = new Rect(item3.rc.xMin / item2.root.rc.width, item3.rc.yMin / item2.root.rc.height, item3.rc.width / item2.root.rc.width, item3.rc.height / item2.root.rc.height);
				item.x += 0.5f / item2.root.rc.width;
				item.width -= 1f / item2.root.rc.width;
				item.y += 0.5f / item2.root.rc.height;
				item.height -= 1f / item2.root.rc.height;
				list3.Add(item);
			}
			item2.uvRects = new AtlasDescriptor[list3.Count];
			for (int i = 0; i < list3.Count; i++)
			{
				item2.uvRects[i] = new AtlasDescriptor();
				item2.uvRects[i].width = (int)result[i].rc.width;
				item2.uvRects[i].height = (int)result[i].rc.height;
				item2.uvRects[i].name = result[i].name;
				item2.uvRects[i].uvRect = list3[i];
			}
			item2.root.Clear();
			if (item2 != list2[list2.Count - 1])
			{
				item2.texture.Apply(updateMipmaps: false, makeNoLongerReadable: true);
			}
			else
			{
				item2.texture.Apply(updateMipmaps: false, makeNoLongerReadable: false);
			}
		}
		return list2.ToArray();
	}

	public static Texture2D CalculateTexture(int h, int w, float r, float cx, float cy, Texture2D sourceTex)
	{
		Color[] pixels = sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
		Texture2D texture2D = new Texture2D(h, w, TextureFormat.RGBA32, mipChain: false);
		for (int i = (int)(cx - r); (float)i < cx + r; i++)
		{
			for (int j = (int)(cy - r); (float)j < cy + r; j++)
			{
				float num = (float)i - cx;
				float num2 = (float)j - cy;
				float num3 = Mathf.Sqrt(num * num + num2 * num2);
				if (num3 <= r)
				{
					texture2D.SetPixel(i - (int)(cx - r), j - (int)(cy - r), sourceTex.GetPixel(i, j));
				}
				else
				{
					texture2D.SetPixel(i - (int)(cx - r), j - (int)(cy - r), Color.clear);
				}
			}
		}
		texture2D.Apply();
		return texture2D;
	}
}
