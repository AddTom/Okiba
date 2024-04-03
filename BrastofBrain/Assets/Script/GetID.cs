using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.InputSystem;
using TMPro;

public class GetID : MonoBehaviour
{
    public  TMP_InputField inputField;// ユーザーのIDを入力するためのTMP_InputField
    public static string ID; // 取得したユーザーのIDを保持する静的変数
    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("ID").GetComponent< TMP_InputField>();// ユーザーのIDを入力するTMP_InputFieldを取得
        ID="0"; // 初期値としてIDを"0"に設定
    }

    public void GetIDs()
    {
        //InputFieldからテキスト情報を取得する
        ID = inputField.text;
        Debug.Log(ID); // デバッグログに取得したIDを出力
         SceneManager.LoadScene("Menu"); // メニューシーンに遷移
 
    }
}
