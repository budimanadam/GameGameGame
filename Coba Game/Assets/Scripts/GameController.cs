using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Player{
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]


public class PlayerColor{
	public Color panelColor;
	public Color textColor;
} 


public class GameController : MonoBehaviour {

	// Use this for initialization
	public Text[] buttonList;
	private string playerSide;

	public GameObject gameOverPanel;
	public Text gameOverText;

	private int moveCount;
	public GameObject restartButton;

	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;


	private string computerSide;
	public bool playerMove;
	public float delay;
	private int value;



	void Awake(){
	
		gameOverPanel.SetActive (false);
		SetGameControllerReferenceOnButtons ();
		moveCount = 0;
		restartButton.SetActive (false);
		playerMove = true;


	}


/*	void Update(){
		if (playerMove == false) {
			delay += delay * Time.deltaTime;
			if (delay > 100) {
				value = Random.Range (0, 8);
				if (buttonList [value].GetComponentInParent<Button> ().interactable == true) {
					buttonList [value].text = GetComputerSide;
					buttonList [value].GetComponentInParent<Button> ().interactable = false;
					EndTurn ();
				}
			}
		}

	}*/

	void SetGameControllerReferenceOnButtons(){



		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].GetComponentInParent<GridSpace> ().SetGameControllerReference (this);
		}
	}

	public void SetStartingSide(string startingSide){
		playerSide = startingSide;
		if(playerSide=="X"){
			computerSide = "O";
			SetPlayerColor (playerX, playerO);

		}else{
			computerSide = "X";
			SetPlayerColor (playerO, playerX);
		}

		StartGame ();
	}

	void StartGame(){
		SetBoardInteractable (true);
		SetPlayerButtons (false);
		startInfo.SetActive (false);

	}

	public string GetPlayerSide(){

		return playerSide;
	}

	public string GetComputerSide(){

		return computerSide;
	}

	public void EndTurn(){

		moveCount++;
		if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide) {
			GameOver (playerSide);

		} else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);

		}

		else if (moveCount >= 9) {
			//ini benar
			if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) {
				GameOver (playerSide);

			} else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
				GameOver (playerSide);

			} else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) {
				GameOver (playerSide);

			} else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide) {
				GameOver (playerSide);

			} else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide) {
				GameOver (playerSide);

			} else if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide) {
				GameOver (playerSide);

			} else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide) {
				GameOver (playerSide);
			} else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide) {
				GameOver (playerSide);

			}
			//ini salah

			else {
				GameOver ("draw");
			}
		} else {
			changeSide ();

		}
	}

	void SetPlayerColor(Player newPlayer, Player oldPlayer){
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;

	}

	void GameOver(string winningPlayer){
		SetBoardInteractable (false);
		//SetGameOverText(  playerSide + " WIN!");
		if (winningPlayer == "draw") {
			SetGameOverText ("It's a draw");
			SetPlayersColorsInactive ();

		} else {
			SetGameOverText(winningPlayer+ " Wins");
		}

		restartButton.SetActive (true);

	
	}

	void changeSide(){
		playerSide=(playerSide=="X")	? "O" : "X";
//		playerMove=(playerMove==true)	? false : true;

		if (playerSide == "X") {
	//	if (playerMove == true) {
			SetPlayerColor (playerX, playerO);

		} else {
			SetPlayerColor (playerO, playerX);
		}

	}

	void SetGameOverText(string value){
		gameOverPanel.SetActive (true);
		gameOverText.text = value;
	}
	public void restartGame(){

		moveCount = 0;
		gameOverPanel.SetActive (false);


		restartButton.SetActive (false);
		SetPlayerButtons (true);
		SetPlayersColorsInactive ();
		startInfo.SetActive (true);



		for (int i = 0; i < buttonList.Length; i++) {
			
			buttonList [i].text = "";
		}




	}


	void SetBoardInteractable(bool toggle){
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].GetComponentInParent<Button> ().interactable = toggle;

		}
	}

	void SetPlayerButtons(bool toggle){
		playerX.button.interactable = toggle;
		playerO.button.interactable = toggle;
	}
	void SetPlayersColorsInactive(){
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;


	}

}
