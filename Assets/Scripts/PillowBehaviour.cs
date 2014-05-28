using UnityEngine;
using System.Collections;

public class PillowBehaviour : MonoBehaviour 
{
	public bool randomAngle;
	public bool collidable;

	public float airMass;
	public float airDrag;
	public float airAngularDrag;
	public float groundMass;
	public float groundDrag;
	public float groundAngularDrag;
	public float heldMass;
	public float heldDrag;
	public float heldAngularDrag;

	public Color onColor;
	public Color offColor;

	public GameObject featherParticles;

	private SpriteRenderer renderSprite;
	private Rigidbody2D pillowBody;
	private PillowStackingBehaviour pillowStack;

	void Start()
	{
		renderSprite = gameObject.GetComponent<SpriteRenderer>();
		pillowBody = gameObject.GetComponent<Rigidbody2D>();
		pillowStack = gameObject.GetComponentInChildren<PillowStackingBehaviour>();

		TurnOffCollider();
		GroundBehaviour ();

		if (randomAngle)
		{
			float startRotation = Random.Range (0f,180f);
			transform.rotation = Quaternion.Euler (0,0,startRotation);
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (collidable && !pillowStack.stacked)
		{
			Vector2 location = other.contacts[0].point;
			GameObject obj = Instantiate(featherParticles, new Vector3(location.x, location.y, 0), other.transform.rotation) as GameObject;
			TurnOffCollider();
			GroundBehaviour();
		}
	}

	public void TurnOnCollider()
	{
		collider2D.isTrigger = false; //maybe set IsTrigger to false
		renderSprite.color = onColor;
		collidable = true;
		renderSprite.sortingLayerName = "ActivePillows";
		pillowStack.stackable = false;
	}

	public void TurnOffCollider()
	{
		collider2D.isTrigger = true; //maybe set IsTrigger to true
		renderSprite.color = offColor;
		collidable = false;
		renderSprite.sortingLayerName = "Pillows";
		pillowStack.stackable = true;
	}

	public void AirBehaviour()
	{
		pillowBody.mass = airMass;
		pillowBody.drag = airDrag;
		pillowBody.angularDrag = airAngularDrag;
	}

	public void GroundBehaviour()
	{
		pillowBody.mass = groundMass;
		pillowBody.drag = groundDrag;
		pillowBody.angularDrag = groundAngularDrag;
	}

}
