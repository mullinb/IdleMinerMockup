using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject mineShaftPrefab;
	public ElevatorBehavior elevatorBehavior;
	public PlayerBankBehavior playerBankBehavior;
	public Button newShaftButton;
	private Text newShaftButtonText;
	public double AmountOfCashOnHand;

	private Vector3 firstMineShaftLocation = new Vector3 (1f, -3f, 0f);

	private Vector3 nextMineShaftLocation;
	private float mineShaftDepthIncrement = 3;

	public int mineShaftCount = 0;
	public int depthBarrierCount = 0;
	public int maxNumberOfMineShafts = 20;

	public double newShaftCost = 100;
	public double barrierCost = 50000;

	void Awake () {
		
		nextMineShaftLocation = firstMineShaftLocation;
		elevatorBehavior = GameObject.FindGameObjectWithTag ("Elevator").GetComponent<ElevatorBehavior> ();
		playerBankBehavior = GameObject.FindGameObjectWithTag ("PlayerBank").GetComponent<PlayerBankBehavior> ();

		newShaftButtonText = newShaftButton.GetComponentInChildren<Text> ();
		newShaftButton.onClick.AddListener(AttemptToBuyNewShaft);
		InstantiateNewMine ();
	}

	private void AttemptToBuyNewShaft () {
		if (AmountOfCashOnHand >= newShaftCost) {
			playerBankBehavior.UpdateBucketResources(-newShaftCost);
			newShaftCost *= 4;
			InstantiateNewMine ();
		}
	}
		
	public void InstantiateNewMine () {
		GameObject newMineShaft = GameObject.Instantiate (mineShaftPrefab, nextMineShaftLocation, Quaternion.identity);
		elevatorBehavior.AddNewMineShaftForPickUp (newMineShaft);
		AfterMineInstantiationSetUp ();
	}

	private void AfterMineInstantiationSetUp () {
		mineShaftCount++;
		if (mineShaftCount >= maxNumberOfMineShafts) {
			print ("hit max");
			newShaftButton.gameObject.SetActive(false);
			return;
		} 
		SetNextMineShaftLocation ();
		ShowBarrierOrNewShaftButton ();
	}

	private void ShowBarrierOrNewShaftButton () {
		if (mineShaftCount % 5 == 0) {
			SetUpDepthBarrier ();
		} else {
			UpdateNewShaftButtonAndTranslate ();
		}
	}
		
	private void UpdateNewShaftButtonAndTranslate () {
		UpdateNewShaftButton ();
		newShaftButton.transform.Translate(0, -mineShaftDepthIncrement, 0);
	}

	private void UpdateNewShaftButton () {
		newShaftButton.onClick.RemoveAllListeners();
		newShaftButton.onClick.AddListener(AttemptToBuyNewShaft);
		newShaftButtonText.text = "NEW MINESHAFT? $" + newShaftCost.ToString();
	}

	private void SetUpDepthBarrier () {
		newShaftButton.onClick.RemoveAllListeners();
		newShaftButton.onClick.AddListener(AttemptToBypassBarrier);
		newShaftButtonText.text = "UNLOCK BARRIER? $" + barrierCost.ToString();
		newShaftButton.transform.Translate(0, -mineShaftDepthIncrement, 0);
	}

	private void AttemptToBypassBarrier () {
		if (AmountOfCashOnHand >= barrierCost) {
			playerBankBehavior.UpdateBucketResources(-barrierCost);
			RemoveDepthBarrierAndResetNewShaftButton ();
		}
	}

	private void RemoveDepthBarrierAndResetNewShaftButton () {
		depthBarrierCount++;
		barrierCost = Mathf.Pow ((float)barrierCost, 2f);
		UpdateNewShaftButton ();
	}

	private void SetNextMineShaftLocation () {
		nextMineShaftLocation = new Vector3 (nextMineShaftLocation.x, (nextMineShaftLocation.y - mineShaftDepthIncrement), nextMineShaftLocation.z);
	}
}
