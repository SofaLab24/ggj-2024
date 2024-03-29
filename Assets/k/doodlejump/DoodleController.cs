using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private float moveX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;

    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }
    public void GetCent()
    {
        float coins = PlayerPrefs.GetFloat("money");
        coins += 0.01f;
        PlayerPrefs.SetFloat("money", coins);
    }
}
