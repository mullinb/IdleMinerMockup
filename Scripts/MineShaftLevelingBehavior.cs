using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineShaftLevelingBehavior : LevelingBehavior {

	public GameObject minerPrefab;
	public List<GameObject> allMinersOnThisMine;

	public int thisMineLevel;

	private GameObject thisMine;

	void Awake () {
		thisMine = gameObject.transform.parent.gameObject;
		thisMineLevel = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ().mineShaftCount + 1;

		carryCapacityMultiplierPerLevel = 1.3f;
		loadSpeedMultiplierPerLevel = 1.3f;

		SetNewMineBaseLevel (thisMineLevel);

		InstantiateNewMiner ();
	}

	protected override void ExecuteLevelUp (int currentLevel) {
		base.ExecuteLevelUp (currentLevel);
		if (currentLevel % 10 == 0) {
			InstantiateNewMiner ();
		}
		IncreaseAllMinerStats ();
		StoreTotalStatIncreases ();
	}

	private void IncreaseAllMinerStats () {
		foreach(GameObject miner in allMinersOnThisMine) {
			MinerBehavior minerBehavior = miner.GetComponent<MinerBehavior> ();
			minerBehavior.carryCapacity *= carryCapacityMultiplierPerLevel;
			minerBehavior.loadSpeedPerSecond *= loadSpeedMultiplierPerLevel;
		}
	}

	private void StoreTotalStatIncreases () {
		currentTotalCarryCapacityMultiplier *= carryCapacityMultiplierPerLevel;
		currentTotalLoadSpeedMultiplier *= loadSpeedMultiplierPerLevel;
	}

	private void InstantiateNewMiner () {
		GameObject newMiner = Instantiate (minerPrefab, thisMine.transform);
		ApplyCurrentLevelStatsToNewMiner (newMiner);
		allMinersOnThisMine.Add (newMiner);
	}

	private void ApplyCurrentLevelStatsToNewMiner (GameObject newMiner) {
		MinerBehavior newMinerBehavior = newMiner.GetComponent<MinerBehavior> ();
		newMinerBehavior.carryCapacity *= currentTotalCarryCapacityMultiplier;
		newMinerBehavior.loadSpeedPerSecond *= currentTotalLoadSpeedMultiplier;
	}

	private void SetNewMineBaseLevel (int thisMineLevel) {
		currentTotalCarryCapacityMultiplier *= Mathf.Pow(thisMineLevel, 1.3f);
		currentTotalLoadSpeedMultiplier  *= Mathf.Pow(thisMineLevel, 1.3f);
		levelUpCost *= Mathf.Pow (thisMineLevel, 1.6f);
	}
}
