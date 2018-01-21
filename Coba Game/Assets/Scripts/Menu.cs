using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

	// Use this for initialization
	public void button_clickTicTacToe(){
		SceneManager.LoadScene (1);
	}
	public void button_clickPacMan(){
		SceneManager.LoadScene (2);
	}

	public void button_clickSnake(){
		SceneManager.LoadScene (6);
	}

	public void Exit(){
		Application.Quit ();
	}

}
