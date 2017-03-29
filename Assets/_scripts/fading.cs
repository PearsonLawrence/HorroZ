using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class fading : MonoBehaviour {
    public bool FadeIn = true;
    public bool FadeOut;
    public float fade;
    public float DT;
    public Image black;
    private float A = 1;
    public string scene;
    public bool quit;
	// Use this for initialization
	void Start () {
        DT = Time.deltaTime;
        //black = GetComponent<Image>();
        if(!black)
        {
            Debug.Log("brok");
        }
        //A = black.color.a;
	}
	
	// Update is called once per frame
	void Update () {
        if(FadeIn || FadeOut)
        {

            DT = Time.deltaTime;
        }
        
        Mathf.Clamp(A, 0, 255);
        if (FadeIn)
        {
            A -= DT/2;
            Mathf.Clamp(A, 0, 255);
            black.color = new Color(black.color.r, black.color.g, black.color.b, A);
            if(A <= 0)
            {
                FadeIn = false;
            }
            
        }
        if (FadeOut)
        {
            A += DT;
            black.color = new Color(black.color.r, black.color.g, black.color.b, A);

            Debug.Log(A);

            if(A >= 1)
            {
                if (!quit)
                {
                    FadeOut = false;
                    SceneManager.LoadScene(scene, LoadSceneMode.Single);
                }
                else
                {
                    Debug.Log("quit");
                    if (!Application.isEditor)
                        Application.Quit();                    
                }
            }
        }
    }
}
