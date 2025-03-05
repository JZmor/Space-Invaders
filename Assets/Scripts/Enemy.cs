using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;

    public delegate void HitWall(bool left);
    public static event HitWall OnHitWall;
    
    public GameObject bulletPrefab;
    Animator enemyAnimator;
    private bool direction = true;

    public int value = 10;
    // Start is called before the first frame update

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        Enemy.OnHitWall += WeHitWall;
        GameManager.speedUpEvent += SetSpeed;
        Player.OnPlayerDied += PlayerDied;
    }

    void WeHitWall(bool left)
    {
        if (left)
        {
            enemyAnimator.SetTrigger("Down");
            enemyAnimator.SetBool("Right", true);
            direction = false;
        }
        else
        {
            enemyAnimator.SetTrigger("Down");
            enemyAnimator.SetBool("Right", false);
            direction = true;
        }
    }

    void SetSpeed(int speed)
    {
        enemyAnimator.speed = speed;
    }

    void PlayerDied()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (this.transform.position.x >= 20 && !direction)
        {
            //enemyAnimator.SetTrigger("Down");
            //enemyAnimator.SetBool("Right", false);
            OnHitWall?.Invoke(false);
            direction = true;
        } else if (this.transform.position.x <= -20 && direction)
        {
            //enemyAnimator.SetTrigger("Down");
            //enemyAnimator.SetBool("Right", true);
            OnHitWall?.Invoke(true);
            direction = false;
        }

        if (new Random().Next(0, 10000) == 777)
        {
            GameObject shot = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Ouch!");
            Destroy(collision.gameObject);
            OnEnemyDied?.Invoke(value);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Enemy.OnHitWall -= WeHitWall;
        GameManager.speedUpEvent -= SetSpeed;
        Player.OnPlayerDied -= PlayerDied;
    }
}
