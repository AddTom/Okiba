using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject score_object;//スコアについてのオブジェクト
    public GameObject player;//プレイヤーオブジェクト
    public int score_num = 0;//スコア数
    private float time=0f;

    // Update is called once per frame
    void Update()
    {
        if(player!=null)//プレイヤーが存在する限り
        {
            Text score_text = score_object.GetComponent<Text> ();//テキストを更新

            time=time+Time.deltaTime;
            
            //1秒ごとにスコアを1更新する
            if(time>1f){
                score_num=score_num+1;
                 score_text.text = "Score:" + score_num;
                time=0f;
             }    
        }
    }
}
