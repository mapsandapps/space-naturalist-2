using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] int startingWave = 0;
  [SerializeField] bool looping = true;

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
      // yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
      StartCoroutine(SpawnAllEnemiesInWave(currentWave));
      yield return new WaitForSeconds(5.0f);
    }
  }

  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
  {
    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
    {
      var newCharacter = Instantiate(waveConfig.GetCharacterPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.Euler(new Vector3(0, 0, 225)));

      newCharacter.GetComponent<Pathing>().SetWaveConfig(waveConfig);

      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
    }
  }
}
