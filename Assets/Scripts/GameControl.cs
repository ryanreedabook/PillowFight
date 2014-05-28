using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public bool player1 = false;
	public bool player2 = false;
	public bool player3 = false;
	public bool player4 = false;

	public ArrayList players = new ArrayList{};

	void Awake () 
	{
		//Singleton Pattern - only one game control throughout a session
		if(control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this)
		{
			Destroy (gameObject);
		}
	}

	public void AddPlayer(GameObject player)
	{
		players.Add(player);
	}

	public void RemovePlayer(GameObject player)
	{
		players.Remove(player);
	}
	
}
