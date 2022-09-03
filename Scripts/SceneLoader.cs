using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keeps track of the scenes we have, putting them into an enum so we can access them and know we're getting the right scene.
//
public static class SceneLoader
{
	//METHOD: Load scene based on enum
	public static void LoadScene(SceneName name){
		//enum entry gets cast to an int. Unity loads the scene of this int.
		UnityEngine.SceneManagement.SceneManager.LoadScene((int)name);
	}
}
//enum is outside the class so we can access it from anywhere
//we're going to have to update this as we add scenes
//in order for this to continue working properly, you have to update this in tandem with the build order in File/Build Settings
public enum SceneName {
	MenuMain,
	Level,
	LevelEnd,
	MenuLevelSelect,
	MenuSettings
}