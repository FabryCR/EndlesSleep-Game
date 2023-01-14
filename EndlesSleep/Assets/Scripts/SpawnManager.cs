using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject ghoulPrefab;

    [SerializeField]
    Transform spawnPosition;    
    
    [SerializeField]
    float enemyLifeTime = 20f;

    [SerializeField]
    AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.pitch = -1.16f;
            Debug.Log("SPAWN");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            var instance = Instantiate(ghoulPrefab, spawnPosition.position, Quaternion.identity);
            Destroy(instance, enemyLifeTime);
        }
    }
}
