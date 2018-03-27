using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBehavior : WorkerBehavior {

	protected override void InitiatePickUp () {
		float currentLoadTime = (float)(carryCapacity / loadSpeedPerSecond);
		StartCoroutine(PickingUp (carryCapacity, currentLoadTime));
	}

	protected override IEnumerator PickingUp (double amount, float currentLoadTime) {
		yield return StartCoroutine (WaitForLoad(currentLoadTime));
		currentAmountCarried = carryCapacity;
		TurnAroundAndStartWalkingBack ();
	}

	protected override IEnumerator DroppingOff (double amount, float currentLoadTime) {
		currentAmountCarried = 0;
		myDropOffBucketScript.UpdateBucketResources (amount);
		TurnAroundAndStartWalkingBack ();
		yield return null;
	}

	protected override void SetTransportAndMinerPickUpAndDropOffLocations (GameObject currentObject) {
		gameObject.transform.position = new Vector3 (transform.parent.localPosition.x - 1.5f, transform.parent.localPosition.y, 0.0f);
		pickUpLocation = new Vector2 (transform.parent.localPosition.x + 1.7f, transform.parent.localPosition.y);
		dropOffLocation = new Vector2 (transform.parent.localPosition.x - 2, transform.parent.localPosition.y);
		myPickUpBucketScript = transform.parent.Find("RawMaterials").GetComponent<BucketBehavior>();
		myDropOffBucketScript = transform.parent.Find("MineDeposit").GetComponent<BucketBehavior>();	
	}
}