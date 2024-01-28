using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death2 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
         if (collision.gameObject.name.StartsWith("Dood"))
        {
            SceneManager.LoadScene("Vincentas-UITest");
        }
    }
}
