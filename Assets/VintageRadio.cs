using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VintageRadio : MonoBehaviour
{

    [SerializeField]
    AudioSource tangoRoomGuide;

    AudioSource radioMusic;

    private void Start()
    {
        radioMusic = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(tangoRoomGuide.isPlaying)
        {
            radioMusic.volume = 0.3f;
        }
        else
        {
            radioMusic.volume = 0.7f;
        }
    }
}
