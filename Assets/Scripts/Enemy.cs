using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  [Header("Enemy Stats")]
  [SerializeField] float health = 100f;
  [SerializeField] int scoreValue = 150;

  [Header("Shooting")]
  [SerializeField] float shotCounter;
  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 3f;
  [SerializeField] GameObject projectile;
  [SerializeField] float projectileSpeed = 20f;

  [Header("Audio")]
  [SerializeField] AudioClip deathSFX;
  [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
  [SerializeField] AudioClip projectileSFX;
  [SerializeField] [Range(0, 1)] float projectileSFXVolume = 0.4f;

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
    AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileSFXVolume);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (!damageDealer) { return; }

    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    damageDealer.Hit();
    if (health <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
    FindObjectOfType<GameSession>().AddToScore(scoreValue);
    Destroy(gameObject);
    AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
  }
}
