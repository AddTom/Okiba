using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CASlider : MonoBehaviour
{

     [SerializeField]
    private Slider Counterlider;// スライダーオブジェクトを参照
    public static float Gage;// スライダーの値を制御する静的変数


    // Update is called once per frame
    void Update()
    {
        
         // スライダーの値を設定する
          Counterlider.value=Gage;
        
    }
}
