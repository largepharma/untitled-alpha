using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SettingsValue : MonoBehaviour
{
	Text valueText;

    // Start is called before the first frame update
    void Awake()
    {
        valueText = GetComponent<Text>();
    }

    //Called as a Dynamic Float through the Slider's On Value Changed () function.
	public void UpdateValue(float f){
		//print the value without a decimal + %
		valueText.text = (f * 100).ToString("F0") + "%";
	}
}
