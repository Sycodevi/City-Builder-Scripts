using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public NavMeshSurface navMeshSurface;

    private int houseCount = 0;
    private int carSpawnThreshold = 5;
    private int carSpawnCount = 0;
    private int carCount = 0;

    private void Update()
    {
        //HouseAndCarCountCheck();
    }

    public void HouseAndCarCountCheck()
    {
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        int currentHouseCount = houses.Length;
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        Debug.Log("Current House Count: " + currentHouseCount);
        Debug.Log("Current Car Count: " + cars.Length);

        if (currentHouseCount > houseCount)
        {
            int housesIncrement = currentHouseCount - houseCount;

            for (int i = 0; i < housesIncrement; i++)
            {
                houseCount++;

                carSpawnCount++;
                if (carSpawnCount == carSpawnThreshold)
                {
                    SpawnRandomCar();
                    carSpawnCount = 0;
                    break;
                }
            }
        }
        else if((houses.Length/5) < cars.Length) 
        {
            for (int i = 0; i < cars.Length; i++)
            {
                Destroy(cars[i].gameObject);
                carSpawnCount = 0;
                if (houses.Length /5 == cars.Length)
                {
                    return;
                }
            }

        }

        houseCount = currentHouseCount;
    }

    private void SpawnRandomCar()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();
        GameObject randomCarPrefab = GetRandomCarPrefab();
        Instantiate(randomCarPrefab, randomPosition, Quaternion.identity);
        navMeshSurface.BuildNavMesh(); // Rebuild NavMesh after spawning the car
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
        Vector3[] navMeshVertices = navMeshData.vertices;
        int[] navMeshIndices = navMeshData.indices;

        Vector3 randomPosition = Vector3.zero;
        int randomIndex = Random.Range(0, navMeshIndices.Length / 3);

        Vector3 vertex1 = navMeshVertices[navMeshIndices[randomIndex * 3]];
        Vector3 vertex2 = navMeshVertices[navMeshIndices[randomIndex * 3 + 1]];
        Vector3 vertex3 = navMeshVertices[navMeshIndices[randomIndex * 3 + 2]];

        randomPosition = (vertex1 + vertex2 + vertex3) / 3f;

        return randomPosition;
    }

    private GameObject GetRandomCarPrefab()
    {
        int randomIndex = Random.Range(0, carPrefabs.Length);
        return carPrefabs[randomIndex];
    }
}
