using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Data File/Level Catalog")]
public class LevelCatalog : ScriptableObject
{

	//not public so we can't accidentally access/reset it, so a serialize field is used to access it from the inspector.
	[SerializeField]
    LevelData[] levels;

    public int Length { get { return levels.Length;}}

    //purpose: level lookup
    public LevelData GetLevel (int index){
    	if (index >= levels.Length || index < 0){
    		return null;
    	}
    	//return the level of the coresponding index.
    	return levels[index];
    }

    public int GetIndexOf (LevelData data){
    	//will return -1 if data isnt in catalog
    	return System.Array.IndexOf (levels, data);
    }

	//get the name of the level for display, unnecessary
    public string GetLevelTitle (int index){
    	return levels[index].levelTitle;
    }

    public Color GetLevelColor(int index){
    	return levels[index].levelColor;
    }

}
