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

	protected float carryCapacityMultiplierPerLevel;
	protected float loadSpeedMultiplierPerLevel;
	protected float moveSpeedMultiplierPerLevel;

	protected float currentTotalCarryCapacityMultiplier = 1f;
	protected float currentTotalLoadSpeedMultiplier = 1f;
	protected float currentTotalMoveSpeedMultiplier = 1f;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		playerBankBehavior = GameObject.FindGameObjectWithTag ("PlayerBank").GetComponent<PlayerBankBehavior> ();

		levelUpText.text = "Level " + currentLevel.ToString () + "\nLevel Up? " + levelUpCost.ToString ("C0");
		levelUpButton.onClick.AddListener(TryToLevelUp);
	}
	
	void TryToLevelUp () {
		if (gameManager.AmountOfCashOnHand >= levelUpCost) {
			DeductLevelUpCostAndIncrementCurrentLevel ();
			ExecuteLevelUp (currentLevel);
			UpdateLevelCostAndDisplay ();
		}
	}
		
	private void DeductLevelUpCostAndIncrementCurrentLevel () {
		playerBankBehavior.UpdateBucketResources(-levelUpCost);
		currentLevel++;
	}

	protected virtual void ExecuteLevelUp (int currentLevel) {
	}
		
	private void UpdateLevelCostAndDisplay () {
		levelUpCost *= 1.5f;
		levelUpText.text = "Level " + currentLevel.ToString () + "\nLevel Up? " + levelUpCost.ToString ("C0");
	}
}
