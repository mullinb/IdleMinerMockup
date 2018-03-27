using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterBehavior : WorkerBehavior {

	protected override IEnumerator PickingUp (double amount, float currentLoadTime) {
		yield return StartCoroutine (WaitForLoad(currentLoadTime));
		TransportAndElevatorPickUp (amount);
		TurnAroundAndStartWalkingBack ();
	}

	protected override IEnumerator DroppingOff (double amount, float currentLoadTime) {
		yield return StartCoroutine (base.DroppingOff (amount, currentLoadTime));
		TurnAroundAndStartWalkingBack ();
	}

	protected override void SetCarryAmountToDisplay () {
		if (currentAmountCarried == 0)
			amountDisplay.text = "";
		else
			amountDisplay.text = currentAmountCarried.ToString ("C0");
	}

	protected override void SetTransportAndMinerPickUpAndDropOffLocations (GameObject currentObject)
	{
		gameObject.transform.position = new Vector3 (3.8f, 0.76f, 0.0f);
		pickUpLocation = new Vector2 (-3.7f, 0.76f);
		dropOffLocation = new Vector2 (4.17f, 0.76f);
		myPickUpBucketScript = GameObject.FindGameObjectWithTag ("ElevatorDeposit").GetComponent<BucketBehavior>();
		myDropOffBucketScript = GameObject.FindGameObjectWithTag ("PlayerBank").GetComponent<BucketBehavior>();
	}
}
