using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "CreateEnemyDataBase")]
public class EnemyDataBease : ScriptableObject
{
    // 敵キャラクターデータを保持するリスト
   public List <Enemy> enemys=new List<Enemy>(); 
}
