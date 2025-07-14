using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollForPolicopedia : MonoBehaviour
{
	public RectTransform panel;

	public Button[] bttn;

	public RectTransform center;

	public Button left;

	public Button right;

	public float Larping = 5f;

	public float DistToNext = 50f;

	private int goToCardX = -1;

	private float[] distance;

	public bool dragging;

	private int bttnDistance;

	private int minButtonNum;

	private int startCard = -1;

	private int startStartCard = -1;

	private void OnEnable()
	{
		left.onClick.AddListener(delegate
		{
			SetgoToCardX(right: false);
		});
		right.onClick.AddListener(delegate
		{
			SetgoToCardX();
		});
	}

	private void OnDisable()
	{
		left.onClick.RemoveAllListeners();
		right.onClick.RemoveAllListeners();
	}

	private void Start()
	{
		int num = bttn.Length;
		distance = new float[num];
		Vector2 anchoredPosition = bttn[1].GetComponent<RectTransform>().anchoredPosition;
		float x = anchoredPosition.x;
		Vector2 anchoredPosition2 = bttn[0].GetComponent<RectTransform>().anchoredPosition;
		bttnDistance = (int)Mathf.Abs(x - anchoredPosition2.x);
		if (Progress.shop.activeCar == 7)
		{
			SetgoToCardX();
		}
		else if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
		{
			SetgoToCardX();
		}
		if (Progress.shop.PolispediaChusenCarFromGarage != -1)
		{
			if (Progress.shop.PolispediaChusenCarFromGarage == 1)
			{
				SetgoToCardX(right: true, 1);
			}
			else if (Progress.shop.PolispediaChusenCarFromGarage == 2)
			{
				SetgoToCardX(right: true, 2);
			}
			Progress.shop.PolispediaChusenCarFromGarage = -1;
		}
		StartCoroutine(tests());
	}

	public bool pressBut(int num)
	{
		minButtonNum = GetMinButtonNum();
		if (num == minButtonNum)
		{
			return false;
		}
		if (num > minButtonNum)
		{
			SetgoToCardX();
			return true;
		}
		if (num < minButtonNum)
		{
			SetgoToCardX(right: false);
			return true;
		}
		return false;
	}

	public void SetPanelNum(int LastLigNum)
	{
		StartCoroutine(timeOut(LastLigNum));
	}

	private IEnumerator timeOut(int LastLigNum = 0)
	{
		dragging = true;
		int t = 3;
		while (t > 0)
		{
			RectTransform rectTransform = panel;
			float x = LastLigNum * -bttnDistance;
			Vector2 anchoredPosition = panel.anchoredPosition;
			rectTransform.anchoredPosition = new Vector2(x, anchoredPosition.y);
			t--;
			yield return null;
		}
		dragging = false;
	}

	public void SetgoToCardX(bool right = true, int numBut = -1)
	{
		minButtonNum = GetMinButtonNum();
		if (numBut != -1)
		{
			minButtonNum = numBut - 1;
		}
		dragging = true;
		if (minButtonNum < 3 && right)
		{
			goToCardX = (minButtonNum + 1) * bttnDistance;
		}
		else if (minButtonNum > 0 && !right)
		{
			goToCardX = (minButtonNum - 1) * bttnDistance;
		}
		StartCoroutine(tests());
	}

	private void Update()
	{
		GetMinButtonNum();
		if (!dragging)
		{
			if (goToCardX != -1)
			{
				goToCardX = -1;
			}
			LerpToBttn(minButtonNum * -bttnDistance);
		}
		if (goToCardX >= 0)
		{
			LerpToBttn(-goToCardX);
			Vector2 anchoredPosition = panel.anchoredPosition;
			if (anchoredPosition.x - 2f < (float)(-goToCardX))
			{
				Vector2 anchoredPosition2 = panel.anchoredPosition;
				if (anchoredPosition2.x + 2f > (float)(-goToCardX))
				{
					RectTransform rectTransform = panel;
					float x = -goToCardX;
					Vector2 anchoredPosition3 = panel.anchoredPosition;
					rectTransform.anchoredPosition = new Vector2(x, anchoredPosition3.y);
					goToCardX = -1;
					dragging = false;
				}
			}
		}
		Vector2 anchoredPosition4 = panel.anchoredPosition;
		if (anchoredPosition4.x > 100f && dragging)
		{
			RectTransform rectTransform2 = panel;
			Vector2 anchoredPosition5 = panel.anchoredPosition;
			rectTransform2.anchoredPosition = new Vector2(100f, anchoredPosition5.y);
			return;
		}
		Vector2 anchoredPosition6 = panel.anchoredPosition;
		if (anchoredPosition6.x < -2700f && dragging)
		{
			RectTransform rectTransform3 = panel;
			Vector2 anchoredPosition7 = panel.anchoredPosition;
			rectTransform3.anchoredPosition = new Vector2(-2700f, anchoredPosition7.y);
		}
	}

	public int GetMinButtonNum()
	{
		for (int i = 0; i < bttn.Length; i++)
		{
			float[] array = distance;
			int num = i;
			Vector3 position = center.transform.position;
			float x = position.x;
			Vector3 position2 = bttn[i].transform.position;
			array[num] = Mathf.Abs(x - position2.x);
		}
		float num2 = Mathf.Min(distance);
		for (int j = 0; j < bttn.Length; j++)
		{
			if (num2 == distance[j])
			{
				minButtonNum = j;
			}
		}
		if (minButtonNum == startCard)
		{
			startCard = -1;
		}
		if (startCard != -1)
		{
			minButtonNum = startCard;
		}
		return minButtonNum;
	}

	private void LerpToBttn(int position)
	{
		Vector2 anchoredPosition = panel.anchoredPosition;
		float num = Mathf.Lerp(anchoredPosition.x, position, Time.deltaTime * Larping);
		float x = num;
		Vector2 anchoredPosition2 = panel.anchoredPosition;
		Vector2 anchoredPosition3 = new Vector2(x, anchoredPosition2.y);
		panel.anchoredPosition = anchoredPosition3;
	}

	public void StartDrag()
	{
		goToCardX = -1;
		dragging = true;
		startCard = -1;
		startStartCard = GetMinButtonNum();
	}

	public void EndDrag()
	{
		startCard = -1;
		startCard = GetMinButtonNum();
		if (startCard == -1)
		{
			goto IL_01c8;
		}
		Vector3 position = bttn[startCard].transform.position;
		if (!(position.x > DistToNext))
		{
			Vector3 position2 = bttn[startCard].transform.position;
			if (!(position2.x < 0f - DistToNext))
			{
				goto IL_01c8;
			}
		}
		Vector3 position3 = bttn[startCard].transform.position;
		if (position3.x - 480f > DistToNext)
		{
			startCard--;
		}
		else
		{
			Vector3 position4 = bttn[startCard].transform.position;
			if (position4.x < 0f - DistToNext)
			{
				startCard++;
			}
		}
		if (startCard < 0 || startCard > 3)
		{
			startCard = -1;
		}
		if (startStartCard == startCard)
		{
			Vector3 position5 = bttn[startCard].transform.position;
			if (position5.x - 480f > DistToNext)
			{
				startCard--;
			}
			else
			{
				Vector3 position6 = bttn[startCard].transform.position;
				if (position6.x - 480f < 0f - DistToNext)
				{
					startCard++;
				}
			}
			if (startCard < 0 || startCard > 2)
			{
				startCard = -1;
			}
		}
		goto IL_01cf;
		IL_01cf:
		dragging = false;
		startStartCard = -1;
		StartCoroutine(tests());
		return;
		IL_01c8:
		startCard = -1;
		goto IL_01cf;
	}

	private IEnumerator tests()
	{
		yield return new WaitForSeconds(0.2f);
		switch (GetMinButtonNum() - 1)
		{
		case -1:
			left.gameObject.SetActive(value: false);
			right.gameObject.SetActive(value: true);
			break;
		case 1:
			left.gameObject.SetActive(value: true);
			right.gameObject.SetActive(value: false);
			break;
		default:
			left.gameObject.SetActive(value: true);
			right.gameObject.SetActive(value: true);
			break;
		}
	}
}
