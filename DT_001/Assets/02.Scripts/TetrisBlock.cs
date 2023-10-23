using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using Unity.Burst.CompilerServices;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 1.0f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    public List<Transform> TileList = new List<Transform>();
    public GameObject Tileparent;

    

    
    public TowerSpawner towerSpawner;
    private void Awake()
    {
        Tileparent = GameObject.Find("TilemapTiles");
        towerSpawner = GameObject.Find("TowerSpawner").GetComponent<TowerSpawner>();
        
    }


    void Start()
    {
        TileList = Tileparent.GetComponentsInChildren<Transform>().ToList();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove() )
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }


        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if(!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))

        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines(); //���� ��ϵ�� ��á���� Ȯ���Ѵ�.
                this.enabled = false;
                FindObjectOfType<spawnertetris>().NewTetris();
            }
            previousTime = Time.time;
        }

    }



    void CheckForLines()
    {
        for(int i=height-1; i>=0; i--) //��Ʈ���� ����� �� ���ٺ��� �Ʒ����� �˻��Ѵ�.
        {
            if(i == height - 1)
            {
                if (CkeckEnd(i))
                {
                    Debug.Log("Gameover!");
                    towerSpawner.GameOver.gameObject.SetActive(true);
                }
            }
            if (HasLine(i)) //���� ������� �� ������ ���
            {
                DeleteLine(i); // �� ���� �����ϰ�
                RowDown(i); //���� ��ĭ ������.
                
                towerSpawner.SpawnTower(TileList[towerSpawner.Index]);
                towerSpawner.Index ++;

                
            }
        }
    }

    bool HasLine(int i) //���� ������� �����ִ��� Ȯ���ϱ�
    {
        for (int j = 0; j < width; j++) // ���� 0~9���� �˻��Ѵ�.
        {
            if (grid[j, i] == null) //����ִٸ�
                return false;       //false�� �����Ѵ�.
        }
        return true; //���� �� �� �ִٸ� true�� �����Ѵ�.
    }

    bool CkeckEnd(int i) //���� ������� �����ִ��� Ȯ���ϱ�
    {
        for (int j = 0; j < width; j++) // ���� 0~9���� �˻��Ѵ�.
        {
            if (grid[j, i] != null) //����ִٸ�
                return true;       //false�� �����Ѵ�.
        }
        return false; //���� �� �� �ִٸ� true�� �����Ѵ�.
    }

    void DeleteLine(int i) //���� �����Ѵ�.
    {
        for(int j=0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) // ���� �Ʒ��� ������.
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if (grid[j,y] != null) // ������ �Ʒ��ٷ� �����ϴ� �����̴�.
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }





    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundX, roundY] = children;
        }
    }
    

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }

            if (grid[roundX, roundY] != null)
                return false;

        }

        return true;

    }
}
