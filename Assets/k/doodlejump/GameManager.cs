using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject plaformPrefab;
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
            // coin
        }

    }


}
