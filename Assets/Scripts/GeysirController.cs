using UnityEngine;

public class GeysirController : MonoBehaviour
{
	public Animator Anim;

	public int TimeBitvinDmg = 120;

	public int TimeDmg = 60;

	public float DmgPerFrame = 0.5f;

	private int is_attack = Animator.StringToHash("isAttack");

	private int _counter;

	private void OnTriggerStay2D(Collider2D other)
	{
		if (_counter >= TimeBitvinDmg && (other.tag == "CarMain" || other.tag == "CarMainChild"))
		{
			RaceLogic.instance.HitMainCar(DmgPerFrame);
		}
	}

	private void OnTriggerEnter2D(Collider2D oth)
	{
		Audio.Play("geyser");
	}

	private void Update()
	{
		if (Game.currentState == Game.gameState.Finish || Game.currentState == Game.gameState.FinishLose || Game.currentState == Game.gameState.PreRace)
		{
			_counter = 0;
		}
		_counter++;
		if (_counter == TimeBitvinDmg)
		{
			Anim.SetBool(is_attack, value: true);
		}
		if (_counter == TimeBitvinDmg + TimeDmg)
		{
			Anim.SetBool(is_attack, value: false);
			_counter = 0;
		}
	}
}
