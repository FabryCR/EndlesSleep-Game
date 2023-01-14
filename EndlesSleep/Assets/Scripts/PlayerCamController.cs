using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;

    public Transform orientation;
    public Camera cam;

    bool allowCamMove = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (allowCamMove)
        {
            MoveCamera();
        }

        if (Input.GetMouseButtonDown(0))
        {
            CameraSelector();
        }
    }


    public void LockCamControls()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        allowCamMove = false;
    }

    public void UnlockCamControls()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        allowCamMove = true;
    }

    void CameraSelector()
    {
        //Camera Selector
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider.CompareTag("KeyPadDoor"))
        {
            float distance = Vector3.Distance(this.transform.position, hitInfo.collider.GetComponent<Transform>().position);

            if (distance <= 8f)
            {
                LockCamControls();
                cam.GetComponentInParent<PlayerController>().allowMove = false;
                hitInfo.collider.gameObject.GetComponent<DoorController>().EnableKeyPad();
            }
        }
        else if (hitInfo.collider.CompareTag("Bed"))
        {
            float distance = Vector3.Distance(this.transform.position, hitInfo.collider.GetComponent<Transform>().position);

            if (distance <= 8f)
            {
                hitInfo.collider.gameObject.GetComponent<BedController>().Sleep();
            }
        }
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
