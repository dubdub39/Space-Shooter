using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int powerUpSpeeed = 3;

    [SerializeField]    //0 = triple_shot, 1 = speed, 2 = shields
    private int powerupID;

    
    void Start()
    {
        Vector3 randomPos = new Vector3(Random.Range(-9.5f, 9.5f), 8, 0);
        transform.position = randomPos;
    }

    void Update()
    {
        transform.Translate(Vector3.down * powerUpSpeeed * Time.deltaTime);

        if (transform.position.y <= -7)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        print("speed power-up collected!");
                        break;
                    case 2:
                        player.ShieldPowerUp();
                        break;
                    case 3:
                        player.HealthPowerUp();
                        break;
                    default:
                        print("default value");
                        break;
                }
            }
            Destroy(this.gameObject);
           
        }
    }

}
