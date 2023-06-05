using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationStatusManager : MonoBehaviour
{

    public bool Blue = false;
    public bool Red = false;
    public bool Magenta = false;
    public bool isInScanningRoom = false;
    
    public void OnOffBlue(){
        Blue = !Blue;
    }

    public void OnOffRed(){
        Red = !Red;
    }

    public void OnOffMagenta(){
        Magenta = !Magenta;
    }
}
