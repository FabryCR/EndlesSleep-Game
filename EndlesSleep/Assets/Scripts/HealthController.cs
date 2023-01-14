using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    [SerializeField]
    float maxHealth = 100;

    [SerializeField]
    float health;

    [SerializeField]
    float maxLives = 3;

    [SerializeField]
    float lives;

    [SerializeField]
    Slider healthBarSlider;

    [SerializeField]
    TMP_Text lifeTxt;

    [SerializeField]
    GameObject panelBlackout;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Transform enemyTrigger1, enemyTrigger2;

    GameObject activeEnemy;

    Vector3 spawnPoint;
    bool playerIsActive = true;

    void Start()
    {
        spawnPoint = transform.position;

        health = maxHealth;
        lives = maxLives;

        lifeTxt.text = "x" + maxLives.ToString();
        healthBarSlider.maxValue = maxHealth;

    }

    public void TakeDamage(float damage)
    {
        if (playerIsActive)
        {
            health -= damage;

            healthBarSlider.value = health;

            if (health <= 0)
            {
                Dies();
            }
        }
    }

    void Dies()
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene(3);
        }

        //Respawn
        lives--;
        lifeTxt.text = "x" + lives.ToString();
        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn()
    {

        //Panel start animation

        panelBlackout.SetActive(true);
        animator.SetTrigger("Blackout");

        playerIsActive = false;
        transform.GetComponentInChildren<PlayerCamController>().LockCamControls();
        transform.GetComponent<PlayerController>().allowMove = false;

        yield return new WaitForSeconds(3f);

        //Relocate in spawn

        activeEnemy = GameObject.FindWithTag("Enemy");
        if (activeEnemy != null)
        {
            Destroy(activeEnemy);
        }

        enemyTrigger1.GetComponent<BoxCollider>().enabled = true;
        enemyTrigger2.GetComponent<BoxCollider>().enabled = true;

        transform.GetComponentInChildren<PlayerCamController>().UnlockCamControls();
        transform.GetComponent<PlayerController>().allowMove = true;
        playerIsActive = true;

        health = maxHealth;
        healthBarSlider.value = health;

        transform.position = spawnPoint;
        panelBlackout.SetActive(false);

    }
}
