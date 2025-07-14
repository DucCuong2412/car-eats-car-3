using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TESTTESTTESTTEST : MonoBehaviour
{
	public enum test
	{
		win,
		lose
	}

	public int kill;

	public int coll;

	public float perc;

	public test types;

	private Results_new_controller Res_New_Contr;

	private Results_lost_new_controller R_L_N_C;

	private void OnEnable()
	{
		SceneManager.LoadScene("results_new", LoadSceneMode.Additive);
		if (types == test.win)
		{
			StartCoroutine(qweqwe());
		}
		else if (types == test.lose)
		{
			StartCoroutine(zcxzc());
		}
	}

	private IEnumerator qweqwe()
	{
		yield return new WaitForSeconds(0.2f);
		GameObject GOC = GameObject.Find("HUD_Pause_Results");
		Results_Glogal_controller RES_CONTR_Glob = GOC.GetComponentInChildren<Results_Glogal_controller>();
		Res_New_Contr = RES_CONTR_Glob.R_N_C;
		Res_New_Contr.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(4f);
		Res_New_Contr.Results_Suma(coll, kill, perc);
	}

	private IEnumerator zcxzc()
	{
		yield return new WaitForSeconds(0.2f);
		GameObject GOC = GameObject.Find("HUD_Pause_Results");
		Results_Glogal_controller RES_CONTR_Glob = GOC.GetComponentInChildren<Results_Glogal_controller>();
		R_L_N_C = RES_CONTR_Glob.R_L_N_C;
		R_L_N_C.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		R_L_N_C.Results_Suma(coll, kill, perc);
	}
}
