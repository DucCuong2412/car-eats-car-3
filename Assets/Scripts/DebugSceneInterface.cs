using System;

public interface DebugSceneInterface
{
	bool isUse
	{
		get;
		set;
	}

	bool hasNewVersion
	{
		get;
	}

	string currentVersion
	{
		get;
	}

	void UpdateNode(Action<bool> callbaсk);
}
