using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[][] grid;
    public BombCount bombCount;
    public Canvas canvas;
    public int xMax;
    public int yMax;
    public int numBombs;
    public int squareSize;
    public GameObject gridNode;
    public int[] bombCoords;
    public int bombRadius;
    public int flagsLeft;

    private Color[] neighborBombsColors = {
        new Color(0f, 0.1137255f, 0.9294118f),
        new Color(0.2078432f, 0.4745098f, 0.1294118f),
        new Color(0.7041177f, 0.1921569f, 0.1333333f),
        new Color(0f, 0f, 0.4588236f),
        new Color(0.5215687f, 0f, 0f),
        new Color(0.003921569f, 0.482353f, 0.5019608f),
        Color.black,
        new Color(0.4980392f, 0.4980392f, 0.4980392f)
    };

    // Start is called before the first frame update
    public void setupGrid()
    {
        bombCount.bombsLeft = numBombs;
        grid = new GameObject[xMax][];
        for (int i = 0; i < xMax; i += 1) 
        { 
	        grid[i] = new GameObject[yMax];
        }
        int gridBuffer = ((int)canvas.GetComponent<RectTransform>().rect.width - (squareSize * xMax)) / 2;
        for (int i = 0; i < xMax; i += 1)
        {
            for (int j = 0; j < yMax; j += 1)
            {
                grid[i][j] = Instantiate(gridNode, new Vector3(i * squareSize + (squareSize / 2) + gridBuffer, j * squareSize + (squareSize / 2), 0), Quaternion.identity);
                grid[i][j].GetComponent<RectTransform>().sizeDelta = new Vector2(squareSize, squareSize);
                grid[i][j].transform.SetParent(gameObject.transform);
            }
        }
        for (int i = 0; i < xMax; i += 1)
        {
            for (int j = 0; j < yMax; j += 1)
            {
                grid[i][j].GetComponent<GridNode>().setGridNode(grid, i, j, xMax, yMax);
            }
        }
        //startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(int startRow, int startCol)
    {
        bombCount.bombsLeft = numBombs;
        if (numBombs > xMax * yMax)
        {
            return;
        }
        List<int> randBombsList = new List<int>();
        int[] randBombs = new int[] {};
        int randIndex = 0;
        for (int i = 0; i < numBombs; i += 1)
        {
            bool notValid = true;
            while(notValid)
            {
                randIndex = Random.Range(0, xMax * yMax);
                notValid = false;
                if ((startRow - (randIndex % xMax) >= -bombRadius && startRow - (randIndex % xMax) <= bombRadius) && (startCol - (randIndex / xMax) >= -bombRadius && startCol - (randIndex / xMax) <= bombRadius))
                {
                    notValid = true;
                }
                else
                {
                    foreach (int j in randBombsList)
                    {
                        if (j == randIndex)
                        {
                            notValid = true;
                            break;
                        }
                    }
                }
            }
            randBombsList.Add(randIndex);
        }
        randBombs = randBombsList.ToArray();
        bombCoords = randBombs;
        for (int i = 0; i < randBombs.Length; i += 1)
        {
            int row = randBombs[i] % xMax;
            int col = randBombs[i] / xMax;
            grid[row][col].GetComponent<GridNode>().setIsBomb(true);
            grid[row][col].GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < randBombs.Length; i += 1)
        {
            int row = randBombs[i] % xMax;
            int col = randBombs[i] / xMax;
            grid[row][col].GetComponent<GridNode>().updateNeighborBombs();
        }
    }

    public void resetGame()
    {
        for (int i = 0; i < xMax; i += 1)
        {
            for (int j = 0; j < yMax; j += 1)
            {
                grid[i][j].GetComponent<GridNode>().resetNode();
            }
        }
    }

    public void checkGameEnd()
    {
        for (int i = 0; i < xMax; i += 1)
        {
            for (int j = 0; j < yMax; j += 1)
            {
                if (!grid[i][j].GetComponent<GridNode>().getIsClicked() && !grid[i][j].GetComponent<GridNode>().getIsBomb())
                {
                    return;
                }
            }
        }
        Debug.Log("You Won");
        gameManager.gameOver = true;
    }

    public Color getNeighborBombsColors(int index)
    {
        return neighborBombsColors[index];
    }
}