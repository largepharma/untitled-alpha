using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
	public Text message;
	public Button restartButton;
	///The text on the restart button
	public Text rButtonLabel;

	void Start(){
		if (PlaySessionManager.ins.gameComplete){
			message.text = "You won the game!!";
			//disable the restart button.
			restartButton.gameObject.SetActive (false);
			//once you return to the menu, you don't want the game to be completed everytime you beat a single level
			PlaySessionManager.ins.gameComplete = false;
			//we dont want to suddenyl change the message we're giving the player once this is done, so stop reading.
			return;
		}
		bool success = PlaySessionManager.ins.mostRecentEndSuccess;
		//if the player succeeded, say the first string; if they failed, say the second string
		message.text = success ? "Level completed" : "Level failed";
		rButtonLabel.text = success ? "Next level" : "Try again";
	}

	public void RestartButtonPressed(){
 		//call the game scene
 		SceneLoader.LoadScene(SceneName.Level);
 	}
 	public void MenuButtonPressed(){
 		//call the game scene
 		SceneLoader.LoadScene(SceneName.MenuMain);
 	}
}
