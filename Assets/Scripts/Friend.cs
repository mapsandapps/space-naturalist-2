using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
  [SerializeField] float health = 100f;
  [SerializeField] int scoreValue = 1000;
  [SerializeField] AudioClip pickupSFX;
  [SerializeField] [Range(0, 1)] float pickupSFXVolume = 0.7f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

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
      Destroy(gameObject);
    }
  }

  public void Collect()
  {
    Destroy(gameObject);
    FindObjectOfType<GameSession>().AddToScore(scoreValue);
    AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, pickupSFXVolume);
  }
}
