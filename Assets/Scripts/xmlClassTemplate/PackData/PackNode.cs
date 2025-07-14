using System.Collections.Generic;
using xmlClassTemplate.LevelData;

namespace xmlClassTemplate.PackData
{
	public class PackNode
	{
		public List<LevelNode> levels = new List<LevelNode>();

		public PackNode Clone()
		{
			PackNode packNode = new PackNode();
			for (int i = 0; i < levels.Count; i++)
			{
				packNode.levels.Add(levels[i].Clone());
			}
			return packNode;
		}
	}
}
