using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorController : MonoBehaviour
{
	private class template
	{
		private GameObject g;

		private Behaviour b;

		public template(GameObject g, Behaviour b)
		{
			this.g = g;
			this.b = b;
		}

		public void Check()
		{
			bool activeInHierarchy = g.activeInHierarchy;
			if (activeInHierarchy != b.enabled)
			{
				b.enabled = activeInHierarchy;
			}
		}
	}

	private const int steps = 4;

	private int step;

	private List<List<template>> allBehaviours = new List<List<template>>();

	public static void CullAll()
	{
		new GameObject("_animatorCulling").AddComponent<AnimatorController>();
	}

	private void Awake()
	{
		GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
		List<Animation> list = new List<Animation>();
		List<Animator> list2 = new List<Animator>();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			list.AddRange(rootGameObjects[i].GetComponentsInChildren<Animation>(includeInactive: true));
			list2.AddRange(rootGameObjects[i].GetComponentsInChildren<Animator>(includeInactive: true));
		}
		List<Behaviour> list3 = new List<Behaviour>();
		foreach (Animation item in list)
		{
			list3.Add(item);
		}
		foreach (Animator item2 in list2)
		{
			list3.Add(item2);
		}
		int count = list3.Count;
		int num = count / 4;
		for (int j = 0; j < 4; j++)
		{
			List<template> list4 = new List<template>();
			int num2 = j * num;
			int num3 = num2 + num;
			for (int k = num2; k < num3; k++)
			{
				list4.Add(new template(list3[k].gameObject, list3[k]));
			}
			if (j + 1 == 4)
			{
				for (int l = num3 + 1; l < count; l++)
				{
					list4.Add(new template(list3[l].gameObject, list3[l]));
				}
			}
			allBehaviours.Add(list4);
		}
	}

	private void Update()
	{
		List<template> list = allBehaviours[step];
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			list[i].Check();
		}
		step++;
		step %= 4;
	}
}
