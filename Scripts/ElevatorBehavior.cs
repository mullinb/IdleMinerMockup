using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElevatorBehavior : WorkerBehavior {

	public List <GameObject> mineShafts;
	public List <Vector3> elevatorPickUpLocations;
	public List <BucketBehavior> elevatorPickUpBucketScripts;

	public int currentTargetShaft = -1;

	private Vector2 elevatorDropOff = new Vector2 (-5.35f, -1.77f);

	protected override void Awake () {
	}

	protected void Start () {
		rb2D = GetComponent <Rigidbody2D> ();
		moveSpeed = 2;

		loadSpeedPerSecond = 100;
		carryCapacity = 200;

		SetElevatorDropOff ();
		ProceedToNextPickUp ();
	}

	private void ProceedToNextPickUp() {
		SetCarryAmountToDisplay ();
		currentTargetShaft++;
		myPickUpBucketScript = elevatorPickUpBucketScripts [currentTargetShaft];
		Move (elevatorPickUpLocations [currentTargetShaft]);
	}
		
	protected override IEnumerator PickingUp (double amount, float currentLoadTime) {
		yield return StartCoroutine (WaitForLoad(currentLoadTime));
		TransportAndElevatorPickUp (amount);
		if (currentAmountCarried == carryCapacity || currentTargetShaft == (elevatorPickUpLocations.Count () - 1)) {
			TurnAroundAndStartWalkingBack ();
		} else {
			ProceedToNextPickUp ();
		}
	}

	protected override IEnumerator DroppingOff (double amount, float currentLoadTime) {
		yield return StartCoroutine (base.DroppingOff (amount, currentLoadTime));
		currentTargetShaft = -1;
		NextWillPickUp = true;
		ProceedToNextPickUp ();
	}
		

	protected override void SetCarryAmountToDisplay () {
		amountDisplay.text = currentAmountCarried.ToString ("C0");
	}
		
	public void AddNewMineShaftForPickUp (GameObject mineShaft)
	{
		elevatorPickUpLocations.Add (new Vector3 (gameObject.transform.position.x, mineShaft.transform.position.y-.35f, 0.0f));
		elevatorPickUpBucketScripts.Add (mineShaft.transform.Find("MineDeposit").GetComponent<BucketBehavior>());
	}

	private void SetElevatorDropOff() {
		dropOffLocation = elevatorDropOff;
		myDropOffBucketScript = GameObject.FindGameObjectWithTag ("ElevatorDeposit").GetComponent<BucketBehavior>();
	}
			


	// Following functions would be useful if restarting existing game state with multiple mineshafts

	public void SetElevatorPickUpsAndDropOff() {  
		mineShafts = GetMineShaftsInDescendingVerticalOrder (mineShafts);
		foreach (GameObject mineShaft in mineShafts) {
			AddNewMineShaftForPickUp (mineShaft);
		}
	}

	private List <GameObject> GetMineShaftsInDescendingVerticalOrder (List <GameObject> mineShafts) {
		return mineShafts = new List<GameObject>(GameObject.FindGameObjectsWithTag("MineShaft").OrderBy((c) => -c.transform.position.y));
	}
}
