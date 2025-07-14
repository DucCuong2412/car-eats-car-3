using SmartLocalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettings : MonoBehaviour
{
	public LevelGalleryCanvasView LGCV;

	public GameObject PlayBtnObj;

	[Header("Langs")]
	public Button US;

	public Button UA;

	public Button RU;

	public Button CN;

	public Button TW;

	public Button GB;

	public Button FR;

	public Button DE;

	public Button ID;

	public Button IT;

	public Button JP;

	public Button KR;

	public Button PL;

	public Button BR;

	public Button ES;

	public Button SE;

	public Button TR;

	public Button NL;

	public Button IR;

	public GameObject OnButUS;

	public GameObject OnButUA;

	public GameObject OnButRU;

	public GameObject OnButCN;

	public GameObject OnButTW;

	public GameObject OnButGB;

	public GameObject OnButFR;

	public GameObject OnButDE;

	public GameObject OnButID;

	public GameObject OnButIT;

	public GameObject OnButJP;

	public GameObject OnButKR;

	public GameObject OnButPL;

	public GameObject OnButBR;

	public GameObject OnButES;

	public GameObject OnButSE;

	public GameObject OnButTR;

	public GameObject OnButNL;

	public GameObject OnButIR;

	private LocalizationManager lm;

	private void OnEnable()
	{
		lm = LocalizationManager.instance;
		US.onClick.AddListener(PressUS);
		UA.onClick.AddListener(PressUA);
		RU.onClick.AddListener(PressRU);
		CN.onClick.AddListener(PressCN);
		TW.onClick.AddListener(PressTW);
		GB.onClick.AddListener(PressGB);
		FR.onClick.AddListener(PressFR);
		DE.onClick.AddListener(PressDE);
		ID.onClick.AddListener(PressID);
		IT.onClick.AddListener(PressIT);
		JP.onClick.AddListener(PressJP);
		KR.onClick.AddListener(PressKR);
		PL.onClick.AddListener(PressPL);
		BR.onClick.AddListener(PressBR);
		ES.onClick.AddListener(PressES);
		SE.onClick.AddListener(PressSE);
		TR.onClick.AddListener(PressTR);
		NL.onClick.AddListener(PressNL);
		IR.onClick.AddListener(PressIR);
		OnButUS.SetActive(value: false);
		OnButUA.SetActive(value: false);
		OnButRU.SetActive(value: false);
		OnButCN.SetActive(value: false);
		OnButTW.SetActive(value: false);
		OnButGB.SetActive(value: false);
		OnButFR.SetActive(value: false);
		OnButDE.SetActive(value: false);
		OnButID.SetActive(value: false);
		OnButIT.SetActive(value: false);
		OnButJP.SetActive(value: false);
		OnButKR.SetActive(value: false);
		OnButPL.SetActive(value: false);
		OnButBR.SetActive(value: false);
		OnButES.SetActive(value: false);
		OnButSE.SetActive(value: false);
		OnButTR.SetActive(value: false);
		OnButNL.SetActive(value: false);
		OnButIR.SetActive(value: false);
		Change();
	}

	private void Change()
	{
		if (lm.currentlyLanguageCode == "en-US")
		{
			OnButUS.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "uk-UA")
		{
			OnButUA.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "ru-RU")
		{
			OnButRU.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "zh-CN")
		{
			OnButCN.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "zh-TW")
		{
			OnButTW.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "en-GB")
		{
			OnButGB.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "fr-FR")
		{
			OnButFR.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "de-DE")
		{
			OnButDE.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "id-ID")
		{
			OnButID.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "it-IT")
		{
			OnButIT.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "ja-JP")
		{
			OnButJP.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "ko-KR")
		{
			OnButKR.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "pl-PL")
		{
			OnButPL.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "pt-BR")
		{
			OnButBR.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "es-ES")
		{
			OnButES.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "sv-SE")
		{
			OnButSE.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "tr-TR")
		{
			OnButTR.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "nl-NL")
		{
			OnButNL.SetActive(value: true);
		}
		else if (lm.currentlyLanguageCode == "fa-IR")
		{
			OnButIR.SetActive(value: true);
		}
	}

	private void PressUS()
	{
		SetLeng("en-US");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressUA()
	{
		SetLeng("uk-UA");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressRU()
	{
		SetLeng("ru-RU");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressCN()
	{
		SetLeng("zh-CN");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressTW()
	{
		SetLeng("zh-TW");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressGB()
	{
		SetLeng("en-GB");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressFR()
	{
		SetLeng("fr-FR");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressDE()
	{
		SetLeng("de-DE");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressID()
	{
		SetLeng("id-ID");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressIT()
	{
		SetLeng("it-IT");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressJP()
	{
		SetLeng("ja-JP");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressKR()
	{
		SetLeng("ko-KR");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressPL()
	{
		SetLeng("pl-PL");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressBR()
	{
		SetLeng("pt-BR");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressES()
	{
		SetLeng("es-ES");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressSE()
	{
		SetLeng("sv-SE");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressTR()
	{
		SetLeng("tr-TR");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressNL()
	{
		SetLeng("nl-NL");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void PressIR()
	{
		SetLeng("fa-IR");
		if (LGCV._shopWindowModel != null)
		{
			UnityEngine.Object.Destroy(LGCV._shopWindowModel.gameObject);
			LGCV._shopWindowModel = null;
		}
		StartCorut();
	}

	private void SetLeng(string str)
	{
		lm.SaveNextAvailableLanguage(str);
		LocalizationObjectReferences[] array = UnityEngine.Object.FindObjectsOfType<LocalizationObjectReferences>();
		foreach (LocalizationObjectReferences localizationObjectReferences in array)
		{
			localizationObjectReferences.RefreshLanguageLables();
		}
		OnButUS.SetActive(value: false);
		OnButUA.SetActive(value: false);
		OnButRU.SetActive(value: false);
		OnButCN.SetActive(value: false);
		OnButTW.SetActive(value: false);
		OnButGB.SetActive(value: false);
		OnButFR.SetActive(value: false);
		OnButDE.SetActive(value: false);
		OnButID.SetActive(value: false);
		OnButIT.SetActive(value: false);
		OnButJP.SetActive(value: false);
		OnButKR.SetActive(value: false);
		OnButPL.SetActive(value: false);
		OnButBR.SetActive(value: false);
		OnButES.SetActive(value: false);
		OnButSE.SetActive(value: false);
		OnButTR.SetActive(value: false);
		OnButNL.SetActive(value: false);
		OnButIR.SetActive(value: false);
		if (str == "en-US")
		{
			OnButUS.SetActive(value: true);
		}
		else if (str == "uk-UA")
		{
			OnButUA.SetActive(value: true);
		}
		else if (str == "ru-RU")
		{
			OnButRU.SetActive(value: true);
		}
		else if (str == "zh-CN")
		{
			OnButCN.SetActive(value: true);
		}
		else if (str == "zh-TW")
		{
			OnButTW.SetActive(value: true);
		}
		else if (str == "en-GB")
		{
			OnButGB.SetActive(value: true);
		}
		else if (str == "fr-FR")
		{
			OnButFR.SetActive(value: true);
		}
		else if (str == "de-DE")
		{
			OnButDE.SetActive(value: true);
		}
		else if (str == "id-ID")
		{
			OnButID.SetActive(value: true);
		}
		else if (str == "it-IT")
		{
			OnButIT.SetActive(value: true);
		}
		else if (str == "ja-JP")
		{
			OnButJP.SetActive(value: true);
		}
		else if (str == "ko-KR")
		{
			OnButKR.SetActive(value: true);
		}
		else if (str == "pl-PL")
		{
			OnButPL.SetActive(value: true);
		}
		else if (str == "pt-BR")
		{
			OnButBR.SetActive(value: true);
		}
		else if (str == "es-ES")
		{
			OnButES.SetActive(value: true);
		}
		else if (str == "sv-SE")
		{
			OnButSE.SetActive(value: true);
		}
		else if (str == "tr-TR")
		{
			OnButTR.SetActive(value: true);
		}
		else if (str == "nl-NL")
		{
			OnButNL.SetActive(value: true);
		}
		else if (str == "fa-IR")
		{
			OnButIR.SetActive(value: true);
		}
	}

	private void StartCorut()
	{
		StartCoroutine(DelayToPlayCheng());
	}

	private IEnumerator DelayToPlayCheng()
	{
		yield return null;
		PlayBtnObj.SetActive(value: false);
		yield return null;
		PlayBtnObj.SetActive(value: true);
	}
}
