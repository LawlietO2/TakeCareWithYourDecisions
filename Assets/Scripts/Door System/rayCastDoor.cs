using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCastDoor : MonoBehaviour
{
    GameObject doorHandler;
    DoorHandler dh;
    string name;
    int id;
    // Start is called before the first frame update
    void Start()
    {  
           doorHandler = GameObject.Find("doorHandler"); 
           dh = doorHandler.GetComponent<DoorHandler>();
    }

    // Update is called once per frame
    void Update()
    {
           
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward),Color.red);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, 3.5f) )
            {
                if(hit.transform.gameObject.tag == "Porta"){
                name =  hit.transform.gameObject.name;
                //Debug.Log("Porta");
                string ids = "";

                string[] splitArray =  name.Split(char.Parse("_"));
                ids = splitArray[1];
                id = int.Parse(ids);
                /*
                    foreach(string s in splitArray)
                    {
                        Debug.Log("AQUI [" + s +"]");
                    }
                */
                //Debug.Log("AQUI [" + id +"]");
   

                //Debug.Log("Porta");
                /*switch(name)	{
                        case "frontDoor_0":
                        id = 0;
                        break;
                        case "labDoor_1":
                        id = 1;
                        break;
                        case "labInsideDoor_2":
                        id = 2;
                        break;
                        case "quarantineDoor_3":
                        id = 3;
                        break;
                        case "glassDoor_4":
                        id = 4;
                        break;
                        default:Debug.Log("erro");
                        break;
                    }*/
                    dh.hit_Door = true;
                    dh.id = id;
                    dh.open_Door = true;

                 }
               }
         

    }
}
