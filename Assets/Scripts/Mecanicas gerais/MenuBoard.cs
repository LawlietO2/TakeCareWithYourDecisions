using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBoard : MonoBehaviour
{
    public void Iniciar_Jogo () {
        Debug.Log("Iniciar Jogo");
        SceneManager.LoadScene("Carregando");
    }

    public void Creditos_Jogo () {
        Debug.Log("Creditos Jogo");
        SceneManager.LoadScene("Creditos");
    }
    public void Voltar_Menu () {
        SceneManager.LoadScene("Main_Menu");
    }
    public void Sair_Jogo (){
        Debug.Log("Saiu do Jogo");
        Application.Quit();
    }
}