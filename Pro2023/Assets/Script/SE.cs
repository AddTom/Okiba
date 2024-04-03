using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
     public AudioClip Gameover;//ゲームオーバー音声
 
 
    public void PlayStart()
    {
       AudioSourceController.instance.PlayOneShot(Gameover);//ゲームオーバー音声をならす

    }
}