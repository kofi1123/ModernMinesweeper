using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameManager gameManager;
    public GridManager gridManager;

    public void onClick()
    {
        gameManager.resetGame();
        gridManager.resetGame();
    }
}
