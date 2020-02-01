using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
  
    private bool stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false)
        {
            Vector3 randomEnemySpawn = new Vector3(Random.Range(-9.5f, 9.5f), 8f, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, randomEnemySpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

            /* assigning the newEnemy GO here just so enemies will be spawned neatly in another GO heirarchy in 
             the inspector.  after we define the newEnemy GO that is to be instantiated, we assign the transform.parent
             of the newEnemy to the enemyContainer empty GO*/
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false)
        {
            Vector3 randomPowerupSpawn = new Vector3(Random.Range(-9f, 9.5f), 8f, 0);
            int randomInt = Random.Range(0, 3);
            GameObject newPowerup = Instantiate(powerups[randomInt], randomPowerupSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
        
    }

    public void OnPlayserDeath()
    {
        stopSpawning = true;
    }
}
