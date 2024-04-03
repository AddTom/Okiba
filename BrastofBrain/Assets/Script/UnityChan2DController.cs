using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 
using Photon.Realtime; 
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class UnityChan2DController : MonoBehaviourPunCallbacks, IPunObservable,IDdamage
{
   public float maxSpeed = 10f; // 最大速度
    public float jumpPower = 1000f; // ジャンプ力
    public Vector2 backwardForce = new Vector2(-4.5f, 5.4f);

    public LayerMask whatIsGround; // 地面と判定するレイヤーマスク

    private Animator m_animator; // アニメーター
     private BoxCollider2D m_boxcollier2D; // ボックスコライダー
    private Rigidbody2D m_rigidbody2D; // リジッドボディ
 private bool m_isGround; // 地面に接しているかどうかのフラグ
      private const float m_centerY = 1.5f; // 中心のY座標
   private State m_state = State.Normal; // プレイヤーの状態

  
  public GameObject gameoverText; // ゲームオーバーのテキスト
    public GameObject MahoPrefab; // 魔法のプレハブ
    public AudioClip sound1; // 効果音1
    public AudioClip Hit; // ヒットの効果音
    private AudioSource audioSource; // オーディオソース


     public PhotonView myView;//マルチの際に自分の視点に


     public float CounterGage=90f;// カウンターゲージ
     private float Countertime=2.0f;// カウンターの時間
     [SerializeField]
   
    private float Gagetime;// ゲージ時間
    private float cooltime; // クールタイム
      

            
   [SerializeField] private MagicDataBase magicDataBase;//データベース呼び出し
    Magic magic;        //保存用

    private int life=100;// プレイヤーの体力

    [SerializeField]
    private  Slider hpslider;// HPスライダー
    private  float mp=0f;// プレイヤーのMP
    public static float LMP=0f;
    [SerializeField]
    private Slider mpslider; // MPスライダー
    private float defense=1;// 防御力

    //public PhotonTransformView myTransform;
    private Camera mainCam;// メインカメラ
    
    
    
     private void Start() {
        life=100;
        if (myView.IsMine)    //自キャラであれば実行
        {
            magic =magicDataBase.magics[0];//魔法の種類
            MahoPrefab=magic.Hantei;

            //MainCameraのtargetにこのゲームオブジェクトを設定
            mainCam = Camera.main;  
            mainCam.GetComponent<Ca>().target = this.gameObject.transform;
            StartGage.flagc=true;
        }
    
    }

   

    void Reset()
    {
        Awake();

        // UnityChan2DController
        backwardForce = new Vector2(-4.5f, 5.4f);
        whatIsGround = 1 << LayerMask.NameToLayer("Ground");

        // Transform
        transform.localScale = new Vector3(1, 1, 1);

        // Rigidbody2D
        m_rigidbody2D.gravityScale = 3.5f;
        //m_rigidbody2D.fixedAngle = true;

        // BoxCollider2D
        m_boxcollier2D.size = new Vector2(1, 2.5f);
        m_boxcollier2D.offset = new Vector2(0, -0.25f);

        // Animator
        m_animator.applyRootMotion = false;
    }

    void Awake()
    {
          //各コンポーネントの取得
        m_animator = GetComponent<Animator>();
        m_boxcollier2D = GetComponent<BoxCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    

   
    private void Update()
    {
         var gamepad = Gamepad.current;//ゲームパットの読み取り
        cooltime+=Time.deltaTime; //クールタイムの設定
        //Debug.Log(gamepad);
               

         if (myView.IsMine)//ローカルプレイヤーの場合
        {
            if (m_state != State.Damaged)
            {
                //各キーの読み取り
                float x = Input.GetAxis("Horizontal");;//移動処理
                bool jump = Input.GetButtonDown("Jump");//ジャンプ処理
                hpslider.value=(float)life;//hpバーの処理

                Move(x, jump);//移動処理

            }


            LMP=mp;//mpの更新

            //各コントローラーで攻撃した際の処理
            //マウスクリック
            if (Input.GetMouseButtonDown(1)&&magic.lostmp<=mp&&Shot.caun==false){ 
                mp=mp-magic.lostmp;//mpを消費して
                m_animator.SetBool("Attack", true);//攻撃モーションへ
                
            }

            //ジョイスティックのボタンを押す
            //この際の処理はマウスクリックと同じ
            if (Input.GetKeyDown ("joystick button 1")&&magic.lostmp<=mp&&Gamepad.current == null&&Shot.caun==false){ 
                mp=mp-magic.lostmp;
                m_animator.SetBool("Attack", true);
            }
            
            if (Input.GetKeyDown ("joystick button 2")&&magic.lostmp<=mp&&Gamepad.current == null&&Shot.caun==false){ 
                mp=mp-magic.lostmp;
                m_animator.SetBool("Attack", true);
            }

            Shot.Mylife=life;//lifeを更新する

            if(cooltime>=0.5f){//mpとlifeの回復処理
                MPInc();
                
                if (myView.IsMine) {
                    if(life<=100f){
                        life+=testkansu.life;
                    }
                    cooltime=0;
                }
            }

            //Debug.Log(CounterGage);
            
             //pキーでライフを0にしてゲームを終了
            if (Input.GetKey(KeyCode.P)){
                

                   life=0;
                    
                
            }

            //eキーでカウンター
            if (Input.GetKey(KeyCode.E)&&CounterGage>=90f){
                

                    Countertime=2.0f;//カウンターの時間
                    CounterGage=0.0f;//ゲージを0に
                    Offline_Shot.caun=true;//カウンターオブジェクトをonにする
                    
                
            }

            //ジョイステックの0のボタンでカウンター
            if (Input.GetKeyDown ("joystick button 0")&&CounterGage>=90f&&Gamepad.current == null){
                
                     //処理はeキーでのカウンター処理と同様
                    Countertime=2.0f;
                    CounterGage=0.0f;
                    Shot.caun=true;
                    
                
            }

           

            if(Shot.caun==true&&Countertime>=0f){
                Countertime-=Time.deltaTime;//カウンターの発動時間を計測
            }
            if(Shot.caun==true&&0f>=Countertime){
               Shot.caun=false;//カウンターをoffに
            }
            
            //カウンターのゲージを増やす処理
            if(Shot.caun==false){
                if(90f>CounterGage&&CounterGage>=0.0f){
                    Gagetime+=Time.deltaTime;
                    if(1.0f<=Gagetime){
                        Gagetime=0.0f;
                        CounterGage+=3.0f;
                    }
                }
            }
              CASlider.Gage=CounterGage;//カウンターのゲージ更新

        }
           
        hpslider.value=life;//hpのゲージ更新
        mpslider.value=mp;//mpのゲージ更新
      
        if (Gamepad.current == null) 
            return;            

        //攻撃処理
        if (Gamepad.current.aButton.wasPressedThisFrame&&magic.lostmp<=mp) 
                mp=mp-magic.lostmp;
         //カウンター処理
         if (CounterGage>=90f&&Gamepad.current.xButton.wasPressedThisFrame){
                
                    Countertime=2.0f;
                    CounterGage=0.0f;
                    Shot.caun=true;   
                
        }
    }

    void Move(float move, bool jump)
    {
        // 移動方向に応じて向きを変更
        if (Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }
        // 移動量に応じて速度を設定
        m_rigidbody2D.velocity = new Vector2(move * maxSpeed, m_rigidbody2D.velocity.y);
        

         float speed=move;
         if(speed<=-0.001)
            speed=speed*-1;
            
         // アニメーターに移動速度を設定
        m_animator.SetFloat("Horizontal", speed);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
        m_animator.SetBool("isGround", m_isGround);

         // ジャンプが有効でかつ接地している場合、ジャンプする
        if (jump && m_isGround)
        {
            m_animator.SetTrigger("Jump");
            SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
            m_rigidbody2D.AddForce(Vector2.up * jumpPower);
            audioSource.PlayOneShot(sound1);
            
        }
    }

    void FixedUpdate()
    {
         // 地面との接触判定を行う
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.tag == "DamageObject" && m_state == State.Normal)
    //     {
    //         m_state = State.Damaged;
    //         StartCoroutine(INTERNAL_OnDamage());
    //     }
    // }

    IEnumerator INTERNAL_OnDamage()
    {
         // ダメージアニメーションの再生
        m_animator.Play(m_isGround ? "Damage" : "AirDamage");
        m_animator.Play("Idle");

         // ダメージを受けたことを通知
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);

        m_rigidbody2D.velocity = new Vector2(transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);

        yield return new WaitForSeconds(.2f);

        while (m_isGround == false)
        {
            yield return new WaitForFixedUpdate();
        }
        m_animator.SetTrigger("Invincible Mode");
        m_state = State.Invincible;
    }

    void MPInc(){//MPを増加するプログラム
        if (myView.IsMine) {
            if(mp<=100f){
               mp+=testkansu.mp;
               
            }
        }
        
    }

     // ダメージ判定
    public void Damagea(float power){
        life=life-(int)(power);
        Debug.Log(life);
    }

    enum State
    {
        Normal,
        Damaged,
        Invincible,
    }

    void FinA(){
         m_animator.SetBool("Attack", false);
    }

   

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // 自身のHP,MPを送信する
            stream.SendNext(mp);
            stream.SendNext(life);
        } else {
            // 他プレイヤーのHP,MPを受信する
            mp = (float)stream.ReceiveNext();
            life = (int)stream.ReceiveNext();
        }
    }
}


 public interface IDdamage
{
       public void Damagea(float power);
}



