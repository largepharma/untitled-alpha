using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//we dont need to use UnityEngine.UI right now because the inspector is handling that communication.

public class MainMenuUI : MonoBehaviour
{
 	public void StartButtonPressed(){
 		//call the game scene
 		SceneLoader.LoadScene(SceneName.Level);
 	}
 	public void LevelSelectButtonPressed(){
 		//call the select scene
 		SceneLoader.LoadScene(SceneName.MenuLevelSelect);
 	}
 	public void SettingsButtonPressed(){
 		//call the menu scene
 		SceneLoader.LoadScene(SceneName.MenuSettings);
 	}
}
