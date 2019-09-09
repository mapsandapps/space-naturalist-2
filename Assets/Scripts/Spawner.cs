using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> enemyWaveConfigs;
  [SerializeField] List<WaveConfig> friendlyWaveConfigs;
  [SerializeField] int wavesCompleted = 0;
  [SerializeField] bool looping = true;
  [SerializeField] string waveType;

  IEnumerator Start()
  {
    do
    {
      yield return StartCoroutine(SpawnWave());
    } while (looping);
  }

  private IEnumerator SpawnWave()
  {
    if (wavesCompleted % 4 == 2)
    {
      // spawn friendly wave
      waveType = "Friend";
      int randomWaveIndex = Random.Range(0, friendlyWaveConfigs.Count);
      WaveConfig currentWave = friendlyWaveConfigs[randomWaveIndex];
      wavesCompleted++;
      StartCoroutine(SpawnAllEnemiesInWave(currentWave));
      yield return new WaitForSeconds(4.0f);
    }
    else
    {
      // spawn enemy wave
      waveType = "Enemy";
      int randomWaveIndex = Random.Range(0, enemyWaveConfigs.Count);
      WaveConfig currentWave = enemyWaveConfigs[randomWaveIndex];
      wavesCompleted++;
      StartCoroutine(SpawnAllEnemiesInWave(currentWave));
      yield return new WaitForSeconds(4.0f);
    }
  }

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
  {
    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
    {
      var angle = waveType == "Enemy" ? Quaternion.Euler(new Vector3(0, 0, 225)) : Quaternion.identity;
      var newCharacter = Instantiate(waveConfig.GetCharacterPrefab(), waveConfig.GetWaypoints()[0].transform.position, angle);

      newCharacter.GetComponent<Pathing>().SetWaveConfig(waveConfig);

      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
    }
  }
}
