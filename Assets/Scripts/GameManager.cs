using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 

            if (UIManager.instance != null)
            {
                DontDestroyOnLoad(UIManager.instance.gameObject); 
            }
        }
        else
        {
            Destroy(gameObject); 
           
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Сцена загружена");
    }

    public void ShowDeathMenu()
    {
     
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowDeathMenu();
        }
       
    }

    public void HideDeathMenu()
    {
        
        if (UIManager.instance != null)
        {
            UIManager.instance.HideDeathMenu();
        }
       
    }

    public void RestartGame()
    {
        HideDeathMenu(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}