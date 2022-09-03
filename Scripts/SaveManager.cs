using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

//static, so we can always access it no matter where we are in the game
public static class SaveManager
{
	static string path = "/Saves/";
	static string filename = "savedGame";
	static string ext = ".sav";


	//because this is a static class, we dont need to call an instance of SaveManager, we can just do SaveManager.Save();
	public static void SaveGameData(SaveData data){

		//Commented out: puts the FurthestLevel as "FurthestLevel" key in PlayerPrefs. A rudimentary way of saving a game.
		//PlayerPrefs.SetInt("FurthestLevel", PlaySessionManager.ins.FurthestLevel);

		BinaryFormatter bf = new BinaryFormatter(); //creates object that will do formatting for us.
		FileStream file = File.Create(Application.dataPath + path + filename + ext); 
		//Application.dataPath takes us to the Assets folder.
		//youll want this in a different location when you eventually build the game
		//you may also want to put this into a property, to prevent typos.

		//were serializing just the furthest level. you way want to save other data as well down the line.
		bf.Serialize(file, data);
		file.Close();
		Debug.Log("Game Saved.");
	}

	public static SaveData LoadGameData(){
		//Get the key and update the Furthest Level/
		//PlaySessionManager.ins.FurthestLevel = PlayerPrefs.GetInt("FurthestLevel");
		if (File.Exists(Application.dataPath + path + filename + ext)){
			//Load Data
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.dataPath + path + filename + ext, FileMode.Open);
			SaveData data =(SaveData) bf.Deserialize(file) as SaveData; 
			file.Close();
			return data;
		} else {
			//if no data has ever been saved, reset.
			return new SaveData(0, 0f, "bar"); //sets furthestlevel, cum, and foo to some default values
		}
	}

	//a debug function
	public static void ClearSaves(){
		//deletes everything in player prefs.
		PlayerPrefs.DeleteAll();
		Debug.Log("Saves Cleared!");
	}

	public static void SaveSetting(string label, float value){
		PlayerPrefs.SetFloat(label, value);
	}

	public static float LoadSetting(string label){
		return PlayerPrefs.GetFloat(label);
	}
}


[System.Serializable] //allows this class to be saved. incidentally if we had a public save data variable in one of our monobehavior classes, it would show up in the inspector.
public class SaveData
{
    public int furthestLevel;
    public float cum;
    public string foo;

    public SaveData (int furthestLevel, float cum, string foo)
    {
        //sets what is passed through the method as the varibles above. if the variables were called differently in the call, the "this." would be unnecessary
        this.furthestLevel = furthestLevel;
        this.cum = cum;
        this.foo = foo;
    }


}