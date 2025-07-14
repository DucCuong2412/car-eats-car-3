using UnityEngine;

public class MoveOnPathScript : MonoBehaviour
{
	public EditorPathScript PathToFollow;

	public int CurrentWaiPointID;

	public float speed;

	public float reachDistance = 1f;

	public float rotationSpeed = 5f;

	public string pathName;

	public Animator RotateAnim;

	public Animator DropAnim;

	public GameObject GO;

	public DailyBonusSetting DBS;

	private int _isMoveRight = Animator.StringToHash("isMoveRight");

	private int _isBombDropped = Animator.StringToHash("isBombDropped");

	private void Update()
	{
		float num = Vector3.Distance(PathToFollow.path_objs[CurrentWaiPointID].position, base.transform.position);
		base.transform.position = Vector3.MoveTowards(base.transform.position, PathToFollow.path_objs[CurrentWaiPointID].position, Time.deltaTime * speed);
		if (num <= reachDistance)
		{
			CurrentWaiPointID++;
		}
		if (CurrentWaiPointID >= PathToFollow.path_objs.Count)
		{
			CurrentWaiPointID = 0;
			DBS.list[DBS.TempZmina].Drone.SetActive(value: true);
			Audio.Stop("drones");
			GO.SetActive(value: false);
		}
		RotateAnim.SetBool(_isMoveRight, PathToFollow.path_objs[CurrentWaiPointID].GetComponent<ForRotateOrDropDombDroneInDailyBonus>().needRotate);
		if (PathToFollow.path_objs[CurrentWaiPointID].GetComponent<ForRotateOrDropDombDroneInDailyBonus>().needDrop)
		{
			DropAnim.SetTrigger(_isBombDropped);
		}
	}
}
