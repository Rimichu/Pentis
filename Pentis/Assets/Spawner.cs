using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Pentominos
    public GameObject[] pentaminoes;

    public void spawnNext()
    {
        // Random index
        int i = Random.Range(0, pentaminoes.Length);

        // Spawn group at current position
        // replaced Instantiate(pentaminoes[i], transform.position, Quaternion.identity); for debugging
        Instantiate(pentaminoes[i], transform.position, Quaternion.identity);
    }

    void Start()
    {
        // Spawn initial group
        spawnNext();
    }
}
