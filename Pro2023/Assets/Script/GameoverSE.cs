using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverSE : MonoBehaviour
{
    public GameObject player;//プレイヤ―オブジェクト
     public AudioClip Gameover;//ゲームオーバー音声
     private int a=1;   //flag




    // Update is called once per frame
    void Update()
    {
         if(player==null&&a==1)
        {
            AudioSourceController.instance.PlayOneShot(Gameover);//ゲームオーバー音声を流す
            a=0;//flagをに
        }
    }
}
