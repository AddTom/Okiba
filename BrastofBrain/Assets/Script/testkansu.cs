using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;



public class testkansu : MonoBehaviour
{
    private TextAsset NouhaFile;
    List<string[]> NouhaData=new List<string[]>();
    //public GameObject Path=null;//パスを確認する用
    private string File="/csv/num.csv";//ファイル名
    //ファイル読み取り用


   //アクションゲーム用
    public static float mp=0;
    public static int life=0;
    public static float con;
    public static float Gametime=0;
    
    private float time;
    private int line=0;


/* ファイルのパスを読み取り用変数 
　　Application.streamingAssetsPathは実行ファイルはStreamingAssetsに入れるとビルド後にも中のデータを使える*/
    private static readonly string FoldePath = Application.streamingAssetsPath + "/EEG_class/dist";//ファイルパス
    private static readonly string ExePath = FoldePath + "/predictEEG.exe";//実行ファイル
    private Process kakikomiexe;
    public static string flag;

  public void KillProcess(int systemId) //裏で動いているプロセスを破棄する
  {
    Process p = Process.GetProcessById(systemId);
    if(p != null)
    {
      p.Kill();
    }
  }
    


  private void Awake()
  {
    kakikomiexe=new Process();
    flag="-1";
    if(GetID.ID==null){//デバック用GetIDがNULLでも動くようにする
          GetID.ID="0";
    }

    //プロセス起動に必要な値をセット
    kakikomiexe.StartInfo = new ProcessStartInfo
    {

            FileName = ExePath,                        // 起動するファイルのパスを指定する
            UseShellExecute = false,                    // プロセスの起動にオペレーティング システムのシェルを使用するかどうか(既定値:true)
            WorkingDirectory = FoldePath,              // 開始するプロセスの作業ディレクトリを取得または設定する(既定値:"")
            RedirectStandardInput = true,               // StandardInput から入力を読み取る(既定値：false)
            RedirectStandardOutput = true,              // 出力を StandardOutput に書き込むかどうか(既定値：false)
            CreateNoWindow = true,                     // プロセス用の新しいウィンドウを作成せずにプロセスを起動するかどうか(既定値：false)
            ArgumentList =                            //IDを因数として渡す(GetID.IDはGetIDの変数)
            {
                GetID.ID
            }                     
            
    };

    //外部プロセスの終了を検知する
        kakikomiexe.EnableRaisingEvents = true;
        kakikomiexe.Exited += DisposeProcess;

        // プロセスを起動する
        kakikomiexe.Start();
        kakikomiexe.BeginOutputReadLine();
        UnityEngine.Debug.Log("起動完了");


  }

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log(Application.streamingAssetsPath);//パスの確認
        
        //変数の初期化
        con=0;  
        Gametime=0;
        SceneManager.sceneUnloaded += SceneUnloaded;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        time=time+Time.deltaTime;//時間計測
       
        if(time>1.001f){//1秒ごとに
          
          using(var Str=new FileStream(FoldePath+File, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)){//ファイルの読み取りに必要
            Str.Seek(line,System.IO.SeekOrigin.Begin);//ファイルの最初からlineにいれた数に移動する
           // Str.Seek(-4,System.IO.SeekOrigin.End);
            using(StreamReader ReadFile=new StreamReader(Str)){//ファイルの読み取りに必要
             // Debug.Log("1+");
               Gametime++;
               mp=0.001f;
               life=0;
              if(!ReadFile.EndOfStream){//最後の行でないときに実行される
               //   Debug.Log("2+");
                  
                  flag=ReadFile.ReadLine(); //ファイル内の文字を変数に保存
                  UnityEngine.Debug.Log(flag);//デバック用
                  if(flag=="1"){//集中しているとき
                      con++;
                      mp = 3f;

                          //Debug.Log("1");
                  }

                  if(flag=="0"){//リラックスしているとき
                           life =  1;
                  }
                 // Debug.Log("3+");
                 line+=12;
                  time=0f;

              }
              else
              {
                 line=0;
              }
            }
          }
         


            
        }
     }

     void SceneUnloaded  (Scene nextScene) {
       flag="2";
        UnityEngine.Debug.Log(nextScene.name);
       
        DisposeProcess();
        //kakikomiexe.Close();

    }

     private void OnApplicationQuit()//シーンを移動したときに
    {
      flag="2";
      UnityEngine.Debug.Log("破棄");
       DisposeProcess();
    }

    private static void OnStandardOut(object sender, DataReceivedEventArgs e)
        => UnityEngine.Debug.Log($"外部プロセスの標準出力 : {e.Data}");
    
    private void DisposeProcess(object sender, EventArgs e)
        => DisposeProcess();


    

    


    private void DisposeProcess()
    {
        if (kakikomiexe == null || kakikomiexe.HasExited) return;
        
        kakikomiexe.StandardInput.Close();
        kakikomiexe.CloseMainWindow();
        kakikomiexe.Dispose();
        kakikomiexe = null;
       UnityEngine.Debug.Log("破棄2");
    }
}
