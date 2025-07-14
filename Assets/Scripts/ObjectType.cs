using UnityEngine;

public class ObjectType : MonoBehaviour
{
	public enum Type
	{
		Default,
		Decor,
		StaticObject,
		SimpleObject,
		Construction,
		Collectible,
		AI
	}

	public Type type;
}
