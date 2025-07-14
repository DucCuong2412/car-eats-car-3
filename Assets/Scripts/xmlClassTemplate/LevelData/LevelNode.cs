using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.AIObject;
using xmlClassTemplate.AIObjectCivic;
using xmlClassTemplate.CollectibleObject;
using xmlClassTemplate.CollectibleObjectRuby;
using xmlClassTemplate.ConstructionObject;
using xmlClassTemplate.DynamicObject;
using xmlClassTemplate.GroundObject;
using xmlClassTemplate.StaticObject;

namespace xmlClassTemplate.LevelData
{
	public class LevelNode
	{
		public List<GroundObjectNode> l_ground = new List<GroundObjectNode>();

		public List<StaticObjectNode> l_static = new List<StaticObjectNode>();

		public List<DynamicObjectNode> l_dynamic = new List<DynamicObjectNode>();

		public List<CollectibleObjectNode> l_colletible = new List<CollectibleObjectNode>();

		public List<CollectibleObjectRubyNode> l_colletibleRuby = new List<CollectibleObjectRubyNode>();

		public List<ConstructionObjectNode> l_construction = new List<ConstructionObjectNode>();

		public List<AIObjectNode> l_ai = new List<AIObjectNode>();

		public List<AIObjectNodeCivic> l_ai_c = new List<AIObjectNodeCivic>();

		public float levelLength;

		public Vector2 levelOffset = Vector2.zero;

		public Vector2 startPosition = Vector2.one;

		public float finishPosition;

		public LevelNode Clone()
		{
			LevelNode levelNode = new LevelNode();
			levelNode.levelLength = levelLength;
			levelNode.finishPosition = finishPosition;
			levelNode.startPosition = startPosition;
			levelNode.levelOffset = levelOffset;
			for (int i = 0; i < l_ai.Count; i++)
			{
				levelNode.l_ai.Add(l_ai[i].Clone());
			}
			for (int j = 0; j < l_ai_c.Count; j++)
			{
				levelNode.l_ai_c.Add(l_ai_c[j].Clone());
			}
			for (int k = 0; k < l_ground.Count; k++)
			{
				levelNode.l_ground.Add(l_ground[k].Clone());
			}
			for (int l = 0; l < l_static.Count; l++)
			{
				levelNode.l_static.Add(l_static[l].Clone());
			}
			for (int m = 0; m < l_dynamic.Count; m++)
			{
				levelNode.l_dynamic.Add(l_dynamic[m].Clone());
			}
			for (int n = 0; n < l_colletible.Count; n++)
			{
				levelNode.l_colletible.Add(l_colletible[n].Clone());
			}
			for (int num = 0; num < l_colletibleRuby.Count; num++)
			{
				levelNode.l_colletibleRuby.Add(l_colletibleRuby[num].Clone());
			}
			for (int num2 = 0; num2 < l_construction.Count; num2++)
			{
				levelNode.l_construction.Add(l_construction[num2].Clone());
			}
			return levelNode;
		}
	}
}
