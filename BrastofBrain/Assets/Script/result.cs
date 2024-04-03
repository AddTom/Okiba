using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;


public class result : MonoBehaviour
{

     
      private float con;      // 集中度の値を保持する変数
    private float Restime;  // 参加時間の値を保持する変数
    private float ResCon;   // 集中度の割合を計算するための変数
    private float Reslir;   // リラックス度の割合を計算するための変数
    public TextMeshProUGUI timestr;  // 参加時間を表示するためのテキストオブジェクト
    public TextMeshProUGUI constr;   // 集中度を表示するためのテキストオブジェクト
    public TextMeshProUGUI lirstr;   // リラックス度を表示するためのテキストオブジェクト

    
      // Start is called before the first frame update
    void Start()
    {
        Restime = testkansu.Gametime;  // 参加時間を初期化
        con = testkansu.con;            // 集中度を初期化
    }

    // Update is called once per frame
    void Update()
    {
        // 集中度とリラックス度を計算
        ResCon = con / Restime * 100;
        Reslir = 100 - ResCon;

        // テキストオブジェクトに計算結果を表示
        timestr.SetText("ゲームの参加時間 : {0:1}", Restime); 
        constr.SetText("集中の割合: {0:1}", ResCon); 
        lirstr.SetText("リラックスの割合 : {0:1}", Reslir); 
    }
}