using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLevelingBehavior : LevelingBehavior {

	private ElevatorBehavior elevatorBehavior;

	void Awake () {
		elevatorBehavior = GameObject.FindGameObjectWithTag ("Elevator").GetComponent<ElevatorBehavior> ();
	}

	protected override void TriggerLevelUp (int currentLevel) {
		base.TriggerLevelUp (currentLevel);
		if (currentLevel % 10 == 0) {
			elevatorBehavior.carryCapacity *= 1.3f;
			elevatorBehavior.loadSpeedPerSecond *= 1.3f;	
			elevatorBehavior.moveSpeed *= 1.3f;		
		}
		elevatorBehavior.carryCapacity *= 1.3f;
		elevatorBehavior.loadSpeedPerSecond *= 1.3f;	
	}
}
