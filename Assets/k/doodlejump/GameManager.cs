using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject plaformPrefab;
    public GameObject coin;
    [Range(0, 1)]
    public float coinSpawnChance;
    public int platformCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Sp1 = new Vector3();

        for (int i = 0; i < platformCount; i++)
        {
            Sp1.y += Random.Range(2f, 4f);
            Sp1.x = Random.Range(-5f, 5f);
            Instantiate(plaformPrefab, Sp1, Quaternion.identity);

            if (Random.Range(0, 1f) < coinSpawnChance)
            {
                Vector3 coinSpawn = new Vector3(Sp1.x, Sp1.y + 1f);
                Instantiate(coin, coinSpawn, Quaternion.identity);
            }

        }

    }


}
