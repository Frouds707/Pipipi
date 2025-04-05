using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject deathMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 

        }
        else
        {
            Destroy(gameObject); 

        }

        if (deathMenu != null)
        {
            deathMenu.SetActive(false);
        }
    }
    public void ShowDeathMenu()
    {
        if (deathMenu != null)
        {
            deathMenu.SetActive(true);
        }
    }
    
    public void HideDeathMenu()
    {
        if (deathMenu != null)
        {
            deathMenu.SetActive(false);
        }
    }
}