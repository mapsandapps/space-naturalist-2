﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] int startingWave = 0;
  [SerializeField] bool looping = false;

  // Start is called before the first frame update
  IEnumerator Start()
  {
    do
    {
      yield return StartCoroutine(SpawnAllWaves());
    } while (looping);
  }

  private IEnumerator SpawnAllWaves()
  {
    for (int i = startingWave; i < waveConfigs.Count; i++)
    {
      WaveConfig currentWave = waveConfigs[i];
      yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }
  }

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
  {
    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
    {
      var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.Euler(new Vector3(0, 0, 225)));

      newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
    }
  }
}
