using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.InputSystem;

public class IDMode : MonoBehaviour
{
     public void StartIDClick()
    {
        SceneManager.LoadScene("ID");
      

    }
     void Update()
    {

        
        if(Input.GetKeyDown ("joystick button 2")&&Gamepad.current == null){
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("ID");

        }

        if(Input.GetKey(KeyCode.E)){
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("ID");

        }
        if (Gamepad.current == null) 
            return;            

        if (Gamepad.current.aButton.wasPressedThisFrame) {
             PhotonNetwork.Disconnect();
            SceneManager.LoadScene("ID");

        }
                
    }
}
