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
}