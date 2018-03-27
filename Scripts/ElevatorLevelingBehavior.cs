using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLevelingBehavior : LevelingBehavior {

	private ElevatorBehavior elevatorBehavior;

	private float carryCapacityMultiplierPerExtraLevelBoost = 1.6f;
	private float loadSpeedMultiplierPerExtraLevelBoost = 1.6f;
	private float moveSpeedMultiplierPerExtraBoost = 1.6f;

	void Awake () {
		elevatorBehavior = GameObject.FindGameObjectWithTag ("Elevator").GetComponent<ElevatorBehavior> ();

		carryCapacityMultiplierPerLevel = 1.3f;
		loadSpeedMultiplierPerLevel = 1.3f;
	}
		
	protected override void ExecuteLevelUp (int currentLevel) {
		base.ExecuteLevelUp (currentLevel);
		if (currentLevel % 10 == 0) {
			ExtraLevelBoost ();
		}
		IncreaseElevatorStats ();
	}

	private void IncreaseElevatorStats () {
		elevatorBehavior.carryCapacity *= carryCapacityMultiplierPerLevel;
		elevatorBehavior.loadSpeedPerSecond *= loadSpeedMultiplierPerLevel;	
	}
		

	private void ExtraLevelBoost () {
		elevatorBehavior.carryCapacity *= carryCapacityMultiplierPerExtraLevelBoost;
		elevatorBehavior.loadSpeedPerSecond *= loadSpeedMultiplierPerExtraLevelBoost;	
		elevatorBehavior.moveSpeed *= moveSpeedMultiplierPerExtraBoost;		
	}
}
