using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridNode : MonoBehaviour
{
    private GameObject topLeft = null;
    private GameObject top = null;
    private GameObject topRight = null;
    private GameObject left = null;
    private GameObject bottomLeft = null;
    private GameObject bottom = null;
    private GameObject bottomRight = null;
    private GameObject right = null;
    private bool isClicked = false;
    private int neighborBombs = 0;
    private int neighborFlags = 0;
    private bool isBomb = false;
    private bool isFlagged = false;
    private int xCoor = -1;
    private int yCoor = -1;
    private GameManager gameManager;
    private GridManager gridManager;
    private BombCount bombCount;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("/Canvas/GameScene/Grid").GetComponent<GridManager>();
        bombCount = GameObject.Find("/Canvas/GameScene/BombCount").GetComponent<BombCount>();
    }

    public void setGridNode(GameObject[][] grid, int x, int y, int xMax, int yMax)
    {
        xCoor = x;
        yCoor = y;
        if (x > 0)
        {
            this.top = grid[x - 1][y];
            if (y > 0)
            {
                this.topLeft = grid[x - 1][y - 1];
                this.left = grid[x][y - 1];
            }
            if (y + 1 < yMax)
            {
                this.topRight = grid[x - 1][y + 1];
                this.right = grid[x][y + 1];
            }
        }
        if (x + 1 < xMax)
        {
            this.bottom = grid[x + 1][y];
            if (y > 0)
            {
                this.bottomLeft = grid[x + 1][y - 1];
                this.left = grid[x][y - 1];
            }
            if (y + 1 < yMax)
            {
                this.bottomRight = grid[x + 1][y + 1];
                this.right = grid[x][y + 1];
            }
        }
    }

    public void onClick()
    {
        if (gameManager.gameOver)
        {
            return;
        }
        if (this.isBomb && !gameManager.flagMode && !this.isFlagged)
        {
            gameManager.gameOver = true;
            return;
        }
        else if (this.isClicked && gameManager.flagMode && this.neighborBombs == this.neighborFlags)
        {
            this.clickNumRecursion();
        }
        else if (this.isClicked && gameManager.flagMode)
        {
            return;
        }
        else if (gameManager.flagMode)
        {
            if (this.isFlagged)
            {
                gameObject.GetComponent<Image>().color = Color.white;
                updateNeighborFlags(-1);
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.green;
                updateNeighborFlags(1);
            }
            this.setIsFlagged(!this.isFlagged);
        }
        else
        {
            if (!gameManager.firstMove)
            {
                gridManager.startGame(xCoor, yCoor);
                gameManager.firstMove = true;
            }
            this.clickRecursion();
        }
        gridManager.checkGameEnd();
    }

    public void clickNumRecursion()
    {
        if (this.isBomb)
        {
            gameManager.gameOver = true;
            return;
        }
        if (this.neighborBombs != 0)
        {
            gameObject.GetComponentsInChildren<Text>()[0].text = this.neighborBombs.ToString();
            gameObject.GetComponentsInChildren<Text>()[0].color = gridManager.getNeighborBombsColors(this.neighborBombs - 1);
        }
        if (this.topLeft && this.topLeft.GetComponent<GridNode>().isBomb && !this.topLeft.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("topLeft");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.topLeft.GetComponent<GridNode>().xCoor.ToString() + " , " + this.topLeft.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.top && this.top.GetComponent<GridNode>().isBomb && !this.top.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("top");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.top.GetComponent<GridNode>().xCoor.ToString() + " , " + this.top.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.topRight && this.topRight.GetComponent<GridNode>().isBomb && !this.topRight.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("topRight");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.topRight.GetComponent<GridNode>().xCoor.ToString() + " , " + this.topRight.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.left && this.left.GetComponent<GridNode>().isBomb && !this.left.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("left");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.left.GetComponent<GridNode>().xCoor.ToString() + " , " + this.left.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.bottomLeft && this.bottomLeft.GetComponent<GridNode>().isBomb && !this.bottomLeft.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("bottomLeft");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.bottomLeft.GetComponent<GridNode>().xCoor.ToString() + " , " + this.bottomLeft.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.bottom && this.bottom.GetComponent<GridNode>().isBomb && !this.bottom.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("bottom");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.bottom.GetComponent<GridNode>().xCoor.ToString() + " , " + this.bottom.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.bottomRight && this.bottomRight.GetComponent<GridNode>().isBomb && !this.bottomRight.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("bottomRight");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.bottomRight.GetComponent<GridNode>().xCoor.ToString() + " , " + this.bottomRight.GetComponent<GridNode>().yCoor.ToString());
            return;
        }
        if (this.right && this.right.GetComponent<GridNode>().isBomb && !this.right.GetComponent<GridNode>().isFlagged)
        {
            gameManager.gameOver = true;
            Debug.Log("right");
            Debug.Log(this.xCoor.ToString() + " , " + this.yCoor.ToString());
            Debug.Log(this.right.GetComponent<GridNode>().xCoor.ToString() + " , " + this.right.GetComponent<GridNode>().yCoor.ToString());
            return;
        }

        if (gameManager.gameOver)
        {
            return;
        }

        if (this.topLeft && !this.topLeft.GetComponent<GridNode>().isClicked)
        {
            this.topLeft.GetComponent<GridNode>().clickRecursion();
        }
        if (this.top && !this.top.GetComponent<GridNode>().isClicked)
        {
            this.top.GetComponent<GridNode>().clickRecursion();
        }
        if (this.topRight && !this.topRight.GetComponent<GridNode>().isClicked)
        {
            this.topRight.GetComponent<GridNode>().clickRecursion();
        }
        if (this.left && !this.left.GetComponent<GridNode>().isClicked)
        {
            this.left.GetComponent<GridNode>().clickRecursion();
        }
        if (this.bottomLeft && !this.bottomLeft.GetComponent<GridNode>().isClicked)
        {
            this.bottomLeft.GetComponent<GridNode>().clickRecursion();
        }
        if (this.bottom && !this.bottom.GetComponent<GridNode>().isClicked)
        {
            this.bottom.GetComponent<GridNode>().clickRecursion();
        }
        if (this.bottomRight && !this.bottomRight.GetComponent<GridNode>().isClicked)
        {
            this.bottomRight.GetComponent<GridNode>().clickRecursion();
        }
        if (this.right && !this.right.GetComponent<GridNode>().isClicked)
        {
            this.right.GetComponent<GridNode>().clickRecursion();
        }
    }

    public void clickRecursion()
    {
        if (this.isClicked || this.isBomb || this.isFlagged) {
            return;
        }
        this.setIsClicked(true);
        gameObject.GetComponent<Image>().color = Color.gray;
        if (this.neighborBombs != 0)
        {
            gameObject.GetComponentsInChildren<Text>()[0].text = this.neighborBombs.ToString();
            gameObject.GetComponentsInChildren<Text>()[0].color = gridManager.getNeighborBombsColors(this.neighborBombs - 1);
        }
        if (this.neighborBombs == 0)
        {
            if (this.topLeft)
            {
                this.topLeft.GetComponent<GridNode>().clickRecursion();
            }
            if (this.top)
            {
                this.top.GetComponent<GridNode>().clickRecursion();
            }
            if (this.topRight)
            {
                this.topRight.GetComponent<GridNode>().clickRecursion();
            }
            if (this.left)
            {
                this.left.GetComponent<GridNode>().clickRecursion();
            }
            if (this.bottomLeft)
            {
                this.bottomLeft.GetComponent<GridNode>().clickRecursion();
            }
            if (this.bottom)
            {
                this.bottom.GetComponent<GridNode>().clickRecursion();
            }
            if (this.bottomRight)
            {
                this.bottomRight.GetComponent<GridNode>().clickRecursion();
            }
            if (this.right)
            {
                this.right.GetComponent<GridNode>().clickRecursion();
            }
        }
    }

    public void updateNeighborBombs()
    {
        if (this.topLeft && !this.topLeft.GetComponent<GridNode>().isBomb)
        {
            this.topLeft.GetComponent<GridNode>().setNeighborBombs(this.topLeft.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.top && !this.top.GetComponent<GridNode>().isBomb)
        {
            this.top.GetComponent<GridNode>().setNeighborBombs(this.top.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.topRight && !this.topRight.GetComponent<GridNode>().isBomb)
        {
            this.topRight.GetComponent<GridNode>().setNeighborBombs(this.topRight.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.left && !this.left.GetComponent<GridNode>().isBomb)
        {
            this.left.GetComponent<GridNode>().setNeighborBombs(this.left.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.bottomLeft && !this.bottomLeft.GetComponent<GridNode>().isBomb)
        {
            this.bottomLeft.GetComponent<GridNode>().setNeighborBombs(this.bottomLeft.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.bottom && !this.bottom.GetComponent<GridNode>().isBomb)
        {
            this.bottom.GetComponent<GridNode>().setNeighborBombs(this.bottom.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.bottomRight && !this.bottomRight.GetComponent<GridNode>().isBomb)
        {
            this.bottomRight.GetComponent<GridNode>().setNeighborBombs(this.bottomRight.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
        if (this.right && !this.right.GetComponent<GridNode>().isBomb)
        {
            this.right.GetComponent<GridNode>().setNeighborBombs(this.right.GetComponent<GridNode>().getNeighborBombs() + 1);
        }
    }

    public void updateNeighborFlags(int flagIncrement)
    {
        if (this.topLeft)
        {
            this.topLeft.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.top)
        {
            this.top.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.topRight)
        {
            this.topRight.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.left)
        {
            this.left.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.bottomLeft)
        {
            this.bottomLeft.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.bottom)
        {
            this.bottom.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.bottomRight)
        {
            this.bottomRight.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        if (this.right)
        {
            this.right.GetComponent<GridNode>().neighborFlags += flagIncrement;
        }
        bombCount.bombsLeft -= flagIncrement;
    }

    public void resetNode()
    {
        this.isClicked = false;
        this.neighborBombs = 0;
        this.neighborFlags = 0;
        this.isBomb = false;
        this.isFlagged = false;
        gameObject.GetComponent<Image>().color = Color.white;
        gameObject.GetComponentsInChildren<Text>()[0].text = "";
    }

    public bool getIsClicked()
    {
        return this.isClicked;
    }

    public void setIsClicked(bool clicked)
    {
        this.isClicked = clicked;
    }

    public bool getIsBomb()
    {
        return this.isBomb;
    }

    public void setIsBomb(bool bomb)
    {
        this.isBomb = bomb;
    }

    public void setIsFlagged(bool flagged)
    {
        this.isFlagged = flagged;
    }

    public int getNeighborBombs()
    {
        return this.neighborBombs;
    }

    public void setNeighborBombs(int neighborBombs)
    {
        this.neighborBombs = neighborBombs;
    }
}