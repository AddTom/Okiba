using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioClip getCoin;
     public GameObject life_object;

     void OnCollisionEnter2D(Collision2D other)
     {

   
           if(other.gameObject.CompareTag("Player"))
           {
            AudioSourceController.instance.PlayOneShot(getCoin);
            Destroy(gameObject);
           
        
    
           }
          

     }
    
    
}
