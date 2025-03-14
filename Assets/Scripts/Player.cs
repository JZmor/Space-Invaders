﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
  public delegate void PlayerDied();
  public static event PlayerDied OnPlayerDied;
  
  public GameObject bulletPrefab;

  public Transform shottingOffset;

  Animator playerAnimator;

  public AudioClip explodeClip;
  void Start()
  {
    Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
    playerAnimator = GetComponent<Animator>();
  }

  void OnDestroy()
  {
    Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
  }

  void EnemyOnOnEnemyDied(int points)
  {
    Debug.Log($"I know about dead enemy points: {points}");
  }

  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        playerAnimator.SetTrigger("Shoot");
        GameObject shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);

        //Destroy(shot, 3f);
      }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.CompareTag("Evil Bullet"))
      {
        Destroy(other.gameObject);
        GetComponent<Collider2D>().enabled = false;
        playerAnimator.SetTrigger("Death");
      }
    }

    void killPlayer()
    {
      OnPlayerDied?.Invoke();
      Destroy(gameObject);
    }

    void PlaySound()
    {
      AudioSource audioSrc = GetComponent<AudioSource>();
      audioSrc.clip = explodeClip;
      audioSrc.Play();
    }
}
