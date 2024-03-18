using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Offline_Shot : MonoBehaviour
{

    
    [SerializeField] private MagicDataBase magicDataBase;//データベース呼び出し
    Magic magic;
    private GameObject mahoPrefab= default;

    public GameObject player= default;
    public GameObject cau= default;
    public GameObject Caunter= default;
    public  Rota Rota;
    public  MagicDamage [] MagicDamage;
    private Vector2 pos; //座標保存用
    private float Rot;
    float x;            //x座標
    float y;            //y座標

    public static int Mylife=100;
    private string LoserName;
    private 
    TextMeshProUGUI WinorLose;
    private GameObject UI;
    float RoadTime=0.0f;
    bool flagA=false;
    public static  bool FlagB=false;
    
    public static bool caun=false;
  

    Collider2D other;

        // Start is called before the first frame update
    void Start()
    {
        magic =magicDataBase.magics[0];//魔法の種類
        mahoPrefab=magic.Hantei;
        UI=GameObject.Find("WinorLose");
        WinorLose = UI.GetComponent<TextMeshProUGUI>();
        
        flagA=false;
        WinorLose.text="";
        Mylife=100;
        
    }

    // Update is called once per frame
    void Update()
    {
        pos=player.transform.position;
        Rot=player.transform.localEulerAngles.y;
        x=pos.x;
        y=pos.y;

         
         if(Input.GetMouseButtonDown(1)&&magic.lostmp<=Ofline_PlayerController.LMP&&caun==false){
                Mashot(x,y,Rot);
                
        }

        if(Input.GetKeyDown ("joystick button 1")&&magic.lostmp<=Ofline_PlayerController.LMP&&Gamepad.current == null&&caun==false){
                Mashot(x,y,Rot);
                
        }

         if(Input.GetKeyDown ("joystick button 2")&&magic.lostmp<=Ofline_PlayerController.LMP&&Gamepad.current == null&&caun==false){
                Mashot(x,y,Rot);
            
        }

        if(player.gameObject.GetComponent<Ofline_Hit>().flag==true)
        {
                
            Damage();
            player.gameObject.GetComponent<Ofline_Hit>().flag=false;
            Des();
        }

        if(player.gameObject.GetComponent<Ofline_Hit>().Cflag==true){
            
               
            player.gameObject.GetComponent<Ofline_Hit>().Cflag=false;
            Caunte(x,y,Rot);
            Des();
        }
            



         if(0>=Mylife){
            WorL(this.gameObject.name);         
        }

         if(caun==true){
            CaunOn();
        }

         if(caun==false){
            CaunOff();
        }

        if(flagA==true)
            RoadTime+=Time.deltaTime;

        if (RoadTime >= 3.0f){
            fin();
        }

        if (Gamepad.current == null) 
            return;            

        if (Gamepad.current.aButton.wasPressedThisFrame&&magic.lostmp<=Ofline_PlayerController.LMP) {
                Mashot(x,y,Rot);
                
        }

        if (Gamepad.current.bButton.wasPressedThisFrame&&magic.lostmp<=Ofline_PlayerController.LMP) {
                Mashot(x,y,Rot);    
        }

    }
    
    private void Mashot (float xa ,float ya,float roty) {

        Rota.getRotate(roty);
        var bullet = Instantiate(mahoPrefab,new Vector2(xa,ya),Quaternion.identity);
        Debug.Log(mahoPrefab);
        
    }

   
    private void Caunte (float xa ,float ya,float roty) {

        Rota.getRotate(roty);
        var bullet = Instantiate(mahoPrefab,new Vector2(xa,ya),Quaternion.identity);
       
    }

    
    private void Des()
    {
         other=player.gameObject.GetComponent<Ofline_Hit>().otherCollider;


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
             WinorLose.text="  You Lose  \nリザルト画面に移ります・・・";
        }

        else{
             WinorLose.text="  You Win  \nリザルト画面に移ります・・・";
        }
        flagA=true;      
        
    }

    private void CaunOn()
    {
        Caunter.SetActive(true);
        
    }

    private void CaunOff()
    {
        Caunter.SetActive(false);
        
    }

    private void fin()
    {
    
        SceneManager.LoadScene("resalt");

    }

}
