using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUtil : MonoBehaviour
{
    public GameOverMenu gameOverMenu;
    public void DestroyHelper()
    {
        Destroy(gameObject);
        gameOverMenu.GameOver();
    }
}
