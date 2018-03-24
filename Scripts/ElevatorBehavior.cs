using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElevatorBehavior : WorkerBehavior {


	public List <Vector3> elevatorPickUpLocations;
	public List <GameObject> mineShafts;
	public List <BucketBehavior> elevatorPickUpBucketScripts;

	private int currentTargetShaft = -1;

	private Vector2 elevatorDropOff = new Vector2 (-5.35f, -1.77f);

	public override void Awake () {
		
		rb2D = GetComponent <Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;

		mineShafts = GetMineShaftsInDescendingVerticalOrder (mineShafts);
		SetElevatorPickUpsAndDropOff (mineShafts);

		ProceedToNextPickUp ();
	}
		
	public override IEnumerator PickUpAndTurnBack (double amount, float currentLoadTime) {
		yield return StartCoroutine (base.PickUpAndTurnBack (amount, currentLoadTime));
		print ("should be picking up now");
		if (currentAmountCarried == carryCapacity || currentTargetShaft == (mineShafts.Count () - 1)) {
			TurnAroundAndStartWalkingBack ();
		} else {
			ProceedToNextPickUp ();
		}
	}

	public override IEnumerator DropOffAndTurnBack (double amount, float currentLoadTime) {
		yield return StartCoroutine (base.DropOffAndTurnBack (amount, currentLoadTime));
		currentTargetShaft = -1;
		NextWillPickUp = true;
		ProceedToNextPickUp ();
	}

	private void SetElevatorPickUpsAndDropOff(List<GameObject> mineShafts) {

		dropOffLocation = elevatorDropOff;
		myDropOffBucketScript = GameObject.FindGameObjectWithTag ("ElevatorDeposit").GetComponent<BucketBehavior>();

		foreach (GameObject mineShaft in mineShafts)
		{
			AddNewMineShaftForPickUp (mineShaft);
		}
	}

	public void AddNewMineShaftForPickUp (GameObject mineShaft)
	{
		elevatorPickUpLocations.Add (new Vector3 (gameObject.transform.position.x, mineShaft.transform.position.y-.35f, 0.0f));
		elevatorPickUpBucketScripts.Add (mineShaft.transform.Find("MineDeposit").GetComponent<BucketBehavior>());
	}
		
	void ProceedToNextPickUp() {
		setCarryAmountToDisplay ();
		currentTargetShaft++;
		myPickUpBucketScript = elevatorPickUpBucketScripts [currentTargetShaft];
		Move (elevatorPickUpLocations [currentTargetShaft]);
	}

	private List <GameObject> GetMineShaftsInDescendingVerticalOrder (List <GameObject> mineShafts) {
		return mineShafts = new List<GameObject>(GameObject.FindGameObjectsWithTag("MineShaft").OrderBy((c) => -c.transform.position.y));
	}
}
