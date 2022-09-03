using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{

	public static LevelManager ins;

//OBJECTS
	public Player player;
	public Goal goal;
	public Transform obstacleFolder;
	public Transform obstaclePrefab;


//EVENTS
	//These let us tell the LevelManager what to do, or listen for the LevelManager.
	public delegate void LevelStartHandler(LevelData levelData);
	public static event LevelStartHandler OnLevelStart;

	//In case we're just quitting out of the game, we want to be able to say level end false.
	public delegate void LevelEndHandler(bool levelCompleted);
	public static event LevelEndHandler OnLevelEnd;

	//we could do a Start() function to start the level from this script and it will work fine
	//but we're building this as a program to interact with, not just a bit of code. (?) This makes sense.

	void Awake(){
		OnLevelStart += SetUpLevel;
		//Once LM hears that the goal was reached, it invokes EndLevel.
		Goal.OnGoalReached += EndLevel;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			//Take out the methods that should be destroyed along with the level when it ends.
			OnLevelStart -= SetUpLevel;
			Goal.OnGoalReached -= EndLevel;
			//this is kind of like how you'd make a pause function.
			SceneLoader.LoadScene (SceneName.MenuMain);
		}
	}

	//This method lets this class listen for a change in the game (OnLevelStart getting info) and respond.
	//Universal Call to Start Level (use after Awake)
	public static void StartLevel(LevelData levelData){
		if (OnLevelStart != null){
			OnLevelStart(levelData);
		}
	}

	//Positions player and goal, and instantiates obstacles
	void SetUpLevel(LevelData levelData){
		if (levelData == null)
			{return;}
		ClearObstacles();
		//the y pos is the same, so we're always standing on the ground properly.
		player.transform.position = new Vector3 (levelData.playerPosition.x, player.transform.position.y, levelData.playerPosition.z);
		goal.transform.position = new Vector3 (levelData.goalPosition.x, goal.transform.position.y, levelData.goalPosition.z);
		//iterate through the obstaclePositions array, 
		for (int i = 0; i < levelData.obstaclePositions.Length; i++){
			//and put an obstacle to every Transform.
			Transform newObstacle = Instantiate (obstaclePrefab) as Transform;
			//And put an obstacle at every Vector3. there are a couple of different ways to do this
			newObstacle.position = new Vector3(levelData.obstaclePositions[i].x, obstaclePrefab.position.y, levelData.obstaclePositions[i].z);
			newObstacle.parent = obstacleFolder;
		}
	}

	//in case we're setting up a level and there's already one there, we don't want them overlapping.
	void ClearObstacles(){
		foreach (Transform child in obstacleFolder){
			Destroy(child.gameObject);
		}
	}
	void EndLevel(){
		//Take out the methods that should be destroyed along with the level when it ends.
		OnLevelStart -= SetUpLevel;
		Goal.OnGoalReached -= EndLevel;
		// if OnLevelEnd is undefined is undefined, make it true. This results in a game end message in OutsiderScript.
		if (OnLevelEnd != null){
			OnLevelEnd (true);
		}
	}



}


