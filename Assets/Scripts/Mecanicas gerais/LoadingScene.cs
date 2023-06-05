using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private bool IsReadyToDoCoroutine = true;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loading Iniciado");
        StartCoroutine("WaitLoadScene");
        
    }

      
    
     IEnumerator WaitLoadScene()
    { 
       
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(5f);       
         IsReadyToDoCoroutine = true;       
         SceneManager.LoadScene("Main_Scene_2");

    }
    
}
