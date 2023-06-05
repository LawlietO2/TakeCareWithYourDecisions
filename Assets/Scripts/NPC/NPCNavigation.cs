using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCNavigation : MonoBehaviour
{
    public GameObject theDestination;
    public GameObject male;
    string[] valor;
    NavMeshAgent theAgent;
    // Start is called before the first frame update
    void Start()
    {     
         male = this.gameObject;
         valor = male.name.Split(char.Parse("_"));
         //Debug.Log("Valor: " + valor[1]);

         theAgent = GetComponent<NavMeshAgent>();
         theDestination = GameObject.Find("locomotion_" + valor[1] + "_(Clone)");
    }

    void Update()
    {   
        theAgent.SetDestination(theDestination.transform.position);
    }

}
