using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject tree1, tree2, tree3, flower1, flower2, rock1, rock2;
    public Transform natureParent;
    int width, length;
    GridStructure grid;
    public int radius = 4;

    public GridStructure Grid { get => grid; }

    public void PrepareWorld(int cellSize, int width, int length)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.width = width;
        this.length = length;
        PrepareEnvironment();
    }
    public GameObject ChooseRandomGameobject()
    {
        System.Random rnd = new System.Random();
        int random = rnd.Next(1, 7);
        switch(random)
        {
            case 1:
                return tree1;
            case 2:
                return tree2;
            case 3:
                return tree3;
            case 4:
                return flower1;
            case 5:
                return flower2;
            case 6:
                return rock1;
            case 7:
                return rock2;
            default:
                return tree1;
        }
    }

    private void PrepareEnvironment()
    {
        EnvironmentGenerator generator = new EnvironmentGenerator(width, length, radius);
        foreach (Vector2 samplePosition in generator.Samples())
        {
            PlaceObjectOnTheMap(samplePosition, ChooseRandomGameobject());
        }
    }

    private void PlaceObjectOnTheMap(Vector2 samplePosition, GameObject objects)
    {
        var positionInt = Vector2Int.CeilToInt(samplePosition);
        var positionGrid = grid.GridPosCalculator(new Vector3(positionInt.x, 0, positionInt.y));
        var natureElement = Instantiate(objects, positionGrid, Quaternion.identity, natureParent);
        grid.AddNatureToCell(positionGrid, natureElement);
    }

    public void DestroyEnvironmentOnLocation(Vector3 position)
    {
        var elementsToDestroy = grid.GetNatureObjectsToRemove(position);
        foreach (var element in elementsToDestroy)
        {
            Destroy(element);
        }
    }
}
