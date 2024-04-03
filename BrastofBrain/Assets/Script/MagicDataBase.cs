using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // 魔法をデータを保持するリスト

[CreateAssetMenu(fileName = "MagicDataBase", menuName = "CreateMagicDataBase")]
public class MagicDataBase : ScriptableObject
{
    public List<Magic> magics=new  List<Magic>();
}
