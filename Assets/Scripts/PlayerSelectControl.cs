using UnityEngine;
using System.Collections;

public class PlayerSelectControl : MonoBehaviour {
	
	public string playerNumber;
	public GameObject playerFace;
	public GameObject playerHand;
	public GameObject playerHat;

	public GameObject player;
	public GameObject realPlayerFace;
	public GameObject realPlayerHand;
	public GameObject realPlayerHat;

	public Sprite[] faces;
	public Sprite[] hands;
	public Sprite[] hats;

	private int faceIndex;
	private int hatIndex;

	private bool playerSelect = false;
	private bool appearanceSelect = false;
	private bool hatSelect = false;

	private SpriteRenderer zzzSpriteRenderer;
	private SpriteRenderer faceSpriteRenderer;
	private SpriteRenderer handSpriteRenderer;
	private SpriteRenderer hatSpriteRenderer;


	void Awake ()
	{
		zzzSpriteRenderer = GetComponent<SpriteRenderer>();
		faceSpriteRenderer = playerFace.GetComponent<SpriteRenderer>();
		handSpriteRenderer = playerHand.GetComponent<SpriteRenderer>();
		hatSpriteRenderer = playerHat.GetComponent<SpriteRenderer>();

	}

	void Start ()
	{
		faceSpriteRenderer.sprite = faces[0];
		handSpriteRenderer.sprite = hands[0];
		hatSpriteRenderer.sprite = hats[0];

		faceIndex = 0;
		hatIndex = 0;
	}

	void Update () 
	{
		if (!playerSelect)
			SelectPlayer();
		else if (!appearanceSelect)
			SelectAppearance();
		else if (!hatSelect)
			SelectHat();
		else
			PlayerFinished();
	}

	void SelectPlayer()
	{
		if (Input.GetButtonUp(playerNumber + "Pickup"))
		{
			zzzSpriteRenderer.enabled = false;
			playerFace.SetActive (true);
			playerHand.SetActive (true);
			playerSelect = true;
		}
		else if (Input.GetButtonUp (playerNumber + "Other"))
		{
			Application.LoadLevel("Opener");
		}
	}

	void SelectAppearance()
	{
		if (Input.GetButtonUp(playerNumber + "LButton"))
		{
			//cycle face and hand sprites left
			faceIndex -= 1;
			if (faceIndex < 0)
				faceIndex = faces.Length - 1;
			faceSpriteRenderer.sprite = faces[faceIndex];
			handSpriteRenderer.sprite = hands[faceIndex];
		}
		else if (Input.GetButtonUp (playerNumber + "RButton"))
		{
			//cycle face and hand sprites right
			faceIndex += 1;
			if (faceIndex >= faces.Length)
				faceIndex = 0;
			faceSpriteRenderer.sprite = faces[faceIndex];
			handSpriteRenderer.sprite = hands[faceIndex];
		}
		else if (Input.GetButtonUp (playerNumber + "Pickup"))
		{
			playerHat.SetActive(true);
			appearanceSelect = true;
		}
		else if (Input.GetButtonUp (playerNumber + "Other"))
		{
			zzzSpriteRenderer.enabled = true;
			playerFace.SetActive (false);
			playerHand.SetActive (false);
			playerSelect = false;
		}
	}

	void SelectHat()
	{
		if (Input.GetButtonUp(playerNumber + "LButton"))
		{
			//cycle hat sprite left
			hatIndex -= 1;
			if (hatIndex < 0)
				hatIndex = hats.Length - 1;
			hatSpriteRenderer.sprite = hats[hatIndex];
		}
		else if (Input.GetButtonUp (playerNumber + "RButton"))
		{
			//cycle hat sprite right
			hatIndex += 1;
			if (hatIndex >= hats.Length)
				hatIndex = 0;
			hatSpriteRenderer.sprite = hats[hatIndex];
		}
		else if (Input.GetButtonUp (playerNumber + "Pickup"))
		{
			playerFace.SetActive(false);
			playerHand.SetActive(false);
			playerHat.SetActive(false);
			
			SetPlayer();
			hatSelect = true;
		}
		else if (Input.GetButtonUp (playerNumber + "Other"))
		{
			playerHat.SetActive (false);
			appearanceSelect = false;
		}
	}

	void PlayerFinished()
	{
		if (Input.GetButtonUp (playerNumber + "Other"))
		{
			UnsetPlayer();
			zzzSpriteRenderer.enabled = true;
			playerSelect = false;
			appearanceSelect = false;
			hatSelect = false;
		}
	}

	void SetPlayer()
	{
		//set face, hand, hat sprite
		realPlayerFace.GetComponent<SpriteRenderer>().sprite = faceSpriteRenderer.sprite;
		realPlayerHand.GetComponent<SpriteRenderer>().sprite = handSpriteRenderer.sprite;
		realPlayerHat.GetComponent<SpriteRenderer>().sprite = hatSpriteRenderer.sprite;
		player.SetActive(true);
		GameControl.control.players.Add(player);

	}

	void UnsetPlayer()
	{
		player.SetActive (false);
		GameControl.control.players.Remove(player);
	}
}
