using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int mineShaftCount;
	public int depthBarrierCount;
	public GameObject[] Mineshafts;
	public GameObject HUD;
	public GameObject MineShaftPrefab;
	public GameObject TransporterPrefab;
	public ElevatorBehavior elevatorBehavior;
	public int maxNumberOfMineShafts;

	private Vector3 firstMineShaftLocation = new Vector3 (0f, -3f, 0f);

	private Vector3 nextMineShaftLocation;
	private float mineShaftDepthIncrement = 3;


	void Awake () {
		nextMineShaftLocation = firstMineShaftLocation;
		Object.Instantiate (MineShaftPrefab, nextMineShaftLocation, Quaternion.identity); 
	}

	public void InstantiateNewMine () {
		mineShaftCount++;
		GameObject newMineShaft = Object.Instantiate (MineShaftPrefab, nextMineShaftLocation, Quaternion.identity); 
		elevatorBehavior.AddNewMineShaftForPickUp (newMineShaft);

		if (mineShaftCount != maxNumberOfMineShafts) {
			ReassignNewShaftButton;
		}
	}

	void ReassignNewShaftButton () {
		if (mineShaftCount % 5 == 0) {
			// block advancement with barrier;
		} else {
			// move button downward, update displayed cost;
		}
	}
	

	private void SetNextMineShaftLocation () {
		nextMineShaftLocation = new Vector3 (nextMineShaftLocation.x, (nextMineShaftLocation.y - mineShaftDepthIncrement), nextMineShaftLocation.z);
	}
}
