using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dunGen : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false; //to do change to 5 bit binary number
        public bool[] status = new bool[4];
    }

    public Vector2 size;
    public int startPos = 0;
    List<Cell> board;
    public GameObject room;
    public Vector2 offset;

    private void Start()
    {
        mazeGen();
    }

    void genDun()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + y * size.x)];
                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -y * offset.y), Quaternion.identity, transform).GetComponent<RoomGen>();
                    newRoom.updateRoom(currentCell.status);
                }
            }
        }
    }



    void mazeGen()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int y = 0; y < size.y; y++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 1000)
        {
            if (currentCell == board.Count - 1)
            {
                break;
            }

            k++;
            board[currentCell].visited = true;
            List<int> neighbors = DFScheck(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell == currentCell)//down or right
                {
                    if (newCell-1 == currentCell)//right
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else //down
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else //up or left
                {
                    if (newCell + 1 == currentCell)//left
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else //up
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        genDun();
    }

    List<int> DFScheck(int cell)
    {
        List<int> neighbors = new List<int>();
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
}
