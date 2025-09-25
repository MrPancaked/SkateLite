using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject TrickMenu;
    [SerializeField] GameObject GrindMenu;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseMenu.SetActive(false);
        TrickMenu.SetActive(true);
        GrindMenu.SetActive(true);
    }

    private void Update()
    {
        if (inputManager.escapePressed)
        {
            if (!pauseMenu.activeInHierarchy) Pause();
            else Resume();
        }

        if (inputManager.tabPressed)
        {
            if (!TrickMenu.activeInHierarchy) TrickMenu.SetActive(true);
            else TrickMenu.SetActive(false);
            if (!GrindMenu.activeInHierarchy) GrindMenu.SetActive(true);
            else GrindMenu.SetActive(false);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        GrindMenu.SetActive(false);
        TrickMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        TrickMenu.SetActive(true);
        GrindMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void ChangeScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

   
}
