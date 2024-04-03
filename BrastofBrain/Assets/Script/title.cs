using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.InputSystem;

public class TitleController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // ゲームパッドの2番目のボタン（Bボタン）が押されたとき、またはEキーが押されたときにタイトルシーンに戻る
        if (Input.GetKeyDown("joystick button 2") && Gamepad.current == null || Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("Title");
        }

        // ゲームパッドのAボタンが押されたときにタイトルシーンに戻る
        if (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Title");
        }
    }
}