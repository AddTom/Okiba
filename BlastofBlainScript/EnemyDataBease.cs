using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "CreateEnemyDataBase")]
public class EnemyDataBease : ScriptableObject
{
   public List <Enemy> enemys=new List<Enemy>(); 
}
