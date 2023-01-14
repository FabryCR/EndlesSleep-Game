using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DIalogueController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textDisplay;

    [SerializeField]
    string[] sentences;

    [SerializeField]
    float timeBetweenType;

    [SerializeField]
    float timeBetweenSentence;

    Collider playerChild;

    private int index;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerChild = other;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            playerChild.gameObject.GetComponentInParent<PlayerController>().allowMove = false;
            NextSentence();
        }
    }

    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            playerChild.gameObject.GetComponentInParent<PlayerController>().allowMove = true;
            textDisplay.text = "";
        }
    }

    public IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(timeBetweenType);
        }
        yield return new WaitForSeconds(timeBetweenSentence);
        NextSentence();
    }
}