using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void loadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
