using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//singleton instance
//follows around the user, telling what scenes and levels need to be loaded.
public class PlaySessionManager : MonoBehaviour
{
	//singleton instance
	public static PlaySessionManager ins;

	public LevelCatalog catalog;
	//to access: PlaySessionManager.ins.catalog
	
	int currentLevel = 0;
	//for say a non-linear game where you may jump ahead x # of levels,
	//it may be a good idea to have some booleans to determine the levels available in the level select
	int furthestLevel = 0; //redundancy, can be removed later.

    SaveData data; //this is where we store the save data as we play


	//stores the end state of the level into the singleton to make communication between classes easier.
	public bool mostRecentEndSuccess = false;
	public bool gameComplete = false;

	//The property version of current level, as signaled by capitalising the first letter of the camelCase variable
	//properties can be accessed in other classes, through this class's singleton instance (ins.<property>)
	public int CurrentLevel {
        set {currentLevel = value;}
    }
	public int FurthestLevel { 
		get {return furthestLevel;}
		set {furthestLevel = value;}
	}

	void Awake(){
		//Singleton Pattern: if nothing has been declared as this static instance...
		if (ins == null){
			//...then THIS is that instance...
			ins = this;
			//... and keep the gameObject I'm attached to
			DontDestroyOnLoad(gameObject);
		//if theres a static instance and this aint it
		} else if (ins != this){
			//destroy the gameObject I'm attached to
			Destroy (gameObject);
            return;
		}
        //Get player data
		data = SaveManager.LoadGameData();
        furthestLevel = data.furthestLevel;

        //Subscribe to level events
		SceneManager.sceneLoaded += OnSceneLoad;
		LevelManager.OnLevelEnd += HandleLevelEnd;
	}

	//Once we load any scene, then we can call some kind of code that we want in here
	void OnSceneLoad(Scene s, LoadSceneMode lsm){
		//if the scene is called level
		if (s.name == "level"){
			LevelManager.StartLevel (catalog.GetLevel(currentLevel));
			PersistentAudioPlayer.ins.ChangeMusic(1);
		}else{
			PersistentAudioPlayer.ins.ChangeMusic(0);
		}
	}

	void HandleLevelEnd (bool levelCompleted){
		//if the level was completed, then the most recent end will be successful.
		mostRecentEndSuccess = levelCompleted;
		if (levelCompleted){
			currentLevel++;
			//if you beat a level, you go to the next one. if you beat the last level, you complete the game and reset to zero.
			if (currentLevel >= catalog.Length){
				currentLevel = 0;
				gameComplete = true;
			}
			//update furthestLevel
			if (currentLevel > furthestLevel){
				furthestLevel = currentLevel;
                data.furthestLevel = furthestLevel;
				//save progress
				SaveManager.SaveGameData(data);
			}

		}
		SceneLoader.LoadScene(SceneName.LevelEnd);
	}



	void Update(){
		if(Input.GetKeyDown(KeyCode.F1)){
			SaveManager.ClearSaves();
		}
	}

}
