using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerBehavior : MonoBehaviour {

	public Vector3 pickUpLocation;
	public Vector3 dropOffLocation;
	public float moveSpeed;
	public double loadSpeedPerSecond;
	public double carryCapacity;
	public double currentAmountCarried = 0;
	public BucketBehavior myPickUpBucketScript;
	public BucketBehavior myDropOffBucketScript;
	public Text amountDisplay;
	public Slider slider;

	protected Rigidbody2D rb2D;

	protected bool NextWillPickUp = true;

	protected virtual void Awake () {
		rb2D = GetComponent <Rigidbody2D> ();
		moveSpeed = 5;

		loadSpeedPerSecond = 100;
		carryCapacity = 200;

		SetTransportAndMinerPickUpAndDropOffLocations(gameObject);
		Move (pickUpLocation);

		slider.gameObject.SetActive(false);
	}

	protected void Move (Vector2 destination) {
		StartCoroutine (SmoothMovement (destination));
	}

	protected virtual void InitiatePickUp () {
		double currentTransferAmount;
		currentTransferAmount = myPickUpBucketScript.AskBucketHowMuchToPickUp (gameObject);
		float currentLoadTime = (float)(currentTransferAmount / loadSpeedPerSecond);
		StartCoroutine (PickingUp (currentTransferAmount, currentLoadTime));
	}

	public void InitiateDropOff() {
		float currentLoadTime = (float)(currentAmountCarried / loadSpeedPerSecond);
		StartCoroutine (DroppingOff (currentAmountCarried, currentLoadTime));
	}

	protected virtual IEnumerator PickingUp (double amount, float currentLoadTime) {
		yield return null;
	}
		
	protected virtual IEnumerator DroppingOff (double amount, float currentLoadTime) {
		yield return StartCoroutine (WaitForLoad(currentLoadTime));
		myDropOffBucketScript.UpdateBucketResources (amount);
		currentAmountCarried = 0;
	}

	protected IEnumerator WaitForLoad (float currentLoadTime) {
		StartCoroutine (AnimateLoadingOrUnloading (currentLoadTime));
		yield return new WaitForSeconds (currentLoadTime);
	}

	protected void TransportAndElevatorPickUp (double amount) {
		if (amount <= myPickUpBucketScript.currentBucketAmount) {
			currentAmountCarried += amount;
			myPickUpBucketScript.UpdateBucketResources (-amount);
		} else { 
			currentAmountCarried = myPickUpBucketScript.currentBucketAmount;
			myPickUpBucketScript.UpdateBucketResources (-myPickUpBucketScript.currentBucketAmount);
		}
	}

	public void TurnAroundAndStartWalkingBack() {
		NextWillPickUp = !NextWillPickUp;
		Vector2 nextDestination = NextWillPickUp ? pickUpLocation : dropOffLocation;
		SetCarryAmountToDisplay ();
		Move (nextDestination);
		if (gameObject.tag != "Elevator")
			gameObject.GetComponent<SpriteRenderer> ().flipX = !gameObject.GetComponent<SpriteRenderer> ().flipX;
	}

	protected IEnumerator SmoothMovement (Vector3 destination) {
		float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
		while (sqrRemainingDistance > .01f) {
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, destination, moveSpeed * Time.deltaTime);
			rb2D.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
			yield return null;
		}
		if (NextWillPickUp) {
			InitiatePickUp ();
		} else
			InitiateDropOff ();
	}

	private IEnumerator AnimateLoadingOrUnloading(float loadTime) {	
		if (loadTime != 0)
			slider.gameObject.SetActive(true);
		float fillRatePerSecond = 1f / loadTime;
		slider.value = 0;
		while (slider.value < 1) {
			slider.value += fillRatePerSecond * Time.deltaTime;
			yield return null;
		}
		slider.value = 0;
		slider.gameObject.SetActive(false);
	}

	protected virtual void SetTransportAndMinerPickUpAndDropOffLocations(GameObject currentObject) {
	}

	protected virtual void SetCarryAmountToDisplay () {
		return;
	}
}