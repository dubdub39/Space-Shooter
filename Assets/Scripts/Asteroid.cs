using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int rotateSpeed = 10;

    private Animator asteroidAnim;
    
    [SerializeField]
    GameObject explosion;

    private SpawnManager _spawnManager;
    void Start()
    {
        Vector3 asteroidPos = transform.position;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Laser")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.15f);

        }
    }

}
