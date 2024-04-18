using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PauseMenuScript : MonoBehaviour
{
    private GameObject _pauseMenu;
    private GameObject _youSureMenu;
    private GameObject _pause;
    private RectTransform[] _pauseFamily;
    private RectTransform[] _youSureFamily;

    public void Start()
    {
        _pauseMenu = GameObject.Find("PauseMenu");
        _pause = GameObject.Find("Pause");
        _youSureMenu = GameObject.Find("YouSure");
        _youSureMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        _pauseFamily = _pause.GetComponentsInChildren<RectTransform>();
        _youSureFamily = _youSureMenu.GetComponentsInChildren<RectTransform>();
        Cursor.visible = false;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        _pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    public void YouSure()
    {
        _youSureMenu.SetActive(true);
        _pause.SetActive(false);
    }

    public void NoNotSure()
    {
        _youSureMenu.SetActive(false);
        _pause.SetActive(true);
    }
}