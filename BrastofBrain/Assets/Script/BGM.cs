using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class BGM : MonoBehaviour
{

   static public BGM instance; // BGMスクリプトのインスタンスを管理するための変数

    // シーンが切り替わってもオブジェクトが破棄されないようにする処理
    private void Awake()
    {
      // インスタンスがまだ存在しない場合
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // オブジェクトをシーン遷移時に破棄しないように設定
        }
        else
        {
            Destroy(this.gameObject); // インスタンスが既に存在する場合、新しいオブジェクトを破棄
        }

    }
   
    // BGMを管理するためのAudioSourceコンポーネントを宣言
     public AudioSource A_BGM;
    public AudioSource B_BGM;
     private string beforeScene="Title";// 前のシーン名を保存する変数


    private void Start()
    {
      
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(A_BGM.gameObject);
            DontDestroyOnLoad(B_BGM.gameObject);
            SceneManager.activeSceneChanged += OnActiveSceneChanged; // シーン切り替え時のイベントハンドラを設定
            A_BGM.Play();     // 最初のBGMを再生
        

       
       
    }
   
     // シーンが切り替わる際の処理
     void OnActiveSceneChanged (Scene prevScene, Scene nextScene)
    {      
            // シーンの変化に応じてBGMを切り替える処理を実行
           // MenuからGame sceneに遷移した場合
            if (beforeScene == "Menu" && nextScene.name == "Game scene")
            {
              A_BGM.Stop();//Menu用のbgmを停止
              B_BGM.Play();// Game scene用のBGMを再生
            }

            // Game sceneからresaltに遷移した場合
             if (beforeScene == "Game scene" && nextScene.name == "resalt")
             {
               A_BGM.Play(); // Game scene用のBGMを再生
               B_BGM.Stop(); // resalt用のBGMを停止
             }
              // resaltからTitleに遷移した場合は何もしない

             // デバッグログに前のシーン名と次のシーン名を出力
            Debug.Log(beforeScene);
            Debug.Log(nextScene.name);

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}
