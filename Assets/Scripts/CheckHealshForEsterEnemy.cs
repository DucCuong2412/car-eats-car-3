using UnityEngine;

public class CheckHealshForEsterEnemy : MonoBehaviour
{
	public Car2DAIController C2DAC;

	[Header("Type 1")]
	public Animator anim11;

	public Animator anim21;

	public Animator anim31;

	public Animator anim41;

	[Header("Type 2")]
	public Animator anim12;

	public Animator anim22;

	public Animator anim32;

	public Animator anim42;

	private void Update()
	{
		float value = C2DAC.HealthModule._Barrel.Value / C2DAC.HealthModule._Barrel.MaxValue * 100f;
		if (C2DAC.constructor.carType == 1)
		{
			if (C2DAC.constructor.isAhead)
			{
				anim11.SetFloat("hull_integrity", value);
				anim21.SetFloat("hull_integrity", value);
			}
			else
			{
				anim31.SetFloat("hull_integrity", value);
				anim41.SetFloat("hull_integrity", value);
			}
		}
		else if (C2DAC.constructor.carType == 2)
		{
			if (C2DAC.constructor.isAhead)
			{
				anim12.SetFloat("hull_integrity", value);
				anim22.SetFloat("hull_integrity", value);
			}
			else
			{
				anim32.SetFloat("hull_integrity", value);
				anim42.SetFloat("hull_integrity", value);
			}
		}
	}
}
