using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBankBehavior : BucketBehavior {

	public GameManager gameManager;

	void Awake () {
		gameManager.AmountOfCashOnHand = currentBucketAmount;
		UpdateBucketResources (1000000000);
	}

	public override void UpdateBucketResources (double amount) {
		base.UpdateBucketResources (amount);
		gameManager.AmountOfCashOnHand = currentBucketAmount;
	}
}
