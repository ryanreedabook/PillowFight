using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	void Start ()
	{
		GameControl.control.player1 = false;
		GameControl.control.player2 = false;
		GameControl.control.player3 = false;
		GameControl.control.player4 = false;
		GameControl.control.players.Clear();
	}

	void Update () 
	{
		HandlePlayerInput();
	}

	void HandlePlayerInput()
	{
		if (Input.GetButtonUp("1Pickup") || Input.GetButtonUp("2Pickup") || Input.GetButtonUp("3Pickup") || Input.GetButtonUp("4Pickup"))
			Application.LoadLevel ("PlayerBegin");
	}

}
