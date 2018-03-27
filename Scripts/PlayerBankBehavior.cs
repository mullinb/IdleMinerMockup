using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBankBehavior : BucketBehavior {

	public GameManager gameManager;

	void Awake () {
		gameManager.AmountOfCashOnHand = currentBucketAmount;
	}

	public override void UpdateBucketResources (double amount) {
		base.UpdateBucketResources (amount);
		gameManager.AmountOfCashOnHand = currentBucketAmount;
	}
}
