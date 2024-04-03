using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Offline_Shot : MonoBehaviour
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
        magic = magicDataBase.magics[0]; // 魔法のデータをデータベースから取得
        mahoPrefab = magic.Hantei; // 魔法のプレハブを設定
        UI = GameObject.Find("WinorLose"); // UIオブジェクトを取得
        WinorLose = UI.GetComponent<TextMeshProUGUI>(); // UIテキストを取得

        flagA = false;
        WinorLose.text = ""; // テキストを初期化
        Mylife = 100; // プレイヤーのライフを初期化
    }

    

    // Update is called once per frame
    void Update()
    {
        pos = player.transform.position; // プレイヤーの位置を取得
        Rot = player.transform.localEulerAngles.y; // プレイヤーの回転角度を取得
        x = pos.x; // x座標を取得
        y = pos.y; // y座標を取得


         // マウスの右クリックが押された場合、魔法を発射
         if(Input.GetMouseButtonDown(1)&&magic.lostmp<=Ofline_PlayerController.LMP&&caun==false){
                Mashot(x,y,Rot);
                
        }
        // キーボードのジョイスティックボタン1が押された場合、魔法を発射
        if(Input.GetKeyDown ("joystick button 1")&&magic.lostmp<=Ofline_PlayerController.LMP&&Gamepad.current == null&&caun==false){
                Mashot(x,y,Rot);
                
        }
         // キーボードのジョイスティックボタン2が押された場合、魔法を発射
         if(Input.GetKeyDown ("joystick button 2")&&magic.lostmp<=Ofline_PlayerController.LMP&&Gamepad.current == null&&caun==false){
                Mashot(x,y,Rot);
            
        }
        
        //プレイヤーの<Ofline_Hit>().flagがtrueの場合
        if(player.gameObject.GetComponent<Ofline_Hit>().flag==true)
        {
                
            Damage();//ダメージ判定
            player.gameObject.GetComponent<Ofline_Hit>().flag=false;//flagをoffに
            Des();//当たったオブジェクトを破棄
        }

        if(player.gameObject.GetComponent<Ofline_Hit>().Cflag==true){
            
               
            player.gameObject.GetComponent<Ofline_Hit>().Cflag=false;
            Caunte(x,y,Rot);//攻撃を跳ね返す
            Des();//当たったオブジェクトを破棄
        }
            


        //自分のライフが0以下
         if(0>=Mylife){
            WorL(this.gameObject.name);//勝利判定         
        }

        //カウンターのflagがonの時
         if(caun==true){
            CaunOn();//カウンターモードへ
        }
         //カウンターのflagがoffの時
         if(caun==false){
            CaunOff();//カウンターモード終了
        }

        if(flagA==true)//勝敗が決まった時
            RoadTime+=Time.deltaTime;//待機時間

        //3秒後
        if (RoadTime >= 3.0f){
            fin();//シーン変更
        }

        if (Gamepad.current == null)//ゲームパットがない場合 
            return;            

        //ゲームパットが読み込まれているときボタンを押してに魔法を発射する
        if (Gamepad.current.aButton.wasPressedThisFrame&&magic.lostmp<=Ofline_PlayerController.LMP) {
                Mashot(x,y,Rot);
                
        }

        if (Gamepad.current.bButton.wasPressedThisFrame&&magic.lostmp<=Ofline_PlayerController.LMP) {
                Mashot(x,y,Rot);    
        }

    }
    
    //魔法の向きを決める
    private void Mashot (float xa ,float ya,float roty) {

        Rota.getRotate(roty); // プレイヤーの向きを設定
        var bullet = Instantiate(mahoPrefab,new Vector2(xa,ya),Quaternion.identity);// 魔法を発射
        Debug.Log(mahoPrefab);
        
    }

   //カウンターの反射方向を決める
    private void Caunte (float xa ,float ya,float roty) {

        Rota.getRotate(roty); // プレイヤーの向きを設定
        var bullet = Instantiate(mahoPrefab,new Vector2(xa,ya),Quaternion.identity);// カウンターを発動
       
    }

    
    private void Des()
    {
         other=player.gameObject.GetComponent<Ofline_Hit>().otherCollider;

         // 衝突した相手が存在し、かつ攻撃や地面でない場合、相手を破棄
        if(player.gameObject.GetComponent<Ofline_Hit>().otherCollider!=null&&other.gameObject.tag!="attack"&&other.gameObject.tag!="isGround"){
           
            Destroy(other.gameObject);
        }
    }
    

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

    private void WorL(string player)
    {
        this.LoserName=player;//敗者の名前を入れる
        if(this.LoserName==this.gameObject.name){
             WinorLose.text="  You Lose  \nリザルト画面に移ります・・・";// 敗者の場合のテキスト表示
        }

        else{
             WinorLose.text="  You Win  \nリザルト画面に移ります・・・"; // 勝者の場合のテキスト表示
        }
        flagA=true;      
        
    }

    private void CaunOn() {
        Caunter.SetActive(true); // カウンターオブジェクトを表示
    }

    private void CaunOff() {
        Caunter.SetActive(false); // カウンターオブジェクトを非表示
    }

    private void fin() {
        SceneManager.LoadScene("resalt"); // リザルト画面へ遷移
    }
}