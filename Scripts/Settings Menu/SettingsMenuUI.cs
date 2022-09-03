using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuUI : MonoBehaviour
{
	public Slider music, sfx;

	public AudioMixer mixer;

	//to make sure we're calling the same names everytime
	string musicPref = "Music";
	string sfxPref = "SFX";

	void Start(){
		//get one minus the value for musicPref, inverting the value.
		music.value = 1f - SaveManager.LoadSetting (musicPref);
		sfx.value = 1f - SaveManager.LoadSetting (sfxPref);
	}

	//attached to Save button
	public void SaveSettings(){
		SaveManager.SaveSetting(musicPref, 1f - music.value);
		SaveManager.SaveSetting(sfxPref, 1f - sfx.value);
		mixer.SetFloat("BGMVolume", SliderToMixerValue(music.value));
		mixer.SetFloat("SFXVolume", SliderToMixerValue(sfx.value));
	}

	//attached to Cancel button
	public void ReturnToMainMenu(){
		SceneLoader.LoadScene(SceneName.MenuMain);
	}
	//private method to convert inverted volume values into mixer values
	float SliderToMixerValue (float sliderValue){
		return sliderValue * 80f - 80f;
	}
}
