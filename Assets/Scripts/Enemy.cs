﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  [SerializeField] float health = 100f;
  [SerializeField] float shotCounter;
  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 3f;
  [SerializeField] GameObject projectile;
  [SerializeField] float projectileSpeed = 20f;

  // Start is called before the first frame update
  void Start()
  {
    shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
  }

  // Update is called once per frame
  void Update()
  {
    CountDownAndShoot();
  }

  private void CountDownAndShoot()
  {
    shotCounter -= Time.deltaTime;
    if (shotCounter <= 0f)
    {
      Fire();
      shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
  }

  private void Fire()
  {
    GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }
}