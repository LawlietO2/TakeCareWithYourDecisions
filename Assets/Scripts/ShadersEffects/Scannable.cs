using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour
{
	public Animator UIAnim;
    public Transform pos;
	public int id;
	string name;
	GameObject panel; 

	void Start(){
		pos = transform;
		//Debug.Log("NAME: " + name + "  " + pos);
		id = 9;
	}

	public Scannable(){
		
	}

    void OnMouseOver()
    {
      panel =  gameObject.GetComponent<GameObject>();
      panel.SetActive( false );
      //Debug.Log("TO EM CIMA DO :" + gameObject.name);
    }


	public void Ping()
	{
		if(this.enabled){
			name = gameObject.name;
			//Debug.Log("NAME: " + name);
			switch(name)	{
				case "Head":
				UIAnim = GameObject.Find("iso ping cabeca").GetComponent<Animator>();
				id = 0;
				break;
				case "Torso":
				UIAnim = GameObject.Find("iso ping torso").GetComponent<Animator>();
				id = 1;
				break;
				case "Pants":
				UIAnim = GameObject.Find("iso ping perna").GetComponent<Animator>();
				id = 2;
				break;
				case "Shorts":
				UIAnim = GameObject.Find("iso ping pe").GetComponent<Animator>();
				id = 3;
				break;
				default:Debug.Log("erro");
				break;
			}		
		}
		UIAnim.SetTrigger("Ping");
    }
	public void invPing(){
		UIAnim.SetTrigger("InvPing");
	}
}
