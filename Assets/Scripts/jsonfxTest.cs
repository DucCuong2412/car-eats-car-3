using JsonFx.Json;
using System;
using UnityEngine;

public class jsonfxTest : MonoBehaviour
{
	public string _json;

	public string _status;

	public int _intIn;

	public uint _uintIn;

	public float _floatIn;

	public string _stringIn;

	public int _intOut;

	public uint _uintOut;

	public float _floatOut;

	public string _stringOut;

	private void Start()
	{
		_uintIn = (uint)_intIn;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 0f, 100f, 50f), _intOut.ToString());
		GUI.Label(new Rect(10f, 50f, 100f, 50f), _uintOut.ToString());
		GUI.Label(new Rect(10f, 100f, 100f, 50f), _floatOut.ToString());
		GUI.Label(new Rect(10f, 150f, 100f, 50f), _stringOut);
		GUI.Label(new Rect(10f, 200f, Screen.width - 20, 50f), _status);
		GUI.Label(new Rect(10f, 250f, Screen.width - 20, 50f), _json);
		if (GUI.Button(new Rect((float)Screen.width - 100f, 10f, 90f, 50f), "test all"))
		{
			TestDataStructAll testDataStructAll = new TestDataStructAll();
			testDataStructAll._int = _intIn;
			testDataStructAll._uint = _uintIn;
			testDataStructAll._float = _floatIn;
			testDataStructAll._string = _stringIn;
			_json = JsonWriter.Serialize(testDataStructAll);
			try
			{
				TestDataStructAll testDataStructAll2 = JsonReader.Deserialize<TestDataStructAll>(_json);
				_uintOut = testDataStructAll2._uint;
				_intOut = testDataStructAll2._int;
				_floatOut = testDataStructAll2._float;
				_stringOut = testDataStructAll2._string;
				_status = "success";
			}
			catch (Exception ex)
			{
				_status = ex.ToString();
				UnityEngine.Debug.Log(ex.ToString());
			}
		}
		if (GUI.Button(new Rect((float)Screen.width - 100f, 10f, 90f, 50f), "test all"))
		{
			TestDataStructAll testDataStructAll3 = new TestDataStructAll();
			testDataStructAll3._int = _intIn;
			testDataStructAll3._uint = _uintIn;
			testDataStructAll3._float = _floatIn;
			testDataStructAll3._string = _stringIn;
			_json = JsonWriter.Serialize(testDataStructAll3);
			try
			{
				TestDataStructAll testDataStructAll4 = JsonReader.Deserialize<TestDataStructAll>(_json);
				_uintOut = testDataStructAll4._uint;
				_intOut = testDataStructAll4._int;
				_floatOut = testDataStructAll4._float;
				_stringOut = testDataStructAll4._string;
				_status = "success";
			}
			catch (Exception ex2)
			{
				_status = ex2.ToString();
				UnityEngine.Debug.Log(ex2.ToString());
			}
		}
		if (GUI.Button(new Rect((float)Screen.width - 100f, 60f, 90f, 50f), "test int"))
		{
			TestDataStructInt testDataStructInt = new TestDataStructInt();
			testDataStructInt._int = _intIn;
			_json = JsonWriter.Serialize(testDataStructInt);
			try
			{
				TestDataStructInt testDataStructInt2 = JsonReader.Deserialize<TestDataStructInt>(_json);
				_intOut = testDataStructInt2._int;
				_status = "success";
			}
			catch (Exception ex3)
			{
				_status = ex3.ToString();
				UnityEngine.Debug.Log(ex3.ToString());
			}
		}
		if (GUI.Button(new Rect((float)Screen.width - 100f, 110f, 90f, 50f), "test uint"))
		{
			TestDataStructUint testDataStructUint = new TestDataStructUint();
			testDataStructUint._uint = _uintIn;
			_json = JsonWriter.Serialize(testDataStructUint);
			try
			{
				TestDataStructUint testDataStructUint2 = JsonReader.Deserialize<TestDataStructUint>(_json);
				_uintOut = testDataStructUint2._uint;
				_status = "success";
			}
			catch (Exception ex4)
			{
				_status = ex4.ToString();
				UnityEngine.Debug.Log(ex4.ToString());
			}
		}
		if (GUI.Button(new Rect((float)Screen.width - 100f, 160f, 90f, 50f), "test float"))
		{
			TestDataStructFloat testDataStructFloat = new TestDataStructFloat();
			testDataStructFloat._float = _floatIn;
			_json = JsonWriter.Serialize(testDataStructFloat);
			try
			{
				TestDataStructFloat testDataStructFloat2 = JsonReader.Deserialize<TestDataStructFloat>(_json);
				_floatOut = testDataStructFloat2._float;
				_status = "success";
			}
			catch (Exception ex5)
			{
				_status = ex5.ToString();
				UnityEngine.Debug.Log(ex5.ToString());
			}
		}
		if (GUI.Button(new Rect((float)Screen.width - 100f, 210f, 90f, 50f), "test string"))
		{
			TestDataStructString testDataStructString = new TestDataStructString();
			testDataStructString._string = _stringIn;
			_json = JsonWriter.Serialize(testDataStructString);
			try
			{
				TestDataStructString testDataStructString2 = JsonReader.Deserialize<TestDataStructString>(_json);
				_stringOut = testDataStructString2._string;
				_status = "success";
			}
			catch (Exception ex6)
			{
				_status = ex6.ToString();
				UnityEngine.Debug.Log(ex6.ToString());
			}
		}
	}
}
