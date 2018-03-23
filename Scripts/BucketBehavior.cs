using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BucketBehavior : MonoBehaviour {

	public double currentBucketAmount;
	public Text AmountDisplay;

	void Start () {
		currentBucketAmount = 500;
		AmountDisplay.text = currentBucketAmount.ToString ("C0");
	}

	public void InitiatePickUpResources (GameObject currentWorker) {

		WorkerBehavior workerBehavior = currentWorker.GetComponent<WorkerBehavior> (); 

		if (currentWorker.tag == "Miner") {
			workerBehavior.TransferAmount (workerBehavior.carryCapacity);
		} else {
			double workerCurrentCarryCapacity = workerBehavior.carryCapacity - workerBehavior.currentAmountCarried;
			double pickUpAmount = DeterminePickUpAmount (workerCurrentCarryCapacity, currentBucketAmount);
			workerBehavior.TransferAmount (pickUpAmount);
		}
	}

	public void InitiateDropOffResources (GameObject currentWorker) {

		WorkerBehavior workerBehavior = currentWorker.GetComponent<WorkerBehavior> (); 
		double dropOffAmount = workerBehavior.currentAmountCarried;

		if (currentWorker.tag == "Miner") {
			UpdateBucketResources (dropOffAmount);
			workerBehavior.currentAmountCarried = 0;
			workerBehavior.TurnAroundAndStartWalkingBack ();
		} else {
			workerBehavior.TransferAmount (dropOffAmount);
		}
	}

	public void UpdateBucketResources (double amount) {
		currentBucketAmount += amount;
		AmountDisplay.text = currentBucketAmount.ToString ("C0");
	}

	public static double DeterminePickUpAmount (double currentCarryCapacity, double currentBucketAmount) {
		return currentCarryCapacity <= currentBucketAmount ? currentCarryCapacity : currentBucketAmount;
	}
}

