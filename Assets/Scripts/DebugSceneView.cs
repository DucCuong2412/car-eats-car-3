using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSceneView : MonoBehaviour
{
	public DebugSceneModel model;

	public Button backButton;

	public Button loadAllButton;

	public Toggle toggle;

	public Button clearButton;

	public GameObject loader;

	public DebugSceneConfigNode configNodeView;

	public RectTransform rectScroll;

	private DebugConfig config;

	private List<DebugSceneConfigNode> allNodes = new List<DebugSceneConfigNode>();

	private IEnumerator Start()
	{
		toggle.isOn = ConfigSettings.Instance.useCache;
		backButton.onClick.AddListener(delegate
		{
			Game.LoadLevel("map_new");
		});
		loadAllButton.onClick.AddListener(delegate
		{
			StartCoroutine(LoadAllFromServer());
		});
		toggle.onValueChanged.AddListener(delegate(bool b)
		{
			ConfigSettings.Instance.useCache = b;
		});
		clearButton.onClick.AddListener(delegate
		{
			loader.SetActive(value: true);
			DebugConfig.ClearCache();
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		});
		loader.SetActive(value: true);
		yield return new WaitForEndOfFrame();
		yield return StartCoroutine(Initialize());
		loader.SetActive(value: false);
	}

	private IEnumerator Initialize()
	{
		yield return new WaitForEndOfFrame();
		config = new DebugConfig();
		config.Init();
		yield return new WaitForEndOfFrame();
		Init(config.GetAvailableConfigs());
	}

	public void Init(Dictionary<string, DebugConfig.ConfigPair> configs)
	{
		int num = 47;
		int count = configs.Keys.Count;
		RectTransform rectTransform = rectScroll;
		Vector2 sizeDelta = rectScroll.sizeDelta;
		rectTransform.sizeDelta = new Vector2(sizeDelta.x, num * count);
		int num2 = 0;
		foreach (string key in configs.Keys)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(configNodeView.gameObject);
			gameObject.GetComponent<RectTransform>().SetParent(configNodeView.transform.parent);
			gameObject.transform.localScale = configNodeView.transform.localScale;
			gameObject.GetComponent<RectTransform>().localPosition = configNodeView.GetComponent<RectTransform>().localPosition - new Vector3(0f, num * num2, 0f);
			DebugSceneConfigNode component = gameObject.GetComponent<DebugSceneConfigNode>();
			component.Init(LoadFromServer, key, (!configs[key].local.cached) ? string.Empty : configs[key].local.ToString(), (!(configs[key].server.lastModified > configs[key].local.lastModified)) ? string.Empty : configs[key].server.ToString());
			if (configs[key].server.lastModified < configs[key].local.lastModified)
			{
				component.loadButton.onClick.RemoveAllListeners();
			}
			allNodes.Add(component);
			num2++;
		}
		configNodeView.gameObject.SetActive(value: false);
	}

	public void LoadFromServer(string key)
	{
		loader.SetActive(value: true);
		StartCoroutine(config.UpdateFile(key, updateCallback));
	}

	public IEnumerator LoadAllFromServer()
	{
		foreach (DebugSceneConfigNode node in allNodes)
		{
			node.loadButton.onClick.Invoke();
			while (loader.activeSelf)
			{
				yield return null;
			}
		}
		loader.SetActive(value: false);
	}

	private void updateCallback(bool success)
	{
		updated();
	}

	private void updated()
	{
		loader.SetActive(value: false);
	}
}
