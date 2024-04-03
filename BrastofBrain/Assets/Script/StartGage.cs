using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGage : MonoBehaviour
{
    // 集中ゲージ表示オブジェクト
    public GameObject CaunterGage = default;
    
    // ゲージ表示フラグ
    private bool flag = true;
    
    // ゲージ表示フラグの更新フラグ
    public static bool flagc = false;

    // Update is called once per frame
    void Update()
    {
        // ゲージが設定され、まだ表示されていない場合に表示する
        if (CASlider.Gage != null && flag == true && flagc == true)
        {
            CaunterGage.SetActive(true); // ゲージを表示する
            flag = false; // ゲージが表示されたことを記録する
            flagc = false; // ゲージ表示更新フラグをリセットする
        }
    }
}
