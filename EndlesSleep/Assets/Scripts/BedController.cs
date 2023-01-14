using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedController : MonoBehaviour
{
    public void Sleep()
    {
        SceneManager.LoadScene(2);
    }
}
