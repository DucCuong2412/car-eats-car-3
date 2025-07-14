using UnityEngine;
using UnityEngine.UI;

public class GarageCarList_CarBut : MonoBehaviour
{
	public int Index;

	public Button But;

	public Animator ButStageAnim;

	private GarageCarList_Controller _controller;

	private int is_owned = Animator.StringToHash("is_owned");

	private int is_active = Animator.StringToHash("is_active");

	private void Start()
	{
		_controller = UnityEngine.Object.FindObjectOfType<GarageCarList_Controller>();
	}

	private void OnEnable()
	{
		But.onClick.AddListener(PressBut);
	}

	public void SetActivate(bool active)
	{
		if (active)
		{
			ButStageAnim.SetBool(is_active, value: true);
		}
		else
		{
			ButStageAnim.SetBool(is_active, value: false);
		}
	}

	public void SetBuyed(bool buy)
	{
		if (buy)
		{
			ButStageAnim.SetBool(is_owned, value: true);
		}
		else
		{
			ButStageAnim.SetBool(is_owned, value: false);
		}
	}

	private void PressBut()
	{
		_controller.PressCar(Index);
		Audio.PlayAsync("car_choose");
	}

	private void OnDisable()
	{
		But.onClick.RemoveAllListeners();
	}
}
