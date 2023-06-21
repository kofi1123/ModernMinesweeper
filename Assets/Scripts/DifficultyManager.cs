using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button customButton;
    public GridManager gridManager;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        easyButton.onClick.AddListener(onClickEasyButton);
        mediumButton.onClick.AddListener(onClickMediumButton);
        hardButton.onClick.AddListener(onClickHardButton);
        customButton.onClick.AddListener(onClickCustomButton);
    }

    private void onClickEasyButton()
    {
        Debug.Log("easyButton Clicked");
        gridManager.xMax = 9;
        gridManager.yMax = 9;
        gridManager.numBombs = 10;
        initiateGame();
    }

    private void onClickMediumButton()
    {
        Debug.Log("mediumButton Clicked");
        gridManager.xMax = 16;
        gridManager.yMax = 16;
        gridManager.numBombs = 40;
        initiateGame();
    }

    private void onClickHardButton()
    {
        Debug.Log("hardButton Clicked");
        gridManager.xMax = 30;
        gridManager.yMax = 16;
        gridManager.numBombs = 99;
        initiateGame();
    }

    private void onClickCustomButton()
    {
        Debug.Log("customButton Clicked");
        initiateGame();
    }

    private void initiateGame()
    {
        Debug.Log("intiating game");
        gameManager.setGameScene(true);
        gridManager.setupGrid();
        gameManager.setMainMenu(false);
    }
}
