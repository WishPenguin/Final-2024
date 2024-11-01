using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject hazard;            // Original asteroid prefab
    public GameObject rareHazard;        // Less frequent asteroid prefab
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float rareHazardFrequency = 0.2f;  // Frequency of rare hazard (e.g., 20%)

    public PlayerController playerController;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        for (int i = 0; i < hazardCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnValues.x, spawnValues.x),
                spawnValues.y,
                spawnValues.z
            );
            Quaternion spawnRotation = Quaternion.identity;

            // Determine if the rare hazard should spawn based on frequency
            GameObject asteroidInstance = Random.value < rareHazardFrequency ?
                                          Instantiate(rareHazard, spawnPosition, spawnRotation) :
                                          Instantiate(hazard, spawnPosition, spawnRotation);

            if (playerController != null)
            {
                playerController.SetTarget(asteroidInstance);
            }

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
