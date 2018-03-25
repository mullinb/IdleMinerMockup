using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelingBehavior : MonoBehaviour {

	public GameManager gameManager;
	public PlayerBankBehavior playerBankBehavior;

	public Button levelUpButton;
	public Text levelUpText;

	public int currentLevel = 1;
	public double levelUpCost = 100;

	public float currentTotalCarryCapacityMultiplier = 1;
	public float currentTotalLoadSpeedMultiplier = 1;
	public float currentTotalMoveSpeedMultiplier = 1;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		playerBankBehavior = GameObject.FindGameObjectWithTag ("PlayerBank").GetComponent<PlayerBankBehavior> ();

		levelUpText.text = "Level " + currentLevel.ToString () + "\nLevel Up? " + levelUpCost.ToString ("C0");
		levelUpButton.onClick.AddListener(TryToLevelUp);
	}
	
	void TryToLevelUp () {
		if (gameManager.AmountOfCashOnHand >= levelUpCost) {
			playerBankBehavior.UpdateBucketResources(-levelUpCost);
			currentLevel++;
			TriggerLevelUp (currentLevel);
		}
	}
		
	protected virtual void TriggerLevelUp (int currentLevel) {
		levelUpCost *= 1.5f;
		levelUpText.text = "Level " + currentLevel.ToString () + "\nLevel Up? " + levelUpCost.ToString ("C0");
	}
}
