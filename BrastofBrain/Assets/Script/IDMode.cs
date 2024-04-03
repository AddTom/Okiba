using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.InputSystem;

public class IDMode : MonoBehaviour
{
     // ID入力モードを開始するためのメソッド
     public void StartIDClick()
    {
        SceneManager.LoadScene("ID"); // "ID"シーンをロードしてID入力シーンへ開始
      

    }
     void Update()
    {

        //ジョイスティックでの処理
        if(Input.GetKeyDown ("joystick button 2")&&Gamepad.current == null){
           // ネットワークから切断して、ID入力モードを開始
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("ID");

        }
        //キーボードでEキーが押された場合の処理
        if(Input.GetKey(KeyCode.E)){
            // ネットワークから切断して、ID入力モードを開始
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("ID");

        }
        if (Gamepad.current == null) 
            return;       

         // ゲームパッドのAボタンが押された場合
        if (Gamepad.current.aButton.wasPressedThisFrame) {
             // ネットワークから切断して、ID入力モードを開始
             PhotonNetwork.Disconnect();
            SceneManager.LoadScene("ID");

        }
                
    }
}
