using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Global : MonoBehaviour
{
    private bool TEST = false;
    public GameObject male;
    public GameObject[] scanArray;
    public GameObject locomotion;
    public GameObject locomotionAux;
    private bool IsReadyToDoCoroutine = true;
    public bool button;
    public Vector3 maleV3;
    public Vector3 maleV3Scan;
    public Vector3 locomotionV3;
    public Vector3 locomotionV3Aux;
    public Vector3[]  quarentineProb;
    public Vector3[]  labortoryProb;
    public string[] names;
    public int cont;
    public ScannerEffectDemo sed;
    public int contagemObject = 0;
    public int contagemObjectAux = 0;
    public State state;
    public GameObject emptyGO;
    public Camera cam1;
    public Camera cam2;
    public Estado estado;
    public int labPosCount;
    public int quarPosCount;
    public Dictionary<string, string> EstadosDicionary;
    void Start()
    {   

        estado = new Estado();
        EstadosDicionary = new Dictionary<string, string>();
        quarentineProb = new  Vector3[6]; 
        quarentineProb[0] = new Vector3(-41.08f,5.8f,19.76f);
        quarentineProb[1] = new Vector3(-41.08f,5.8f,19.76f);
        quarentineProb[2] = new Vector3(-47.81f,5.80f,27.71f);
        quarentineProb[3] = new Vector3(-47.81f,5.80f,20.64f);
        quarentineProb[4] = new Vector3(-54.02f,5.80f,28.21f);
        quarentineProb[5] = new Vector3(-54.02f,5.80f,19.75f);
       
        labortoryProb = new  Vector3[6]; 
        labortoryProb[0] = new Vector3(-44.38f,5.80f,10.32f);
        labortoryProb[1] = new Vector3(-44.38f,5.80f,10.32f);
        labortoryProb[2] = new Vector3(-44.38f,5.80f,-1.75f);
        labortoryProb[3] = new Vector3(-60f,5.80f,0.28f);
        labortoryProb[4] = new Vector3(-53.09f,5.80f,9.69f);
        labortoryProb[5] = new Vector3(-61.04f,5.80f,9.69f);


        scanArray = new GameObject[5];
        scanArray[0] = GameObject.Find("NPC_0_Scan");
        scanArray[1] = Resources.Load<GameObject>("NPC_1_Scan");
        scanArray[2] = Resources.Load<GameObject>("NPC_2_Scan");
        scanArray[3] = Resources.Load<GameObject>("NPC_3_Scan");
        scanArray[4] = Resources.Load<GameObject>("NPC_4_Scan");
       
        maleV3Scan = new Vector3(-74.35f,6.13f,27.82f);
       
        sed = GameObject.Find("Main Camera").GetComponent<ScannerEffectDemo>();
        //scanArray[1].SetActive(false);
        //scanArray[2].SetActive(false);
        //scanArray[3].SetActive(false);
        //scanArray[4].SetActive(false);

        button = false;
        cont = 0;

        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Main Camera_2").GetComponent<Camera>();
        
        
       

        cam1.enabled = true;
        cam2.enabled = false;

        names = new string[5];

        names[0] = "NPC_0";
        names[1] = "NPC_1";
        names[2] = "NPC_2";
        names[3] = "NPC_3";
        names[4] = "NPC_4";


         EstadosDicionary.Add("NPC_0", "");
         EstadosDicionary.Add("NPC_1", "");
         EstadosDicionary.Add("NPC_2", "");
         EstadosDicionary.Add("NPC_3", "");
         EstadosDicionary.Add("NPC_4", "");


        //enum para o controle dos estados do personagem
        state = State.intro;
        //posições iniciais do destino e do personagem
        locomotionV3 = new Vector3(-74.79f,5.67f,12.06f);
        maleV3 = new Vector3(-84, 5, -2);
        //carregamento do personagem do file sys
        locomotion = Resources.Load<GameObject>("Locomotion");
        male = Resources.Load<GameObject>(names[0]);
        //procedimento para anexar um destino x a um personagem x
        male.name = "male_" + contagemObject.ToString() + "_";
        locomotion.name = "locomotion_" + contagemObject.ToString() + "_";
        Instantiate(locomotion, locomotionV3, transform.rotation);
        Instantiate(male, maleV3, transform.rotation);
        male.name = "male_" + contagemObject.ToString() + "_";
        locomotion.name = "locomotion_" + contagemObject.ToString() + "_";  
        contagemObject = contagemObject + 1;    
    }
    
    void Update()
    {    
      /* 
        foreach( KeyValuePair<string, string> kvp in EstadosDicionary)
        {
           // Debug.Log("Key = " +kvp.Key +" " + "Value: "+kvp.Value);
        }*/
        
        switch(cont){

        case 0: 
               // Debug.Log("CLASSE Global: Introdução");
              //  Debug.Log("CLASSE Global: Contagem CASE 0: " + cont); 
        break;
        case 1: 
              //  Debug.Log("CLASSE Global: Exame");
                // posição de destino da capsula de teste
                locomotionV3Aux = new Vector3(-77f,5.6f,26.17f);
                locomotionAux =  GameObject.Find("locomotion_" + (contagemObject-1) + "_(Clone)");
                locomotionAux.transform.position = locomotionV3Aux;  
             //   Debug.Log("CLASSE Global: Contagem CASE 1: " + cont);      
        break;
        case 2:
             // posição de destino do laboratorio
             estado.Personagem = "NPC_"+ (contagemObject-1).ToString();
             estado.Mapa = "LABORATORIO";
             GerarMapeamentoUser(estado, estado.Personagem); 
             switch(labPosCount){
              case 0:
                rankOrderer(labPosCount,false);
                break;
              case 1:
                rankOrderer(labPosCount,false);
                break;
              case 2:
                rankOrderer(labPosCount,false);
              break;
              case 3:
                rankOrderer(labPosCount,false);
              break;
              case 4:
                rankOrderer(labPosCount,false);
              break;
              case 5:
                rankOrderer(labPosCount,false);
              break;
              default:
           
              break;
            }
        break;
        case 3:
/*
                estado.Personagem = "NPC_"+ (contagemObject-1).ToString();
                estado.Mapa = "REPROVADO";
                GerarMapeamentoUser(estado, estado.Personagem);
*/
                if(contagemObject < 5){
                contagemObjectAux = contagemObject-1;
                //Debug.Log("CLASSE Global: novo individuo");
                male = GameObject.Find("male_" + contagemObjectAux + "_(Clone)");    
                
                //fazer o jogo de cameras, enviar oplayer de volta a recepção zerar os valores
                if(state.Equals(State.exit) )
                Destroy(male,5);

                //mudança aqui
                if(emptyGO.Equals(null)){
                emptyGO = new GameObject();
                sed.scan = emptyGO.transform;
                }

                Destroy(scanArray[contagemObjectAux]);
                sed.isNewChar = true;  
               // Debug.Log("TRUE");         
                male = Resources.Load<GameObject>(names[contagemObject]);
                male.name = "male_" + contagemObject.ToString() + "_";
                locomotion.name = "locomotion_" + contagemObject.ToString() + "_";
                scanArray[contagemObject] = Instantiate(scanArray[contagemObject], maleV3Scan, Quaternion.Euler(new Vector3(-3.668f,-95.948f,0f)));
                ///Debug.Log("CLASSE Global: position - " + maleV3);
                Instantiate(male, maleV3, transform.rotation);
                Instantiate(locomotion, locomotionV3, transform.rotation);
                male.name = "male_" + contagemObject.ToString() + "_";
                locomotion.name = "locomotion_" + contagemObject.ToString() + "_";
                //Debug.Log("CLASSE Global: Contagem CASE 3: " + cont); 
                contagemObject = contagemObject + 1;  
                cont = 0;        
                } 
                else{

                    //Vai pro fim do jogo
                    if(EstadosDicionary.Count == 5)
                    {
                      List<string> listMapa = EstadosDicionary.Values.ToList();
                      // DEFINIR CONDIÇÕES E TROCA DE SCENA ...
                      // OPÇOES SÃO (LABORATORIO, REPROVADO, QUARENTENA)
                      /*Debug.Log("1:  "+listMapa[0]);
                      Debug.Log("2:  "+listMapa[1]);
                      Debug.Log("3:  "+listMapa[2]);
                      Debug.Log("4:  "+listMapa[3]);
                      Debug.Log("5:  "+listMapa[4]);*/
                      // CENARIO CORRETO

                      if( 
                        (listMapa[0] == "LABORATORIO") 
                        && 
                        (listMapa[1] == "REPROVADO") 
                        && 
                        (listMapa[2] == "QUARENTENA")
                        && 
                        (listMapa[3] == "QUARENTENA")
                        && 
                        (listMapa[4] == "LABORATORIO") ){
                          // FINAL FELIZ ;)
                          StartCoroutine(LoadScena(true));
                      }
                      else{
                          StartCoroutine(LoadScena(false));
                         // FINAL TRISTE :(
                      }
                    }
                }     
        break;
        case 4:     
                //Debug.Log("CLASSE Global: saida");
                //posição de destino da saida
                
                estado.Personagem = "NPC_"+ (contagemObject-1).ToString();
                estado.Mapa = "REPROVADO";
                GerarMapeamentoUser(estado, estado.Personagem);

                locomotionV3Aux = new Vector3(-82.80f,5.0f,-3.18f);
                locomotionAux =  GameObject.Find("locomotion_" + (contagemObject-1) + "_(Clone)");
                locomotionAux.transform.position = locomotionV3Aux; 
                cont = 3;
                // Debug.Log("CLASSE Global: Contagem CASE 4: " + cont); 
                // SINALIZAR QUE MANDOU ALGUEM EMBORA, POR AQUI!
        break;
        case 5:     
            // posição de destino da quarentena
            estado.Personagem = "NPC_"+ (contagemObject-1).ToString();
            estado.Mapa = "QUARENTENA";
            GerarMapeamentoUser(estado, estado.Personagem); 
            switch(quarPosCount){
              case 0:
                rankOrderer(quarPosCount,true);
                break;
              case 1:
                rankOrderer(quarPosCount,true);
                break;
              case 2:
                rankOrderer(quarPosCount,true);
              break;
              case 3:
                rankOrderer(quarPosCount,true);
              break;
              case 4:
                rankOrderer(quarPosCount,true);
              break;
              case 5:
                rankOrderer(quarPosCount,true);
              break;
              default:
           
              break;
            }            
        break;
        default:
             //   Debug.Log(" CLASSE Global: DEFAULT: " + cont); 
             //   Debug.Log(" CLASSE Global: Erro no case!"); 
        break;
        }              
    }

    void rankOrderer(int i,bool decision){
        if(decision){
                locomotionV3Aux = quarentineProb[i];
                locomotionAux =  GameObject.Find("locomotion_" + (contagemObject-1) + "_(Clone)");
                locomotionAux.transform.position = locomotionV3Aux; 
            }else{
                locomotionV3Aux = labortoryProb[i];
                locomotionAux =  GameObject.Find("locomotion_" + (contagemObject-1) + "_(Clone)");
                locomotionAux.transform.position = locomotionV3Aux; 
            }
    }
     public void GerarMapeamentoUser(Estado estado, string contagemObject){
        EstadosDicionary[contagemObject] = estado.Mapa;
    }
    IEnumerator LoadScena(bool cenaFinal)
      {
          //Wait for 5 seconds
          yield return new WaitForSeconds(3);
          if(cenaFinal){
            SceneManager.LoadScene("FinalConcluido");
          }else{
            SceneManager.LoadScene("FinalFalho");
          }
      }
     IEnumerator WaitForSomething()
    { 
       
         IsReadyToDoCoroutine = false;
         yield return new WaitForSeconds(7f);
         IsReadyToDoCoroutine = true;     
    }

    void test()
    {
        button = true;
        //Debug.Log("TESTE");
    }    
}
