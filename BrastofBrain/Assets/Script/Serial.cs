using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serial : MonoBehaviour
{
    public SerialHandler serialHandler; // シリアル通信を処理するためのハンドラ

    private float time; // 時間を保持する変数


    // Update is called once per frame
    void Update()
    {
        // testkansu.flag の値をシリアル通信で送信
        serialHandler.Write(testkansu.flag);
        
        // 時間をリセットし、testkansu.flag の値をデバッグログに出力
        time = 0;
        Debug.Log(testkansu.flag);
    }

    // アプリケーションが終了する際に呼び出される
    private void OnApplicationQuit()
    {
        // シリアル通信をオフ
        // serialHandler.Close();
    }
}