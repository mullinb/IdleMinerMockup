using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour {

	public Vector2 pickUpLocation;
	public Vector2 dropOffLocation;
	public float loadTime = 3f;
	public float moveTime = 5f;
	public float carryCapacity;
	public float currentAmountCarried;
	public GameObject myPickUp;
	public GameObject myDropOff;
	public GameObject[] elevatorPickUps;

	private Rigidbody2D rb2D;
	private float inverseMoveTime;
	private float inverseLoadTime;

	void Awake () {
		rb2D = GetComponent <Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;
		inverseLoadTime = 1f / loadTime;

		setPickUpAndDropOff (gameObject);
	}

	protected void Move (int xDir, int yDir)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		if (start == pickUpLocation) {
			BucketBehavior.PickUpResources (gameObject);
			return;
		} else if (start == dropOffLocation) 
		{
			BucketBehavior.DropOffResources (gameObject);
			return;
		}

		StartCoroutine (SmoothMovement (end));
	}

	protected IEnumerator SmoothMovement (Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);

			rb2D.MovePosition (newPosition);

			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		}
	}

	private void setPickUpAndDropOff(GameObject currentObject)
	{
		if (gameObject.tag == "Elevator") {
			pickUpLocation = new Vector2 (2f, 2f);
			dropOffLocation = new Vector2 (1f, 1f);
			elevatorPickUps = GameObject.FindGameObjectsWithTag ("MineDeposit");
			myDropOff = GameObject.FindGameObjectWithTag ("ElevatorDeposit");
		} else if (gameObject.tag == "Transporter") {
			pickUpLocation = new Vector2 (0f, 0f);
			dropOffLocation = new Vector2 (-1f, -1f);
			myPickUp = GameObject.FindGameObjectWithTag ("ElevatorDeposit");
			myDropOff = GameObject.FindGameObjectWithTag ("TransporterDeposit");
		} else if (gameObject.tag == "Miner") {
			pickUpLocation = new Vector2 ((transform.parent.localPosition.x - 2), transform.parent.localPosition.y);
			dropOffLocation = new Vector2 (transform.parent.localPosition.x + 1, transform.parent.localPosition.y);
			myPickUp = transform.parent.Find("RawMaterials");
			myDropOff = transform.parent.Find("MineDeposit");
		}
	}
}