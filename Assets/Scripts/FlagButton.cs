using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagButton : MonoBehaviour
{
    public GameManager gameManager;
    
    public void onClick()
    {
        gameManager.flagMode = !gameManager.flagMode;
        Debug.Log(gameManager.flagMode);
        gameObject.GetComponent<Image>().color = gameManager.flagMode ? Color.gray : Color.white;
    }
}
