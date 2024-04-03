using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviourPunCallbacks
{

    [SerializeField] private MagicDataBase magicDataBase;//データベース呼び出し
    Magic magic;// 選択された魔法のデータを保持する変数
    private GameObject mahoPrefab= default;// 魔法のプレハブ

    public GameObject player= default; // プレイヤーオブジェクト
    public GameObject cau= default; // 攻撃オブジェクト
    public GameObject Caunter= default;// カウンターオブジェクト
    public  Rota Rota;// プレイヤーの向きを制御するためのスクリプト
    public  MagicDamage [] MagicDamage; // マジックダメージを処理するためのスクリプト
    private Vector2 pos; //座標保存用
    private float Rot;// 回転角度保存用
    float x;            //x座標
    float y;            //y座標

    public static int Mylife=100;// プレイヤーのライフ
    private string LoserName;// 敗者の名前
    private 
    TextMeshProUGUI WinorLose;// 勝利または敗北を表示するUIテキスト
    private GameObject UI;// UIオブジェクト
    float RoadTime=0.0f;// リザルト画面への遷移タイマー
    bool flagA=false; // リザルト画面への遷移フラグ
     public static bool FlagB = false; // リザルト画面への遷移フラグ
    public static bool caun = false; // カウンター発動フラグ
  
    Collider2D other;// 衝突した相手のCollider2D

    

    // Start is called before the first frame update
    void Start()
    {
        magic =magicDataBase.magics[0];//魔法の種類
        mahoPrefab=magic.Hantei;// 魔法のプレハブを設定
        UI=GameObject.Find("WinorLose");// UIオブジェクトを取得
        WinorLose = UI.GetComponent<TextMeshProUGUI>();// UIテキストを取得
        Debug.Log(photonView.OwnerActorNr);
        flagA = false;
        WinorLose.text = ""; // テキストを初期化
        Mylife = 100; // プレイヤーのライフを初期化
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
            
            if(photonView.IsMine){//ローカルプレイヤーなら
                pos = player.transform.position; // プレイヤーの位置を取得
                Rot = player.transform.localEulerAngles.y; // プレイヤーの回転角度を取得
                x = pos.x; // x座標を取得
                y = pos.y; // y座標を取得
            
            // マウスの右クリックが押された場合、魔法を発射
            if(Input.GetMouseButtonDown(1)&&magic.lostmp<=UnityChan2DController.LMP&&caun==false){
                photonView.RPC(nameof(Mashot), RpcTarget.All,x,y,Rot);
                //Debug.Log(MagicDamage[0].MyID);
            }

            // キーボードのジョイスティックボタン1が押された場合、魔法を発射
            if(Input.GetKeyDown ("joystick button 1")&&magic.lostmp<=UnityChan2DController.LMP&&Gamepad.current == null&&caun==false){
                photonView.RPC(nameof(Mashot), RpcTarget.All,x,y,Rot);
                //Debug.Log(MagicDamage[0].MyID);
            }

            // キーボードのジョイスティックボタン2が押された場合、魔法を発射
            if(Input.GetKeyDown ("joystick button 2")&&magic.lostmp<=UnityChan2DController.LMP&&Gamepad.current == null&&caun==false){
                photonView.RPC(nameof(Mashot), RpcTarget.All,x,y,Rot);
                //Debug.Log(MagicDamage[0].MyID);
            }

             //プレイヤーの<Hit>().flagがtrueの場合
            if(player.gameObject.GetComponent<Hit>().flag==true)
            {
                
                photonView.RPC(nameof(Damage), RpcTarget.All);//ダメージ判定
                player.gameObject.GetComponent<Hit>().flag=false;//flagをoffに
                photonView.RPC(nameof(Des), RpcTarget.All);//当たったオブジェクトを破棄
            }

            if(player.gameObject.GetComponent<Hit>().Cflag==true)
            {
               
                player.gameObject.GetComponent<Hit>().Cflag=false;
                photonView.RPC(nameof(Caunte), RpcTarget.All,x,y,Rot);//攻撃を跳ね返す
                 photonView.RPC(nameof(Des), RpcTarget.All);//当たったオブジェクトを破棄
            }

            //自分のライフが0以下
            if(0>=Mylife){
                photonView.RPC(nameof(WorL), RpcTarget.All,this.gameObject.name);//勝利判定 
                
            }

             //カウンターのflagがonの時
            if(caun==true){
                photonView.RPC(nameof(CaunOn), RpcTarget.All);//カウンタースタートへ
            }

             //カウンターのflagがoffの時
            if(caun==false){
                photonView.RPC(nameof(CaunOff), RpcTarget.All);//カウンター終了
            }

            
            if(flagA==true)//勝敗が決まった時
            RoadTime+=Time.deltaTime;//待機時間
            
        }
        if (RoadTime >= 3.0f){
                photonView.RPC(nameof(fin), RpcTarget.All);//シーン変更
        }

        if (Gamepad.current == null) //ゲームパットがない場合
            return;          

         //ゲームパットが読み込まれているときボタンを押してに魔法を発射する
        if (Gamepad.current.aButton.wasPressedThisFrame&&magic.lostmp<=UnityChan2DController.LMP) {
                photonView.RPC(nameof(Mashot), RpcTarget.All,x,y,Rot);
                //Debug.Log(MagicDamage[0].MyID);
        }

        if (Gamepad.current.bButton.wasPressedThisFrame&&magic.lostmp<=UnityChan2DController.LMP) {
                photonView.RPC(nameof(Mashot), RpcTarget.All,x,y,Rot);
                //Debug.Log(MagicDamage[0].MyID);
        }
    }

     //魔法の向きを決める
    [PunRPC]
    private void Mashot (float xa ,float ya,float roty) {

        
        Rota.getRotate(roty);// プレイヤーの向きを設定
        MagicDamage[0].getID(photonView.OwnerActorNr);
        var bullet = Instantiate(mahoPrefab,new Vector2(xa,ya),Quaternion.identity);
       
      
    }

    //カウンターの反射方向を決める
    [PunRPC]
    private void Caunte (float xa ,float ya,float roty) {

        
        Rota.getRotate(roty);// プレイヤーの向きを設定
        MagicDamage[0].getID(photonView.OwnerActorNr);
        var bullet = Instantiate(mahoPrefab,new Vector2(xa,ya),Quaternion.identity);
       
      
    }

    [PunRPC]
    private void Des()
    {
         // 衝突した相手が存在し、かつ攻撃や地面でない場合、相手を破棄
        if(player.gameObject.GetComponent<Hit>().otherCollider!=null){
            other=player.gameObject.GetComponent<Hit>().otherCollider;
            Destroy(other.gameObject);
        }
    }
    
     [PunRPC]
    IEnumerator Damage ()
	{
        
        
        //Text life_text = life_object.GetComponent<Text> ();
        //life_text.text = "×" + life;
		//レイヤーをPlayerDamageに変更
		player.gameObject.layer = 12;
		//while文を10回ループ
		int count = 10;
		while (count > 0){
			//透明にする
			player.gameObject.GetComponent<Renderer>().material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			player.gameObject.GetComponent<Renderer>().material.color = new Color (1,1,1,1);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		//レイヤーをPlayerに戻す
		player.gameObject.layer = 9;
	}

    [PunRPC]
    private void WorL(string player)
    {
        this.LoserName=player;//敗者の名前を入れる
        if(this.LoserName==this.gameObject.name&&photonView.IsMine){
             WinorLose.text="  You Lose  \nリザルト画面に移ります・・・";
        }

        else{
             WinorLose.text="  You Win  \nリザルト画面に移ります・・・";
        }
        flagA=true;      
        
    }

    [PunRPC]
    private void CaunOn()
    {
        Caunter.SetActive(true); // カウンターオブジェクトを表示
        
    }

    [PunRPC]
    private void CaunOff()
    {
        Caunter.SetActive(false);// カウンターオブジェクトを非表示
        
    }

    [PunRPC]
    private void fin()
    {
        PhotonNetwork.LeaveRoom();
      
        SceneManager.LoadScene("resalt"); // リザルト画面へ遷移

    }


}
