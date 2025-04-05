using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDeathMenu : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("DebugDeathMenu: DeathMenu уничтожен!");
    }
}