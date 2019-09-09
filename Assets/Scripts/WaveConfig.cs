using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
  // TODO: i probably want to remove this enum and uses of it
  public enum WaveType { Enemy, Friend };

  [SerializeField] WaveType waveType;
  [SerializeField] GameObject characterPrefab;
  [SerializeField] GameObject pathPrefab;
  [SerializeField] float secondsBetweenSpawns = 0.5f;
  [SerializeField] float spawnRandomFactor = 0.3f;
  [SerializeField] int numberOfEnemies = 5;
  [SerializeField] float moveSpeed = 2f;

  public GameObject GetCharacterPrefab() { return characterPrefab; }
  public WaveType GetWaveType() { return waveType; }
  public List<Transform> GetWaypoints()
  {
    var waveWaypoints = new List<Transform>();

    foreach (Transform child in pathPrefab.transform)
    {
      waveWaypoints.Add(child);
    }
    return waveWaypoints;
  }
  public float GetTimeBetweenSpawns() { return secondsBetweenSpawns; }
  public float GetSpawnRandomFactor() { return spawnRandomFactor; }
  public int GetNumberOfEnemies() { return numberOfEnemies; }
  public float GetMoveSpeed() { return moveSpeed; }
}
