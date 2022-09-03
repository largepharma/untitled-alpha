using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OutsiderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	LevelManager.OnLevelEnd += AnnounceGameOver;

    	//"Hey, any level managers out there, start your levels"
    	//because this just calls "level managers", all Levels will hear this
    	//This is the kind of thing you can only do in the Start method.
        //LevelManager.StartLevel ();
    }

    // Update is called once per frame
    void AnnounceGameOver(bool completed){
    	if(completed){
    		Debug.Log("Level Completed");
    	} else {
    		Debug.Log("Level Over");
    	}
    }

}