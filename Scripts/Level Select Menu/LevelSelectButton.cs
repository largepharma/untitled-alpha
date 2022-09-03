using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    LevelSelectUI ui;
    int levelIndex;

    public void Initialize(LevelSelectUI ui, int levelIndex, LevelData data){
    	this.ui = ui;
    	this.levelIndex = levelIndex;
    	//GetComponentInChildren<Text>().text = "Level " + levelIndex;
    	GetComponentInChildren<Text>().text = PlaySessionManager.ins.catalog.GetLevelTitle(levelIndex);
    	GetComponentInChildren<Image>().color = PlaySessionManager.ins.catalog.GetLevelColor(levelIndex);

    }
    public void OnPress(){
    	//has its own personal levelIndex
    	ui.LevelButtonPressed (levelIndex);
    }
}
