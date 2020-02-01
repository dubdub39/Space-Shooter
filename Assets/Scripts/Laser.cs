using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    int laserSpeed = 8;

    [SerializeField]
    private bool isEnemyLaser;
    
    void Update()
    {
        if(isEnemyLaser == false)
        {
            MoveUp();
        } else
        {
            MoveDown();
        }
        
    }
    private void MoveUp()
    {
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * laserSpeed * Time.deltaTime);
        if (transform.position.y <= -10)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
