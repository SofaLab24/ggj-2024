using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject kebisPrefab;
    public GameObject cbdPrefab;
    public GameObject obstaclePrefab;
    public GameObject nonAlcoholicBeerPrefab;
    public GameObject kebabarioFloorPrefab;
    public GameObject goombaPrefab;

    private float levelLength = 300f;

    public float obstacleSpawnChance = 0.3f;
    public float cbdSpawnChance = 0.05f;

    public float beerSpawnChance = 0.1f;
    public float goombaSpawnChance = 0.69f;

    public float obstacleSpacing = 10; // Spacing between obstacles
    public float minObstacleHeight = 30f; // Minimum height of obstacles
    public float maxObstacleHeight = 90f; // Maximum height of obstacles

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {

Vector3 floorPosition = new Vector3(0.5f, -2f, 0.0399f);
Vector3 ceilingPosition = new Vector3(0.5f, 12f, 0);
float newXScale = levelLength * 2f; // Adjust this value to the desired x scale
Debug.Log(newXScale);

// Instantiate the object and immediately adjust its scale
GameObject floor = Instantiate(kebabarioFloorPrefab, floorPosition, Quaternion.identity);
floor.transform.localScale = new Vector3(newXScale, floor.transform.localScale.y, floor.transform.localScale.z);

GameObject ceiling = Instantiate(kebabarioFloorPrefab, ceilingPosition, Quaternion.identity);
ceiling.transform.localScale = new Vector3(newXScale, ceiling.transform.localScale.y, ceiling.transform.localScale.z);

        var kebisLocation = new Vector3((levelLength - 1) * obstacleSpacing, 0, 0);
        // Place Kebis at the end of the level
        Instantiate(kebisPrefab, kebisLocation, Quaternion.identity);

        // Spawn obstacles and Cbd_legal_joint randomly
List<Vector3> occupiedPositions = new();

    // Spawn obstacles randomly with proper spacing
for (int x = 0; x < levelLength; x++)
    {
        // Calculate obstacle position
        float obstacleXPosition = x;

        // Check if a new obstacle should be spawned at this position
        if (Random.value > obstacleSpawnChance && IsObstacleNearby(obstacleXPosition, occupiedPositions))
        {
            if (Random.value < 0.33f) {


float obstacleHeight = Random.Range(minObstacleHeight, maxObstacleHeight);
Vector3 obstacleBottomPosition = new Vector3(obstacleXPosition, 0, 0);
GameObject obstacleBottom = Instantiate(obstaclePrefab, obstacleBottomPosition, Quaternion.identity);
float newYScale = obstacleHeight * 3f;

float yOffset = (newYScale - floor.transform.localScale.y) * 0.5f;
obstacleBottom.transform.position += new Vector3(0, yOffset, 0);

// Scale the object vertically
obstacleBottom.transform.localScale = new Vector3(obstacleBottom.transform.localScale.x, newYScale, obstacleBottom.transform.localScale.z);
float gap = 10; // Adjust this value according to the desired gap between obstacles
Vector3 obstacleTopPosition = obstacleBottomPosition + new Vector3(0, newYScale / 2 + gap / 2, 0);
Instantiate(obstaclePrefab, obstacleTopPosition, Quaternion.identity);


            // Add obstacle position to the list of occupied positions
            occupiedPositions.Add(obstacleBottomPosition);
            }
            else {
            // Spawn obstacle
            float obstacleHeight = Random.Range(minObstacleHeight, maxObstacleHeight);
            Vector3 obstaclePosition = new Vector3(obstacleXPosition, 0, 0);
            GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
            float newYScale = obstacleHeight * 6f;
            if (newYScale > 11) {
                newYScale -= 1;
            }

            float yOffset = (newYScale - floor.transform.localScale.y) * 0.5f;
            obstacle.transform.position += new Vector3(0, yOffset / 2, 0);

            // Scale the object vertically
            obstacle.transform.localScale = new Vector3(obstacle.transform.localScale.x, newYScale, 0);

            

            // Add obstacle position to the list of occupied positions
            occupiedPositions.Add(obstaclePosition);

            }
        }
        else if (Random.value < cbdSpawnChance)
        {
            // Spawn CBD (if desired)
            Instantiate(cbdPrefab, new Vector3(obstacleXPosition, 0, 0), Quaternion.identity);
        }
        else if (Random.value < beerSpawnChance) {
            Instantiate(nonAlcoholicBeerPrefab, new Vector2(obstacleXPosition, 0), Quaternion.identity);
        }
        else if (Random.value < goombaSpawnChance) {
            Instantiate(goombaPrefab, new Vector2(obstacleXPosition, 0), Quaternion.identity);
        }

    }
    }

bool IsObstacleNearby(float xPosition, List<Vector3> occupiedPositions)
{
    if (occupiedPositions.Count == 0) {
        var kebisLocation = new Vector3((levelLength - 1) * obstacleSpacing, 0, 0);
        occupiedPositions.Add(kebisLocation);
        return true;
    }
    // Check if any occupied position is too close to the current position
    foreach (Vector3 occupiedPos in occupiedPositions)
    {
        
        var blet = Mathf.Abs(occupiedPos.x - xPosition);
        if (blet > 5 && blet < 20)
        {
            occupiedPositions.Remove(occupiedPos);
            return true;
        }
    }
    return false;
}

}
