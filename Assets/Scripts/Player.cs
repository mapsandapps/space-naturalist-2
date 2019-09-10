using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  [Header("Player Movement")]
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] int health = 1000;
  private bool inputEnabled = true;

  [Header("Projectile")]
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] float projectileSpeed = 20f;
  float xMin;
  float xMax;
  float yMin;
  float yMax;

  [Header("Audio")]
  [SerializeField] AudioClip deathSFX;
  [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
  [SerializeField] AudioClip projectileSFX;
  [SerializeField] [Range(0, 1)] float projectileSFXVolume = 0.25f;

  // Start is called before the first frame update
  void Start()
  {
    SetUpMoveBoundaries();
  }

  // Update is called once per frame
  void Update()
  {
    if (inputEnabled)
    {
      Move();
      Fire();
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (damageDealer)
    {
      ProcessHit(damageDealer);
    }

    Friend friend = other.gameObject.GetComponent<Friend>();
    if (friend)
    {
      CollectFriend(friend);
    }
  }

  private void CollectFriend(Friend friend)
  {
    friend.Collect();
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
    GetComponent<SpriteRenderer>().color = Color.black;
    inputEnabled = false;
    AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    StartCoroutine(WaitForDeathSoundToComplete());
  }

  IEnumerator WaitForDeathSoundToComplete()
  {
    yield return new WaitForSeconds(1.547f);
    EndGame();
  }

  public void EndGame()
  {
    Destroy(gameObject);
    FindObjectOfType<LevelLoading>().LoadGameOver();
  }

  public int GetHealth()
  {
    return health;
  }

  private void Fire()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
      bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
      AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileSFXVolume);
    }
  }

  private void Move()
  {
    float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
    float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

    float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

    transform.position = new Vector2(newXPos, newYPos);
  }

  private void SetUpMoveBoundaries()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
  }
}
