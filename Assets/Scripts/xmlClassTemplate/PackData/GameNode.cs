using System.Collections.Generic;

namespace xmlClassTemplate.PackData
{
	public class GameNode
	{
		public List<PackNode> packs = new List<PackNode>();

		public GameNode Clone()
		{
			GameNode gameNode = new GameNode();
			for (int i = 0; i < packs.Count; i++)
			{
				gameNode.packs.Add(packs[i].Clone());
			}
			return gameNode;
		}
	}
}
