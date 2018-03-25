using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineShaftLevelingBehavior : LevelingBehavior {

	public GameObject minerPrefab;
	public List<GameObject> miners;
	private GameObject thisMine;

	public int thisMineLevel;

	void Awake () {
		thisMine = gameObject.transform.parent.gameObject;
		thisMineLevel = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ().mineShaftCount + 1;

		currentTotalCarryCapacityMultiplier *= Mathf.Pow(thisMineLevel, 1.3f);
		currentTotalLoadSpeedMultiplier  *= Mathf.Pow(thisMineLevel, 1.3f);
		levelUpCost *= Mathf.Pow (thisMineLevel, 1.6f);

		InstantiateNewMiner ();

		// foreach is unnecessary but could be useful if you have multiple miners spawning on new mine
		foreach (Transform child in thisMine.transform) {
			if (child.CompareTag ("Miner")) {
				miners.Add (child.gameObject);
			}
		}
	}

	protected override void TriggerLevelUp (int currentLevel) {
		base.TriggerLevelUp (currentLevel);
		if (currentLevel % 10 == 0) {
			InstantiateNewMiner ();
		}

		foreach(GameObject miner in miners) {
			MinerBehavior minerBehavior = miner.GetComponent<MinerBehavior> ();
			minerBehavior.carryCapacity *= 1.3f;
			minerBehavior.loadSpeedPerSecond *= 1.3f;
			currentTotalCarryCapacityMultiplier *= 1.3f;
			currentTotalLoadSpeedMultiplier *= 1.3f;
		}
	}

	private void InstantiateNewMiner () {
		GameObject newMiner = Instantiate (minerPrefab, thisMine.transform);
		MinerBehavior newMinerBehavior = newMiner.GetComponent<MinerBehavior> ();
		newMinerBehavior.carryCapacity *= currentTotalCarryCapacityMultiplier;
		newMinerBehavior.loadSpeedPerSecond *= currentTotalLoadSpeedMultiplier;
		miners.Add (newMiner);
	}
}
