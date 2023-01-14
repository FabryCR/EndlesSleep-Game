using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameObject.FindWithTag("Enemy") == null)
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
