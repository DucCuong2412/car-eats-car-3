using System;

namespace UnityEngine.Purchasing.Security
{
	public class GooglePlayTangle
	{
		private static byte[] data = Convert.FromBase64String("Drw/HA4zODcUuHa4yTM/Pz87Pj3Cy3/wnSznfP48WFLxcBL+mbwShtcbckFKBN0tg+RZHNVLb69Pjc1IWfCqvQ1cVFvC53f1OVHsycbuUpurK1G6lPv6jE7B7AOXkwTGXpEAyWUdMrCRAZF4+Gg8AEZymKz0wbPcvEsvGTGLokGYZztpNNAtMAXQfYnCCOyWfOT2yrseSfHn9x6/ahqA8vVH1sN0tsoZrFag0eHYZRPjLUaAdej2GxTHtPwHanihetOgew/dLQVlAnoP5SAYrwEi28FCNn3nHkrw/dyXByJID2SQeNo80eIB1lGmz73nvD8xPg68PzQ8vD8/Pqm0uylIR0Xf35q+JbKB4KNI0KW9lpSEsp/1qVxIVzUNqwEigzw9Pz4/");

		private static int[] order = new int[15]
		{
			0,
			9,
			2,
			4,
			11,
			12,
			12,
			13,
			11,
			12,
			13,
			11,
			12,
			13,
			14
		};

		private static int key = 62;

		public static readonly bool IsPopulated = true;

		public static byte[] Data()
		{
			if (!IsPopulated)
			{
				return null;
			}
			return Obfuscator.DeObfuscate(data, order, key);
		}
	}
}
