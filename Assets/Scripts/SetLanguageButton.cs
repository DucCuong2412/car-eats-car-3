using SmartLocalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SetLanguageButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	private Image im;

	private LocalizationManager lm;

	private void Start()
	{
		im = base.gameObject.GetComponent<Image>();
		lm = LocalizationManager.instance;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		lm.SaveNextAvailableLanguage(im.sprite.name);
		LocalizationObjectReferences[] array = UnityEngine.Object.FindObjectsOfType<LocalizationObjectReferences>();
		foreach (LocalizationObjectReferences localizationObjectReferences in array)
		{
			localizationObjectReferences.RefreshLanguageLables();
		}
	}
}
