using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resumeGame : MonoBehaviour
{
    public GameObject pauseMenu;

    public void resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
