using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //Adding Rows and Columns
    public int Columns = 8;
    public int Rows = 8;

    //Adding Floor Tiles
    public GameObject [] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject[] FoodTiles;
    public GameObject[] EnemyTiles;

    private List <Vector2> gridPos = new List <Vector2>();
    private Transform boardParent;

    //Initilizes the grid position
    void InitilizeGridPos()
    {
        // Clears the grid postitions
        gridPos.Clear();

        for(int x = 1; x < Columns; x++)
        {
            for(int y = 1; y < Rows; y++)
            {
                gridPos.Add(new Vector2(x, y));
            }
        }
    }

    // Setting up the board
    void BoardSetup()
    {
        boardParent = new GameObject("Board Parent").transform;

        for(int x = 1; x < Columns; x++)
        {
            for(int y = 1; y < Rows; y++)
            {
                var Tile = Instantiate(FloorTiles[Random.Range(0, FloorTiles.Length)], new Vector2(x, y), Quaternion.identity);
                Tile.transform.SetParent(boardParent);
            }
        }
    }

    public void SetUpScene(int CurrentLvl)
    {
        InitilizeGridPos();
        BoardSetup();
        PlaceRanObj(WallTiles, 4, 9);
        PlaceRanObj(FoodTiles, 1, 2);
        PlaceRanObj(EnemyTiles, CurrentLvl, CurrentLvl + 2);
    }

    public Vector2 RandPos()
    {
        var randIndex = Random.Range(0, gridPos.Count + 1);
        var randPos = gridPos[randIndex];
        gridPos.RemoveAt(randIndex);
        return randPos;
    }

    public void PlaceRanObj(GameObject[] sprites, int min, int max)
    {
        var random = Random.Range(min, max + 1);
        for (int i = 0; i < random; i++)
        {
            var randPos = RandPos();
            var spriteChoice = sprites[Random.Range(0, sprites.Length)];
            Instantiate(spriteChoice, randPos, Quaternion.identity);
        }
    }
}
