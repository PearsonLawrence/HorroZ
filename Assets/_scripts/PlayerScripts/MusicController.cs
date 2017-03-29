using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public AudioSource[] Musics;
    bool nextSong = false;
	// Update is called once per frame
	void Update () {
        int i;

		if(Musics[0].isPlaying && Musics[1].isPlaying == false)
        {

        }
        else
        {
            Musics[1].Play();
        }

        if (Musics[1].isPlaying && Musics[0].isPlaying == false)
        {


        }
        else
        {
            Musics[0].Play();
        }


    }
}
