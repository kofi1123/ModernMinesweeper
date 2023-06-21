using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;

    public GameObject mainMenu;
    public GameObject gameScene;
    public bool flagMode = false;
    public bool gameOver = false;
    public bool bombsRevealed = false;
    public bool firstMove = false;
    public bool gameActive = false;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("/Canvas/GameScene/Grid").GetComponent<GridManager>();
        setGameScene(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && !bombsRevealed)
        {
            bombsRevealed = true;
            for (int i = 0; i < gridManager.bombCoords.Length; i += 1)
            {
                int row = gridManager.bombCoords[i] % gridManager.xMax;
                int col = gridManager.bombCoords[i] / gridManager.xMax;
                if (gridManager.grid[row][col].GetComponent<Image>().color != Color.green)
                {
                    gridManager.grid[row][col].GetComponent<Image>().color = Color.blue;
                }
            }
        }
    }

    public void resetGame()
    {
        this.gameOver = false;
        this.bombsRevealed = false;
        this.firstMove = false;
    }

    public void setMainMenu(bool active)
    {
        mainMenu.SetActive(active);
    }

    public void setGameScene(bool active)
    {
        gameScene.SetActive(active);
    }

}
