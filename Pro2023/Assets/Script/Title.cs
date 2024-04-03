using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
     public AudioClip Gameover;//ゲームオーバー音声
 
    public void OnClickStartButton()
    {
         AudioSourceController.instance.PlayOneShot(Gameover);//ゲームオーバー音声を鳴らす
         SceneManager.LoadScene("Title");//タイトルに戻る
    }
    

     
}
