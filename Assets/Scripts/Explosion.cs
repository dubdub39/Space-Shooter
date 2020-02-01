using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private AudioSource _asteroidBoom;

    [SerializeField]
    private AudioClip _boom;
    void Start()
    {
        _asteroidBoom = GetComponent<AudioSource>();
        _asteroidBoom.PlayOneShot(_boom);
        Destroy(this.gameObject, 3.0f);
    }
    void Update()
    {
        
    }
}
