using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    Transform hands;

    [SerializeField]
    Transform flashlight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flashlight.gameObject.SetActive(true);
            hands.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
