using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{

	public float speed = 5f;				// The fastest the player can travel in x and y axis
	public float speedDecreaseFactor = 0.8f;
	public float throwSpeed = 5f;
	public bool hasPillow = false;			// Whether player is currently holding a pillow
	public LayerMask grabbable;
	public Transform playerHand;
	public float pickupRadius = 1f;
	public GameObject bloodParticles;
	public float knockbackForce = 100f;

	//buttons
	public int playerNumber;

	private HingeJoint2D hinge;
	private GameObject pillow;
	private Vector2 facingDirection;

	private float maxSpeed;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		hinge = GetComponent<HingeJoint2D> ();
		hinge.enabled = false;
		hinge.anchor = new Vector2 (playerHand.localPosition.x, playerHand.localPosition.y);
	}
	
	
	void Update()
	{
		//Set the player's sprite facing direction

		if (rigidbody2D.velocity != Vector2.zero)
		{
			facingDirection = rigidbody2D.velocity;
			float targetAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0, 0, targetAngle);
		}

		//Pickup or drop pillow
		if (Input.GetButtonUp (playerNumber + "Pickup") && hasPillow == true)
		{
			PillowBehaviour pillowBehaviour = pillow.GetComponent<PillowBehaviour>();
			hinge.connectedBody = null;
			pillow.rigidbody2D.velocity = Vector2.zero;
			pillowBehaviour.TurnOffCollider();
			pillowBehaviour.GroundBehaviour ();
			pillow = null;
			hasPillow = false;
			hinge.enabled = false;
		}
		else if (Input.GetButtonUp(playerNumber + "Pickup") && hasPillow == false)
		{
			Collider2D[] pillowsAvailable = Physics2D.OverlapCircleAll (new Vector2 (playerHand.position.x, playerHand.position.y), pickupRadius, grabbable);
			if (pillowsAvailable.Length > 0)
			{
				pillow = pillowsAvailable[0].gameObject;
				PillowBehaviour pillowBehaviour = pillow.GetComponent<PillowBehaviour>();
				hinge.connectedBody = pillow.rigidbody2D;
				hasPillow = true;
				hinge.enabled = true;
				pillowBehaviour.TurnOnCollider();
				pillowBehaviour.AirBehaviour();
				pillowBehaviour.collidable = false;
			}
		}

		//Throw a pillow
		if (Input.GetButtonUp (playerNumber + "Throw") && hasPillow == true)
		{
			hinge.connectedBody = null;
			pillow.rigidbody2D.velocity =  facingDirection.normalized * throwSpeed;
			pillow.GetComponent<PillowBehaviour>().collidable = true;
			pillow = null;
			hasPillow = false;
			hinge.enabled = false;
		}
	}


	
	void FixedUpdate ()
	{
		if (hasPillow)
			maxSpeed = speed * speedDecreaseFactor;
		else
			maxSpeed = speed;

		//Set player's move direction and speed
		float h = Input.GetAxis(playerNumber + "Horizontal");
		float v = Input.GetAxis(playerNumber + "Vertical");
		rigidbody2D.AddForce(new Vector2 (h * maxSpeed, v * maxSpeed));
	}

	/*
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag != "BadPillow")
			return;

		Vector2 location = col.contacts[0].point;
		GameObject obj = Instantiate(bloodParticles, new Vector3(location.x, location.y, 0), col.transform.rotation) as GameObject;

		Vector2 forceDirection = new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y);
		rigidbody2D.AddForce(forceDirection.normalized * knockbackForce);
	}
	*/

}
