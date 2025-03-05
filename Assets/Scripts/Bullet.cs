using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class Bullet : MonoBehaviour
{
  private Rigidbody2D myRigidbody2D;

  public float speed = 5;

  public bool evil = false;
    // Start is called before the first frame update
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }

    // Update is called once per frame
    private void Fire()
    {
      if (evil)
      {
        myRigidbody2D.linearVelocity = Vector2.down * speed; 
      }
      else
      {
        myRigidbody2D.linearVelocity = Vector2.up * speed; 
      }
      Debug.Log("Wwweeeeee");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Despawn"))
      {
        Destroy(gameObject);
      }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("Despawn"))
      {
        Destroy(gameObject);
      }
    }
}
