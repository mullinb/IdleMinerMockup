using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterLevelingBehavior : LevelingBehavior {

	public GameObject transporterPrefab;
	private GameObject[] transporters;

	protected override void TriggerLevelUp (int currentLevel) {
		base.TriggerLevelUp (currentLevel);
		if (currentLevel % 10 == 0) {
			InstantiateNewTransporter ();
		}
		transporters = GameObject.FindGameObjectsWithTag ("Transporter");
		foreach(GameObject transporter in transporters) {
			TransporterBehavior transporterBehavior = transporter.GetComponent<TransporterBehavior> ();
			transporterBehavior.carryCapacity *= 1.3f;
			transporterBehavior.loadSpeedPerSecond *= 1.3f;
			currentTotalCarryCapacityMultiplier *= 1.3f;
			currentTotalLoadSpeedMultiplier *= 1.3f;
		}
	}

	private void InstantiateNewTransporter() {
		GameObject newTransporter = Instantiate (transporterPrefab, new Vector3 (3.8f, 0.76f, 0.0f), Quaternion.identity);
		TransporterBehavior newTransporterBehavior = newTransporter.GetComponent<TransporterBehavior> ();
		newTransporterBehavior.carryCapacity *= currentTotalCarryCapacityMultiplier;
		newTransporterBehavior.loadSpeedPerSecond *= currentTotalLoadSpeedMultiplier;
	}
}
