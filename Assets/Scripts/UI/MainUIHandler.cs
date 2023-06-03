using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    public void ReturnToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
