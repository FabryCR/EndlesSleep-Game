using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    GameObject keyPad;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    Animator animator;

    public void OpenDoor()
    {
        animator.SetTrigger("DoorOpens");
        keyPad.SetActive(false);
        cam.GetComponent<PlayerCamController>().UnlockCamControls();
        cam.GetComponentInParent<PlayerController>().allowMove = true;
        gameObject.tag = "OpenDoor";
    }

    public void DisableKeyPad()
    {
        keyPad.SetActive(false);
        cam.GetComponent<PlayerCamController>().UnlockCamControls();
        cam.GetComponentInParent<PlayerController>().allowMove = true;
    }

    public void EnableKeyPad()
    {
        keyPad.SetActive(true);
    }
}
