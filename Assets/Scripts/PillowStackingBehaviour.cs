using UnityEngine;
using System.Collections;

public class PillowStackingBehaviour : MonoBehaviour {

	public GameObject parentPillow;
	public bool stacked;
	public bool stackable;

	private PillowBehaviour pillowBehave;

	void Start()
	{
		pillowBehave = parentPillow.GetComponent<PillowBehaviour>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.name + "Entered");
		PillowStackingBehaviour otherPillow = other.GetComponent<PillowStackingBehaviour>();

		if (!stacked && stackable && !otherPillow.stacked && otherPillow.stackable)
		{
			pillowBehave.TurnOnCollider();
			pillowBehave.GroundBehaviour();
			stacked = true;
			stackable = true;
			Debug.Log ("Stacked");
		}
			
	}

	void OnTriggerStay2D(Collider2D other)
	{
		PillowStackingBehaviour otherPillow = other.GetComponent<PillowStackingBehaviour>();
		
		if (!stacked && stackable && !otherPillow.stacked && otherPillow.stackable)
		{
			pillowBehave.TurnOnCollider();
			pillowBehave.GroundBehaviour();
			stacked = true;
			stackable = true;
			Debug.Log ("Stacked");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log (other.name + "Exited");
		PillowStackingBehaviour otherPillow = other.GetComponent<PillowStackingBehaviour>();

		if (stacked && stackable && !otherPillow.stacked && otherPillow.stackable)
		{
			pillowBehave.TurnOffCollider ();
			pillowBehave.GroundBehaviour ();
			stacked = false;
			Debug.Log ("Unstacked");
		}
	}
	
}
