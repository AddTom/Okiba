using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rota : MonoBehaviour
{

    public static float playerRotate;
   
    public int comID;

     public  void getRotate(float rot){
          playerRotate=rot; //プレイヤーのrotatを保存する
     }

}
