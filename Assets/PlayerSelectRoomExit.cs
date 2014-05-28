using UnityEngine;
using System.Collections;

public class PlayerSelectRoomExit : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		if (Input.GetButtonUp("1Pickup") || Input.GetButtonUp("2Pickup") || Input.GetButtonUp("3Pickup") || Input.GetButtonUp("4Pickup"))
			Application.LoadLevel("RoomParents");
	}
}
