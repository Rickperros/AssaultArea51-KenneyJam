using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public GameObject AlienPrefab;
    [Range (10, 30)]public float TimeBetweenSpawns;

    [HideInInspector] public bool AlienOnScene;
    float spawnTimer;

    List<CarOscillation> Cars = new List<CarOscillation>();

    private void Update()
    {
        if (AlienOnScene)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer > TimeBetweenSpawns)
            SpawnAlien();
    }

    private void SpawnAlien ()
    {
        Vector3 SpawnPos = new Vector3(Random.Range(-20, 20), 12);
        GetCards();

        GameObject alien = Instantiate(AlienPrefab, SpawnPos, Quaternion.identity);
        alien.GetComponent<Alien>().manager = this;
        alien.GetComponent<Alien>().Car = Cars[Random.Range (0, Cars.Count-1)];
        AlienOnScene = true;


        spawnTimer = Random.Range(0, 10);
    }

    private void GetCards ()
    {
        Cars.Clear();
        Cars.AddRange(FindObjectsOfType<CarOscillation>());
    }
}
