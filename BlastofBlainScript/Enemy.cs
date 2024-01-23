using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName="Enemy",menuName="CreatEnemy")]

public class Enemy : ScriptableObject
{
   public int id;
    public string name;
    public float defense;

    public float HP;
    public float speed;
    public float jumppow;
    public string info;

    public GameObject sprite;
    
    public Enemy(Enemy enemy){
        this.HP=enemy.HP;
        this.info=enemy.info;
        this.name=enemy.name;
        this.id=enemy.id;
        this.defense=enemy.defense;
        this.jumppow=enemy.jumppow;
        this.speed=enemy.speed;
        this.sprite=enemy.sprite;


    }
    
}
