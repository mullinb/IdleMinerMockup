using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterLevelingBehavior : LevelingBehavior {

	public GameObject transporterPrefab;
	public List<GameObject> allTransporters;


	void Awake () {
		carryCapacityMultiplierPerLevel = 1.3f;
		loadSpeedMultiplierPerLevel = 1.3f;

		InstantiateNewTransporter ();
	}

	protected override void ExecuteLevelUp (int currentLevel) {
		base.ExecuteLevelUp (currentLevel);
		if (currentLevel % 10 == 0) {
			InstantiateNewTransporter ();
		}
		IncreaseAllTransporterStats ();
		StoreTotalStatIncreases ();

	}

	private void IncreaseAllTransporterStats () {
		foreach (GameObject transporter in allTransporters) {
			TransporterBehavior transporterBehavior = transporter.GetComponent<TransporterBehavior> ();
			transporterBehavior.carryCapacity *= carryCapacityMultiplierPerLevel;
			transporterBehavior.loadSpeedPerSecond *= loadSpeedMultiplierPerLevel;
		}
	}

	private void StoreTotalStatIncreases () {
		currentTotalCarryCapacityMultiplier *= carryCapacityMultiplierPerLevel;
		currentTotalLoadSpeedMultiplier *= loadSpeedMultiplierPerLevel;
	}

	private void InstantiateNewTransporter () {
		GameObject newTransporter = Instantiate (transporterPrefab, new Vector3 (3.8f, 0.76f, 0.0f), Quaternion.identity);
		ApplyCurrentLevelStatsToNewTransporter (newTransporter);
		allTransporters.Add (newTransporter);
	}

	private void ApplyCurrentLevelStatsToNewTransporter (GameObject newTransporter) {
		TransporterBehavior newTransporterBehavior = newTransporter.GetComponent<TransporterBehavior> ();
		newTransporterBehavior.carryCapacity *= currentTotalCarryCapacityMultiplier;
		newTransporterBehavior.loadSpeedPerSecond *= currentTotalLoadSpeedMultiplier;
	}
}

