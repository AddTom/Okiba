using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ofline_Hit : MonoBehaviour
{
    
    public  bool flag=false;
    public  bool Cflag=false;
    public float pow=10f;
    
    public Collider2D MyCollider;
    public Collider2D otherCollider=null;

    public GameObject Pearent= default;
     
    
    // Start is called before the first frame update
    void Start()
    {
         MyCollider = GetComponent<Collider2D>();
        
         Debug.Log(MyCollider);
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void OnTriggerEnter2D(Collider2D other) {
   
         otherCollider=other;
            {
                if(MyCollider!=otherCollider){
                    if(Shot.caun==true){
                        Cflag=true;
                    }
                    else{
                        //MagicDamage.Damage(MyCollider,pow);
                        flag=true;
                       
                    }
                }
                    
            }   
     }

      void OnCollisionEnter2D(Collision2D collision)
    {
    
            {
                if(collision.gameObject.CompareTag("enemy")){
                    if(Offline_Shot.caun==true){
                        Cflag=true;
                    }
                    else{
                         Enem_Damage enemDamageInstance = new Enem_Damage();
                         enemDamageInstance.Damage(collision, pow);
                         flag = true;
                         Destroy (collision.gameObject);
                    }
                }
                    
            }   
    }
           
}

         
    


