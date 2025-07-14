using System.Collections.Generic;
using UnityEngine;

namespace tk2dRuntime.TileMap
{
	public static class RenderMeshBuilder
	{
		public static void BuildForChunk(tk2dTileMap tileMap, SpriteChunk chunk, ColorChunk colorChunk, bool useColor, bool skipPrefabs, int baseX, int baseY)
		{
			List<Vector3> list = new List<Vector3>();
			List<Color> list2 = new List<Color>();
			List<Vector2> list3 = new List<Vector2>();
			int[] spriteIds = chunk.spriteIds;
			Vector3 tileSize = tileMap.data.tileSize;
			int num = tileMap.SpriteCollectionInst.spriteDefinitions.Length;
			Object[] tilePrefabs = tileMap.data.tilePrefabs;
			tk2dSpriteDefinition firstValidDefinition = tileMap.SpriteCollectionInst.FirstValidDefinition;
			bool flag = firstValidDefinition != null && firstValidDefinition.normals != null && firstValidDefinition.normals.Length > 0;
			Color32 c = (!useColor || tileMap.ColorChannel == null) ? Color.white : tileMap.ColorChannel.clearColor;
			if (colorChunk == null || colorChunk.colors.Length == 0)
			{
				useColor = false;
			}
			BuilderUtil.GetLoopOrder(tileMap.data.sortMethod, tileMap.partitionSizeX, tileMap.partitionSizeY, out int x, out int x2, out int dx, out int y, out int y2, out int dy);
			float x3 = 0f;
			float y3 = 0f;
			tileMap.data.GetTileOffset(out x3, out y3);
			List<int>[] array = new List<int>[tileMap.SpriteCollectionInst.materials.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new List<int>();
			}
			int num2 = tileMap.partitionSizeX + 1;
			for (int j = y; j != y2; j += dy)
			{
				float num3 = (float)((baseY + j) & 1) * x3;
				for (int k = x; k != x2; k += dx)
				{
					int rawTile = spriteIds[j * tileMap.partitionSizeX + k];
					int tileFromRawTile = BuilderUtil.GetTileFromRawTile(rawTile);
					bool flag2 = BuilderUtil.IsRawTileFlagSet(rawTile, tk2dTileFlags.FlipX);
					bool flag3 = BuilderUtil.IsRawTileFlagSet(rawTile, tk2dTileFlags.FlipY);
					bool rot = BuilderUtil.IsRawTileFlagSet(rawTile, tk2dTileFlags.Rot90);
					Vector3 a = new Vector3(tileSize.x * ((float)k + num3), tileSize.y * (float)j, 0f);
					if (tileFromRawTile < 0 || tileFromRawTile >= num || (skipPrefabs && (bool)tilePrefabs[tileFromRawTile]))
					{
						continue;
					}
					tk2dSpriteDefinition tk2dSpriteDefinition = tileMap.SpriteCollectionInst.spriteDefinitions[tileFromRawTile];
					int count = list.Count;
					for (int l = 0; l < tk2dSpriteDefinition.positions.Length; l++)
					{
						Vector3 vector = BuilderUtil.ApplySpriteVertexTileFlags(tileMap, tk2dSpriteDefinition, tk2dSpriteDefinition.positions[l], flag2, flag3, rot);
						if (useColor)
						{
							Color a2 = colorChunk.colors[j * num2 + k];
							Color b = colorChunk.colors[j * num2 + k + 1];
							Color a3 = colorChunk.colors[(j + 1) * num2 + k];
							Color b2 = colorChunk.colors[(j + 1) * num2 + (k + 1)];
							Vector3 a4 = vector - tk2dSpriteDefinition.untrimmedBoundsData[0];
							Vector3 vector2 = a4 + tileMap.data.tileSize * 0.5f;
							float t = Mathf.Clamp01(vector2.x / tileMap.data.tileSize.x);
							float t2 = Mathf.Clamp01(vector2.y / tileMap.data.tileSize.y);
							Color item = Color.Lerp(Color.Lerp(a2, b, t), Color.Lerp(a3, b2, t), t2);
							list2.Add(item);
						}
						else
						{
							list2.Add(c);
						}
						list.Add(a + vector);
						list3.Add(tk2dSpriteDefinition.uvs[l]);
					}
					bool flag4 = false;
					if (flag2)
					{
						flag4 = !flag4;
					}
					if (flag3)
					{
						flag4 = !flag4;
					}
					List<int> list4 = array[tk2dSpriteDefinition.materialId];
					for (int m = 0; m < tk2dSpriteDefinition.indices.Length; m++)
					{
						int num4 = (!flag4) ? m : (tk2dSpriteDefinition.indices.Length - 1 - m);
						list4.Add(count + tk2dSpriteDefinition.indices[num4]);
					}
				}
			}
			if (chunk.mesh == null)
			{
				chunk.mesh = tk2dUtil.CreateMesh();
			}
			chunk.mesh.vertices = list.ToArray();
			chunk.mesh.uv = list3.ToArray();
			chunk.mesh.colors = list2.ToArray();
			List<Material> list5 = new List<Material>();
			int num5 = 0;
			int num6 = 0;
			List<int>[] array2 = array;
			foreach (List<int> list6 in array2)
			{
				if (list6.Count > 0)
				{
					list5.Add(tileMap.SpriteCollectionInst.materialInsts[num5]);
					num6++;
				}
				num5++;
			}
			if (num6 > 0)
			{
				chunk.mesh.subMeshCount = num6;
				chunk.gameObject.GetComponent<Renderer>().materials = list5.ToArray();
				int num7 = 0;
				List<int>[] array3 = array;
				foreach (List<int> list7 in array3)
				{
					if (list7.Count > 0)
					{
						chunk.mesh.SetTriangles(list7.ToArray(), num7);
						num7++;
					}
				}
			}
			chunk.mesh.RecalculateBounds();
			if (flag)
			{
				chunk.mesh.RecalculateNormals();
			}
			MeshFilter component = chunk.gameObject.GetComponent<MeshFilter>();
			component.sharedMesh = chunk.mesh;
		}

		public static void Build(tk2dTileMap tileMap, bool editMode, bool forceBuild)
		{
			bool skipPrefabs = (!editMode) ? true : false;
			bool flag = !forceBuild;
			int numLayers = tileMap.data.NumLayers;
			for (int i = 0; i < numLayers; i++)
			{
				Layer layer = tileMap.Layers[i];
				if (layer.IsEmpty)
				{
					continue;
				}
				LayerInfo layerInfo = tileMap.data.Layers[i];
				bool useColor = !tileMap.ColorChannel.IsEmpty && tileMap.data.Layers[i].useColor;
				bool useSortingLayers = tileMap.data.useSortingLayers;
				for (int j = 0; j < layer.numRows; j++)
				{
					int baseY = j * layer.divY;
					for (int k = 0; k < layer.numColumns; k++)
					{
						int baseX = k * layer.divX;
						SpriteChunk chunk = layer.GetChunk(k, j);
						ColorChunk chunk2 = tileMap.ColorChannel.GetChunk(k, j);
						bool flag2 = chunk2?.Dirty ?? false;
						if (flag && !flag2 && !chunk.Dirty)
						{
							continue;
						}
						if (chunk.mesh != null)
						{
							chunk.mesh.Clear();
						}
						if (chunk.IsEmpty)
						{
							continue;
						}
						if (editMode || (!editMode && !layerInfo.skipMeshGeneration))
						{
							BuildForChunk(tileMap, chunk, chunk2, useColor, skipPrefabs, baseX, baseY);
							if (chunk.gameObject != null && useSortingLayers)
							{
								Renderer component = chunk.gameObject.GetComponent<Renderer>();
								if (component != null)
								{
									component.sortingLayerName = layerInfo.sortingLayerName;
									component.sortingOrder = layerInfo.sortingOrder;
								}
							}
						}
						if (chunk.mesh != null)
						{
							tileMap.TouchMesh(chunk.mesh);
						}
					}
				}
			}
		}
	}
}
