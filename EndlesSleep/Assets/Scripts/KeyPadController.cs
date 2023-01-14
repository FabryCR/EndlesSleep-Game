using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPadController : MonoBehaviour
{
    string correctCode = "56444081"; //L=54+2=56 U=42+2=44 C=38+2=40 Y=79+2=81 = 56-44-40-81

    [SerializeField]
    TMP_Text screenText;

    [SerializeField]
    GameObject door;

    string finalComb;

    public void TypeNum(int num)
    {
        finalComb += num.ToString();
        screenText.text = finalComb;
    }

    public void EnterCombination()
    {
        if (finalComb == correctCode)
        {
            door.GetComponent<DoorController>().OpenDoor();
        }
        else
        {
            finalComb = "";
            screenText.text = "CODIGO INCORRECTO";
        }
    }

    public void Cancel()
    {
        finalComb = "";
        screenText.text = "";
        door.GetComponent<DoorController>().DisableKeyPad();
    }
}
