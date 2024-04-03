using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName="Magic",menuName="CreatMagic")]


public class Magic : ScriptableObject
{
    public int id;// 魔法のID
  public string name; // 魔法の名前
    public string info; // 魔法の説明
    public Type type; // 魔法のタイプ
    public Sprite sprite; // 魔法のアイコン画像
    public float forse; // 魔法の威力
    public float lostmp; // 魔法を使用するのに必要なMPの消費量
    public float speed; // 魔法の速度
    public GameObject Hantei; // 魔法の当たり判定を表すゲームオブジェクト

    // 魔法の種類を表す列挙型
    public enum Type{
        Fire,
    }

    // コンストラクター：Magicクラスのコピーを生成する際に使用
    public Magic(Magic magic){
        this.type=magic.type;
        this.info=magic.info;
        this.name=magic.name;
        this.id=magic.id;
        this.sprite=magic.sprite;
        this.Hantei=magic.Hantei;
        this.lostmp=magic.lostmp;

    }
    
}
