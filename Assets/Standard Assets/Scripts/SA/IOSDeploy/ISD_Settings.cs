using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_Settings : ScriptableObject
	{
		public const string VERSION_NUMBER = "2.3.1";

		public bool IsfwSettingOpen;

		public bool IsLibSettingOpen;

		public bool IslinkerSettingOpne;

		public bool IscompilerSettingsOpen;

		public bool IsPlistSettingsOpen;

		public bool IsLanguageSettingOpen = true;

		public bool IsBuildSettingsOpen;

		public bool enableBitCode;

		public bool enableTestability;

		public bool generateProfilingCode;

		public List<Framework> Frameworks = new List<Framework>();

		public List<Lib> Libraries = new List<Lib>();

		public List<string> compileFlags = new List<string>();

		public List<string> linkFlags = new List<string>();

		public List<Variable> PlistVariables = new List<Variable>();

		public List<VariableId> VariableDictionary = new List<VariableId>();

		public List<string> langFolders = new List<string>();

		private const string ISDAssetName = "ISD_Settings";

		private const string ISDAssetExtension = ".asset";

		private static ISD_Settings instance;

		public static ISD_Settings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (Resources.Load("ISD_Settings") as ISD_Settings);
					if (instance == null)
					{
						instance = ScriptableObject.CreateInstance<ISD_Settings>();
					}
				}
				return instance;
			}
		}

		public void AddNewVariable(Variable var)
		{
			PlistVariables.Add(var);
		}

		public void AddVariableToDictionary(string uniqueIdKey, Variable var)
		{
			VariableId variableId = new VariableId();
			variableId.uniqueIdKey = uniqueIdKey;
			variableId.VariableValue = var;
			VariableDictionary.Add(variableId);
		}

		public void RemoveVariable(Variable v, IList ListWithThisVariable)
		{
			if (Instance.PlistVariables.Contains(v))
			{
				Instance.PlistVariables.Remove(v);
			}
			else
			{
				foreach (VariableId item in VariableDictionary)
				{
					if (item.VariableValue.Equals(v))
					{
						VariableDictionary.Remove(item);
						string uniqueIdKey = item.uniqueIdKey;
						if (ListWithThisVariable.Contains(uniqueIdKey))
						{
							ListWithThisVariable.Remove(item.uniqueIdKey);
						}
						break;
					}
				}
			}
		}

		public Variable getVariableByKey(string uniqueIdKey)
		{
			foreach (VariableId item in VariableDictionary)
			{
				if (item.uniqueIdKey.Equals(uniqueIdKey))
				{
					return item.VariableValue;
				}
			}
			return null;
		}

		public Variable GetVariableByName(string name)
		{
			foreach (Variable plistVariable in Instance.PlistVariables)
			{
				if (plistVariable.Name.Equals(name))
				{
					return plistVariable;
				}
			}
			return null;
		}

		public string getKeyFromVariable(Variable var)
		{
			foreach (VariableId item in VariableDictionary)
			{
				if (item.VariableValue.Equals(var))
				{
					return item.uniqueIdKey;
				}
			}
			return null;
		}

		public bool ContainsFreamworkWithName(string name)
		{
			foreach (Framework framework in Instance.Frameworks)
			{
				if (framework.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsPlistVarWithName(string name)
		{
			foreach (Variable plistVariable in Instance.PlistVariables)
			{
				if (plistVariable.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public void AddPlistVariable(Variable newVariable)
		{
			if (Instance.PlistVariables.Count > 0)
			{
				foreach (Variable plistVariable in Instance.PlistVariables)
				{
					if (plistVariable.Name.Equals(newVariable.Name))
					{
						if (plistVariable.Type.Equals(newVariable.Type))
						{
							switch (plistVariable.Type)
							{
							case PlistValueTypes.Dictionary:
								foreach (string childrensId in newVariable.ChildrensIds)
								{
									plistVariable.AddChild(Instance.getVariableByKey(childrensId));
								}
								break;
							case PlistValueTypes.Array:
								foreach (string childrensId2 in newVariable.ChildrensIds)
								{
									plistVariable.AddChild(Instance.getVariableByKey(childrensId2));
								}
								break;
							case PlistValueTypes.Boolean:
								plistVariable.BooleanValue = newVariable.BooleanValue;
								break;
							case PlistValueTypes.Float:
								plistVariable.FloatValue = newVariable.FloatValue;
								break;
							case PlistValueTypes.Integer:
								plistVariable.IntegerValue = newVariable.IntegerValue;
								break;
							case PlistValueTypes.String:
								plistVariable.StringValue = newVariable.StringValue;
								break;
							}
						}
						else
						{
							RemoveVariable(plistVariable, Instance.PlistVariables);
							Instance.PlistVariables.Add(newVariable);
						}
					}
					else
					{
						Instance.PlistVariables.Add(newVariable);
					}
				}
			}
			else
			{
				Instance.PlistVariables.Add(newVariable);
			}
		}

		public bool ContainsLibWithName(string name)
		{
			foreach (Lib library in Instance.Libraries)
			{
				if (library.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}
	}
}
