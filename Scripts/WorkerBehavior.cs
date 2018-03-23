using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehavior : MonoBehaviour {

	public Vector3 pickUpLocation;
	public Vector3 dropOffLocation;
	public float loadTime = 3f;
	public float moveTime = .1f;
	public double carryCapacity = 100;
	public double currentAmountCarried = 0;
	BucketBehavior myPickUpBucketScript;
	BucketBehavior myDropOffBucketScript;
	public GameObject Display;


	private Rigidbody2D rb2D;
	private float inverseMoveTime;
	private float inverseLoadTime;
	private bool NextWillPickUp = true;

	void Awake () {
		rb2D = GetComponent <Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;

		setPickUpAndDropOff (gameObject);
		Move (pickUpLocation);
	}

	protected void Move (Vector2 destination)
	{
		StartCoroutine (SmoothMovement (destination));
	}

	private void CallPickUpOrDropOff ()
	{
		if (NextWillPickUp)
		{
			myPickUpBucketScript.PickUpResources (gameObject);
		}
		else
		{
			myDropOffBucketScript.DropOffResources (gameObject);
		}
	}

	public void WaitToLoadThenTurnAround(double pickUpAmount)
	{
		if (pickUpAmount > 0) 
		{
			Invoke ("TurnAroundAndStartWalkingBack", loadTime);
		} else 
		{
			TurnAroundAndStartWalkingBack ();
		}
	}

	public void WaitToDropOffThenTurnAround(double dropOffAmount)
	{
		if (dropOffAmount > 0) 
		{
			Invoke ("TurnAroundAndStartWalkingBack", loadTime);
		} else 
		{
			TurnAroundAndStartWalkingBack ();
		}
	}

	private void TurnAroundAndStartWalkingBack() 
	{
		print ("inside2");
		NextWillPickUp = !NextWillPickUp;

		Vector2 nextDestination = NextWillPickUp ? pickUpLocation : dropOffLocation;

		gameObject.GetComponent<SpriteRenderer> ().flipX = !gameObject.GetComponent<SpriteRenderer> ().flipX;


		Move (nextDestination);

	}

	protected IEnumerator SmoothMovement (Vector3 destination)
	{
		float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, destination, inverseMoveTime * Time.deltaTime);

			rb2D.MovePosition (newPosition);

			sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

			yield return null;
		}
		CallPickUpOrDropOff ();
	}

	private void setPickUpAndDropOff(GameObject currentObject)
	{
		if (gameObject.tag == "Transporter") {
			pickUpLocation = new Vector2 (-3.7f, 0.76f);
			dropOffLocation = new Vector2 (4.17f, 0.76f);
			myPickUpBucketScript = GameObject.FindGameObjectWithTag ("ElevatorDeposit").GetComponent<BucketBehavior>();
			myDropOffBucketScript = GameObject.FindGameObjectWithTag ("GameManagerDeposit").GetComponent<BucketBehavior>();
		} else if (gameObject.tag == "Miner") {
			pickUpLocation = new Vector2 ((transform.parent.localPosition.x - 2), transform.parent.localPosition.y);
			dropOffLocation = new Vector2 (transform.parent.localPosition.x + 1, transform.parent.localPosition.y);
			myPickUpBucketScript = transform.parent.Find("RawMaterials").GetComponent<BucketBehavior>();
			myDropOffBucketScript = transform.parent.Find("MineDeposit").GetComponent<BucketBehavior>();
		}
	}
}