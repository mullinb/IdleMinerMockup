using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketBehavior : MonoBehaviour {

	public float currentAmount;

	void Start () {
		currentAmount = 0;
		GetComponentInChildren<Text
	}

	public void PickUpResources (GameObject currentWorker) {
		if (currentWorker.tag == "Miner") 
		{
			currentWorker.currentAmountCarried = currentWorker.carryCapacity;
		} else
		{
			currentWorker.currentAmountCarried = DeterminePickUpAmount(currentWorker.carryCapacity, pickUpBucket.currentAmount);
			currentAmount -= DeterminePickUpAmount(carryCapacity, pickUpBucket.currentAmount);
		}
	}

	public void DropOffResources () {
		dropOffBucket.addAmountToBucket (currentAmountCarried);
		currentAmountCarried = 0;
	}

	public static int DeterminePickUpAmount (int carryCapacity, int currentBucketAmount)
	{
		float currentCarryCapacity = carryCapacity - currentAmountCarried;
		return currentCarryCapacity < currentBucketAmount ? currentCarryCapacity : currentBucketAmount;
	}

}

