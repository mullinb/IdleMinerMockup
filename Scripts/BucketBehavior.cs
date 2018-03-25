using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BucketBehavior : MonoBehaviour {

	public double currentBucketAmount = 0;
	public Text AmountDisplay;

	void Start () {
		AmountDisplay.text = currentBucketAmount.ToString ("C0");
	}

	public double AskBucketHowMuchToPickUp (GameObject currentWorker) {
		WorkerBehavior workerBehavior = currentWorker.GetComponent<WorkerBehavior> (); 
		double workerCurrentCarryCapacity = workerBehavior.carryCapacity - workerBehavior.currentAmountCarried;
		return DeterminePickUpAmount (workerCurrentCarryCapacity, currentBucketAmount);
	}

	virtual public void UpdateBucketResources (double amount) {
		currentBucketAmount += amount;
		AmountDisplay.text = currentBucketAmount.ToString ("C0");
	}

	private static double DeterminePickUpAmount (double currentCarryCapacity, double currentBucketAmount) {
		return currentCarryCapacity <= currentBucketAmount ? currentCarryCapacity : currentBucketAmount;
	}
}

