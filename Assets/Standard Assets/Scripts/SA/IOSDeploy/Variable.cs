using SA.Common.Util;
using System;
using System.Collections.Generic;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Variable
	{
		public bool IsOpen = true;

		public bool IsListOpen = true;

		public string Name = string.Empty;

		public PlistValueTypes Type;

		public string StringValue = string.Empty;

		public int IntegerValue;

		public float FloatValue;

		public bool BooleanValue = true;

		public List<string> ChildrensIds = new List<string>();

		public void AddChild(Variable v)
		{
			string text = IdFactory.NextId.ToString();
			ISD_Settings.Instance.AddVariableToDictionary(text, v);
			ChildrensIds.Add(text);
		}
	}
}
