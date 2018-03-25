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
}
