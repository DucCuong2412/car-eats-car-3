using UnityEngine;
using UnityEngine.UI;

public class SectorFortuneNew : MonoBehaviour
{
	public enum ClasAmount
	{
		Level1,
		Level2,
		Level3
	}

	public enum SectorType
	{
		PersentRubins,
		PersentHealth,
		PersentTurbo,
		PersentDamage,
		AddOneBomb,
		None
	}

	public SectorType sectorType;

	public ClasAmount amount;

	public GameObject ruby;

	public GameObject hp;

	public GameObject turbo;

	public GameObject damage;

	public GameObject bomb;

	public GameObject none;

	public FortuneNEw FN;

	public Text texts;

	[Header("winIcons")]
	public GameObject win1;

	public GameObject win2;

	public GameObject win3;

	public GameObject win1bomb;

	public GameObject win2bomb;

	public GameObject win3bomb;

	[Header("animWin")]
	public GameObject anim;

	private void OnEnable()
	{
		switch (sectorType)
		{
		case SectorType.PersentRubins:
			ruby.SetActive(value: true);
			hp.SetActive(value: false);
			turbo.SetActive(value: false);
			damage.SetActive(value: false);
			bomb.SetActive(value: false);
			break;
		case SectorType.PersentTurbo:
			ruby.SetActive(value: false);
			hp.SetActive(value: false);
			turbo.SetActive(value: true);
			damage.SetActive(value: false);
			bomb.SetActive(value: false);
			break;
		case SectorType.PersentDamage:
			ruby.SetActive(value: false);
			hp.SetActive(value: false);
			turbo.SetActive(value: false);
			damage.SetActive(value: true);
			bomb.SetActive(value: false);
			break;
		case SectorType.PersentHealth:
			ruby.SetActive(value: false);
			hp.SetActive(value: true);
			turbo.SetActive(value: false);
			damage.SetActive(value: false);
			bomb.SetActive(value: false);
			break;
		case SectorType.None:
			none.SetActive(value: false);
			break;
		case SectorType.AddOneBomb:
			ruby.SetActive(value: false);
			hp.SetActive(value: false);
			turbo.SetActive(value: false);
			damage.SetActive(value: false);
			bomb.SetActive(value: true);
			break;
		}
		if (!(FN != null) || !(texts != null))
		{
			return;
		}
		switch (amount)
		{
		case ClasAmount.Level1:
			if (sectorType == SectorType.AddOneBomb)
			{
				win1bomb.SetActive(value: true);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			else if (sectorType == SectorType.None)
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			else
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: true);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			break;
		case ClasAmount.Level2:
			if (sectorType == SectorType.AddOneBomb)
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: true);
			}
			else if (sectorType == SectorType.None)
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			else
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: true);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			break;
		case ClasAmount.Level3:
			if (sectorType == SectorType.AddOneBomb)
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: true);
				win2bomb.SetActive(value: false);
			}
			else if (sectorType == SectorType.None)
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: false);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			else
			{
				win1bomb.SetActive(value: false);
				win1.SetActive(value: false);
				win2.SetActive(value: false);
				win3.SetActive(value: true);
				win3bomb.SetActive(value: false);
				win2bomb.SetActive(value: false);
			}
			break;
		}
	}
}
