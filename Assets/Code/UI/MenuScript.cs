using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    private GameObject _controlls;
    private GameObject _startOverlay;
    public void Start()
    {
        _controlls =GameObject.Find("Controlls");
        _startOverlay = GameObject.Find("StartOverlay");
        _controlls.SetActive(false);
        
    }

    public void ShowInstructions()
    {
        _startOverlay.SetActive(false);
        _controlls.SetActive(true);
    }

    public void PlayGame()
    {
        Debug.Log("i start");
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
