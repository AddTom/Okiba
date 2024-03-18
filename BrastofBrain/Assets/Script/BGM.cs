using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class BGM : MonoBehaviour
{

   static public BGM instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
   
     public AudioSource A_BGM;//AudioSource型の変数A_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
    public AudioSource B_BGM;//AudioSource型の変数B_BGMを宣言　対応するAudioSourceコンポーネントをアタッチ
     private string beforeScene="Title";//string型の変数beforeSceneを宣言 


    private void Start()
    {
      
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(A_BGM.gameObject);
            DontDestroyOnLoad(B_BGM.gameObject);
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
              A_BGM.Play();
        

       
       
    }

   
   


     void OnActiveSceneChanged (Scene prevScene, Scene nextScene)
    {      
            //シーンがどう変わったかで判定
            //Scene1からScene2へ
            if (beforeScene == "Menu" && nextScene.name == "Game scene")
            {
              A_BGM.Stop();
              B_BGM.Play();
            }

            // Scene1からScene2へ
             if (beforeScene == "Game scene" && nextScene.name == "resalt")
             {
              A_BGM.Play();
              B_BGM.Stop();
             }
             // Scene1からScene2へ
             if (beforeScene == "resalt" && nextScene.name == "Title")
             {

             }                  

            Debug.Log(beforeScene);
              Debug.Log(nextScene.name);

        //遷移後のシーン名を「１つ前のシーン名」として保持
        beforeScene = nextScene.name;
    }
}
