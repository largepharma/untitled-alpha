using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PersistentAudioPlayer : MonoBehaviour
{    
    public static PersistentAudioPlayer ins;
    AudioSource player;
    bool initAudio = false;

    [SerializeField]
	bool playAudio = true;
    
    public AudioMixer mixer;
    public AudioClip[] musicTracks;

    //two arrays.
    //in a bigger project, you'd have one array with all of your different snapshots in it.
    //
    public AudioMixerSnapshot[] fullAudio, muteBGM;
    public float transitionTime = 0.5f;

    

    void Awake(){
    	//singleton pattern
    	if(ins == null) {
    		DontDestroyOnLoad(gameObject);
    		ins = this;
    		if(playAudio){
	    		player = GetComponent<AudioSource>();
	    		player.Play();
    		}
    	}else if (ins != this) {
    		Destroy (gameObject);
    	}
    }
    void Start(){
        if (!initAudio){
            //takes the setting "Music", sets it to a mixer friendly value, and sets the BGM setting to that value
            player.outputAudioMixerGroup.audioMixer.SetFloat("BGMVolume", SettingToMixerValue(SaveManager.LoadSetting("Music")));
            player.outputAudioMixerGroup.audioMixer.SetFloat("SFXVolume", SettingToMixerValue(SaveManager.LoadSetting("SFX")));
            initAudio = true;
        }
    }
    public void ChangeMusic(int track){
        StartCoroutine (RunChangeMusic(track));
    }


    //passes an integer. in a larger project it may be a good idea to make an enum or some kind of dictionary instead.
    IEnumerator RunChangeMusic(int track){
        //if it doesnt need to change, dont try to change it
        if (musicTracks[track] == player.clip){
            //inenumerables can't just return nothing, so we yield a break
            yield break;
        }else{
        //transition to mute BGM so it doesnt abrupty jump out and back in.
        mixer.TransitionToSnapshots(muteBGM, new float[]{1.0f}, transitionTime);
        //wait for transition to finish
        yield return new WaitForSeconds(transitionTime);
        player.clip = musicTracks[track];
        player.Play();
        mixer.TransitionToSnapshots(fullAudio, new float[]{1.0f}, transitionTime);
        }
    }


    //private method to convert inverted volume values into mixer values
    float SettingToMixerValue (float settingValue){
        return (1f - settingValue) * 80f - 80f;
    }
}
