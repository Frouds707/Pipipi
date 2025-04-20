using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject deathMenu; 
    [SerializeField] private PlayerHUD playerHUD; 

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

        if (playerHUD != null)
        {
            playerHUD.gameObject.SetActive(true);
        }
    }

    public void ShowDeathMenu()
    {
        if (deathMenu != null)
        {
            deathMenu.SetActive(true);
        }
        if (playerHUD != null)
        {
            playerHUD.gameObject.SetActive(false);
        }
    }

    public void HideDeathMenu()
    {
        if (deathMenu != null)
        {
            deathMenu.SetActive(false);
        }
        if (playerHUD != null)
        {
            playerHUD.gameObject.SetActive(true);
        }
    }
}