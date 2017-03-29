using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPressScript : MonoBehaviour {
    public fading fader;
    private Button butt;
    public string scene;
    public bool isQuitButton;
    public void onPress()
    {
        fader.FadeOut = true;
        fader.scene = scene;
        fader.quit = isQuitButton;
    }

 

	
}
