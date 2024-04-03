using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class UnityChan2DController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpPower = 1000f;
    public Vector2 backwardForce = new Vector2(-4.5f, 5.4f);

    public LayerMask whatIsGround;

    private Animator m_animator;
    private BoxCollider2D m_boxcollier2D;
    private Rigidbody2D m_rigidbody2D;
    private bool m_isGround;
    private const float m_centerY = 1.5f;

    private State m_state = State.Normal;

  
    public GameObject gameoverText;

    public AudioClip sound1;
    AudioSource audioSource;

    //private bool Hit=false;
    private int life=3;
     public GameObject life_object;
     public GameObject coin_object;
     public AudioClip getCoin;
     private int coin=0;
    public AudioClip Hit;
      

   
   

   

    void Reset()
    {
        Awake();

        // UnityChan2DController
        maxSpeed = 10f;
        jumpPower = 1000;
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
        m_animator = GetComponent<Animator>();
        m_boxcollier2D = GetComponent<BoxCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    

   
    void Update()
    {
        if (m_state != State.Damaged)
        {
            float x = Input.GetAxis("Horizontal");
            bool jump = Input.GetButtonDown("Jump");

             

          

            Move(x, jump);

            
           
        }
    }

     

     
    

    

    void Move(float move, bool jump)
    {
        
        if (Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }

        m_rigidbody2D.velocity = new Vector2(move * maxSpeed, m_rigidbody2D.velocity.y);

         float speed=move;
         if(speed<=-0.001)
            speed=speed*-1;

        m_animator.SetFloat("Horizontal", speed);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
        m_animator.SetBool("isGround", m_isGround);

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
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollier2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "DamageObject" && m_state == State.Normal)
        {
            m_state = State.Damaged;
            StartCoroutine(INTERNAL_OnDamage());
        }
    }

    IEnumerator INTERNAL_OnDamage()
    {
        m_animator.Play(m_isGround ? "Damage" : "AirDamage");
        m_animator.Play("Idle");

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

      void OnCollisionEnter2D(Collision2D other)
     {
          
           if(other.gameObject.CompareTag("Wall"))
           {
                // Playerオブジェクトを消去する
                Destroy(this.gameObject);

                Text life_text = life_object.GetComponent<Text> ();
                    life_text.text = "×" + 0;
                // ゲームオーバーを呼び出す
                 gameoverText.SetActive(true);
           }

          if(other.gameObject.CompareTag("enemy"))
           {
                
                if(life>0)
                {
                    Destroy(other.gameObject);
                    StartCoroutine ("Damage");
                    AudioSourceController.instance.PlayOneShot(Hit);

                }
                

                if(life<=0){
                    // Playerオブジェクトを消去する
                    Destroy(this.gameObject);
                     // ゲームオーバーを呼び出す
                    gameoverText.SetActive(true);
                }
                
           }

           if(other.gameObject.CompareTag("coin"))
            {
                 AudioSourceController.instance.PlayOneShot(getCoin);
                Destroy(other.gameObject);
                coin++;
                Text coin_text = coin_object.GetComponent<Text> ();
                coin_text.text = "×" + coin;

                if(coin>=10)
                {
                    life++;
                    Text life_text = life_object.GetComponent<Text> ();
                    life_text.text = "×" + life;
                    coin=0;
                    
                    coin_text.text = "×" + coin;


                }
                
                


            }
                 
               

     }

     IEnumerator Damage ()
	{
        life--;
        
         Text life_text = life_object.GetComponent<Text> ();
          life_text.text = "×" + life;
		//レイヤーをPlayerDamageに変更
		gameObject.layer = 12;
		//while文を10回ループ
		int count = 30;
		while (count > 0){
			//透明にする
			GetComponent<Renderer>().material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			GetComponent<Renderer>().material.color = new Color (1,1,1,1);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}
        
        
        
        
		//レイヤーをPlayerに戻す
		gameObject.layer = 9;
	}
    

    

    enum State
    {
        Normal,
        Damaged,
        Invincible,
    }
}
