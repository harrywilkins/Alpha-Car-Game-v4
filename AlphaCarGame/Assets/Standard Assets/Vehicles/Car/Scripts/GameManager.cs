using UnityEngine;
using System.Collections;

[System.Serializable]
public class CarPackage {
	public string name;
	public GameObject car;
	public Camera cam;

	bool enabled = true;

	public void Disable() {
		if (enabled) {
			car.SetActive (false);
			cam.gameObject.SetActive (false);
			enabled = false;
		}
	}

	public void Enable() {
		if (!enabled) {
			car.SetActive (true);
			cam.gameObject.SetActive (true);
			enabled = true;
		}
	}

	public void AdjustCamera(int playerNum, int players) {
		if (playerNum == 1) {
			if (players == 1) {
				cam.rect = new Rect (0f, 0f, 1f, 1f);
			} else if (players == 2 || players == 3) {
				cam.rect = new Rect (0f, 0.5f, 1f, 0.5f);
			} else {
				cam.rect = new Rect (0f, 0.5f, 0.5f, 0.5f);
			}
		} else if (playerNum == 2) {
			if (players == 2) {
				cam.rect = new Rect (0f, 0f, 1f, 0.5f);
			} else if (players == 3) {
				cam.rect = new Rect (0f, 0f, 0.5f, 0.5f);
			} else {
				cam.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
			}
		} else if (playerNum == 3) {
			if (players == 3) {
				cam.rect = new Rect (0.5f, 0f, 0.5f, 0.5f);
			} else {
				cam.rect = new Rect (0f, 0f, 0.5f, 0.5f);
			}
		} else {
			cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
		}
	}
}

public class GameManager : MonoBehaviour {

	public CarPackage[] players;
	private int currentPlayers = 1;
	
	void Start() {
		SetPlayers (1);
	}
	
	void Update() {
		for(int p = 1; p < 5; p++){
			if(Input.GetKeyDown(p.ToString()){
				SetPlayers(p);
			}
		}
	}

	void SetPlayers(int numPlayers) {
		if (numPlayers != currentPlayers) {
			for (int i = 0; i < players.Length; i++) {
				if (i < numPlayers) {
					players [i].Enable ();
					players [i].AdjustCamera (i + 1, numPlayers);
				} else 	players [i].Disable ();
			}
			currentPlayers = numPlayers;
		}
	}
}


