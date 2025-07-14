using UnityEngine;

namespace Analytics
{
	public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T s_Instance;

		private static bool s_IsDestroyed;

		public static T Instance
		{
			get
			{
				if (s_IsDestroyed)
				{
					return (T)null;
				}
				if ((Object)s_Instance == (Object)null)
				{
					s_Instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
					if ((Object)s_Instance == (Object)null)
					{
						GameObject gameObject = new GameObject(typeof(T).Name);
						Object.DontDestroyOnLoad(gameObject);
						s_Instance = (gameObject.AddComponent(typeof(T)) as T);
					}
				}
				return s_Instance;
			}
		}

		protected virtual void OnDestroy()
		{
			if ((bool)(Object)s_Instance)
			{
				UnityEngine.Object.Destroy(s_Instance);
			}
			s_Instance = (T)null;
			s_IsDestroyed = true;
		}
	}
}
