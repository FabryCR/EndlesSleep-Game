using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartLevelController : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI textDisplay;

    //[SerializeField]
    TextMeshProUGUI objectiveMission;

    [SerializeField]
    string[] sentences;

    [SerializeField]
    float timeBetweenType;

    [SerializeField]
    float timeBetweenSentence;


    private int index;


    void Start()
    {
        StartCoroutine(Type());
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

