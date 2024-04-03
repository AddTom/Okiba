using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemode : MonoBehaviour
{

    public void OnClickStartButton()
{
    SceneManager.LoadScene("Game scene");//ゲームシーンに変更
}

}
