using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerCoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DoodleController>(out DoodleController dood))
        {
            dood.GetCent();
            Destroy(this.gameObject);
        }
    }
}
