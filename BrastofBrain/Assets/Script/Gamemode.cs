using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun; 
using UnityEngine.InputSystem; 

public class Gamemode : MonoBehaviour
{
    // スタートボタンがクリックされた時の処理
    public void OnClickStartButton()
    {
        // Photonのネットワーキングを切断し、ゲームシーンに移動する
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Game scene");
    }

   
    // Update is called once per frame
    void Update()
    {
        // ジョイスティックのボタン2が押された時、またはキーボードのEキーが押された時の処理
        if (Input.GetKeyDown("joystick button 2") && Gamepad.current == null)
        {
            PhotonNetwork.Disconnect(); // Photonのネットワーキングを切断
            SceneManager.LoadScene("Game scene"); // ゲームシーンに移動
        }

        if (Input.GetKey(KeyCode.E)) // キーボードのEキーが押された時の処理
        {
            PhotonNetwork.Disconnect(); // Photonのネットワーキングを切断
            SceneManager.LoadScene("Game scene"); // ゲームシーンに移動
        }

        if (Gamepad.current == null) // ゲームパッドが接続されていない場合は以下の処理をスキップ
            return;

        if (Gamepad.current.aButton.wasPressedThisFrame) // ゲームパッドのAボタンが押された時の処理
        {
            PhotonNetwork.Disconnect(); // Photonのネットワーキングを切断
            SceneManager.LoadScene("Game scene"); // ゲームシーンに移動
        }
    }
}