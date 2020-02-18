using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    [SerializeField]
    private float enemySpeed = 4;

    private Player _player;

    Animator anim;
  
    [SerializeField]
    private GameObject _laserPrefab;

    BoxCollider2D newcollider;

    AudioSource _audioSource;

    [SerializeField]
    AudioClip enemyBoom;

    [SerializeField]
    AudioClip laserShot;

    private float fireRate = 3f;

    private float canFire = -1f;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        newcollider = GetComponent<BoxCollider2D>();
      
        if(_player == null)
        {
            Debug.LogError("The Player is null");
        }
        
        anim = gameObject.GetComponent<Animator>();

        if(anim == null)
        {
            Debug.LogError("Animator is null");
        }
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = enemyBoom;
    }

    void Update()
    {
        CalculateMovement();

        if(Time.time > canFire)
        {
            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;
            GameObject enemylaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);

            Laser[] lasers = enemylaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
            _audioSource.PlayOneShot(laserShot);
        } 

    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        if (transform.position.y <= -6f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            newcollider.enabled = false;
            anim.SetTrigger("OnEnemyDeath");
            enemySpeed = 2.5f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.0f);
        }

        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            newcollider.enabled = false;
            anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            enemySpeed = 2.5f;
            Destroy(this.gameObject, 2.0f);
            
        }

        if (other.gameObject.tag == "BigLaser")
        {
            if (_player != null)
            {
                _player.AddScore(10);
            }
            newcollider.enabled = false;
            anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 2.0f);

        }

    }
}
