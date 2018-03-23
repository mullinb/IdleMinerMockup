using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BucketBehavior : MonoBehaviour {

	public double currentBucketAmount;

	void Start () {
		currentBucketAmount = 500;
	}

	public void PickUpResources (GameObject currentWorker) {

		WorkerBehavior workerBehavior = currentWorker.GetComponent<WorkerBehavior> (); 

		if (currentWorker.tag == "Miner") {
			workerBehavior.currentAmountCarried = workerBehavior.carryCapacity;
			workerBehavior.WaitToLoadThenTurnAround (workerBehavior.carryCapacity);
		}
		else 
		{
			double workerCurrentCarryCapacity = workerBehavior.carryCapacity - workerBehavior.currentAmountCarried;

			double pickUpAmount = DeterminePickUpAmount (workerCurrentCarryCapacity, currentBucketAmount);

			workerBehavior.currentAmountCarried = pickUpAmount;
			currentBucketAmount -= pickUpAmount;

			workerBehavior.WaitToLoadThenTurnAround (pickUpAmount);

		}

	}

	public void DropOffResources (GameObject currentWorker) {

		WorkerBehavior workerBehavior = currentWorker.GetComponent<WorkerBehavior> (); 

		double dropOffAmount = workerBehavior.currentAmountCarried;

		currentBucketAmount += workerBehavior.currentAmountCarried;
		workerBehavior.currentAmountCarried = 0;

		workerBehavior.WaitToDropOffThenTurnAround (dropOffAmount);
	}

	public static double DeterminePickUpAmount (double currentCarryCapacity, double currentBucketAmount)
	{
		return currentCarryCapacity <= currentBucketAmount ? currentCarryCapacity : currentBucketAmount;
	}

}

