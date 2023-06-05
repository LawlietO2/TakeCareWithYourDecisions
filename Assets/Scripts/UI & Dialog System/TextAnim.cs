using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class TextAnim : MonoBehaviour
{
    public UIText uitext;
    public Text diseaseName;
    // Start is called before the first frame update
    void Start()
    {
        //diseaseName = GameObject.Find("Text pe").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
    switch(gameObject.name){
                case "Panel cabeca":
                diseaseName.text = uitext.diseaseName[0];           
                break;
                case "Panel torso":
                diseaseName.text = uitext.diseaseName[1];    
                break;
                case "Panel perna":
                diseaseName.text = uitext.diseaseName[2];
                break;
                case "Panel pe":
                diseaseName.text = uitext.diseaseName[3];
                break;
                //default:Debug.Log("erro");
                break;
			}
    }
}
