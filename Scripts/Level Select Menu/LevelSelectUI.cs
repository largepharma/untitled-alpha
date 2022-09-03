using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{

	public LevelSelectButton buttonPrefab;
	public Transform buttonContainer;


	void Start() {
		//making a button for each level the player has reached
		for (int i = 0; i <= PlaySessionManager.ins.FurthestLevel; i++){
			LevelSelectButton newButton = Instantiate(buttonPrefab, buttonContainer);
			newButton.Initialize(this, i, PlaySessionManager.ins.catalog.GetLevel(i));
		}
	}


 	public void BackButtonPressed(){
 		//call the game scene
 		SceneLoader.LoadScene(SceneName.MenuMain);
 	}

 	public void LevelButtonPressed(int LevelIndex){
 		//set PlaySessionManager
 		PlaySessionManager.ins.CurrentLevel = LevelIndex;
 		//change the scene
 		SceneLoader.LoadScene(SceneName.Level);
 	}
}
