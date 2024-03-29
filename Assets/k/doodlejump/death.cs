using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{

    public GameObject player;
    public GameObject platformPrefab;
    public GameObject coin;
    [Range(0, 1f)]
    public float coinSpawnChance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("background"))
        {
            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 50);
        }
        else if (collision.gameObject.name.StartsWith("platform"))
        {
            Vector2 spawnLocation = new Vector2(Random.Range(-5.5f, 5.5f), player.transform.position.y + 14 + Random.Range(0.2f, 1.0f));
            collision.gameObject.transform.position = spawnLocation;
            if (Random.Range(0, 1f) <= coinSpawnChance)
            {
                Instantiate(coin, new Vector3(spawnLocation.x, spawnLocation.y + 1), Quaternion.identity);
            }
        }

        //myPlat = (GameObject) Instantiate(platformPrefab, new Vector2(Random.Range(-10f, 10f), player.transform.position.y + 10 + Random.Range(0f, 0.5f)), Quaternion.identity);  
        //Destroy(collision.gameObject);
    }
}
