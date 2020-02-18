using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
   
    [SerializeField]
    float speed = 3.5f;

    [SerializeField]
    float shiftSpeed = 1.2f;

    [SerializeField]
    float powerUpSpeed = 8.5f;

    private int _shieldDamage;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject tripleLaserPrefab;

    [SerializeField]
    CameraShake cameraShake;

    [SerializeField]
    private float fireRate = 1f;

    private float canFire = -1f;

    [SerializeField]
    Vector3 offSet = new Vector3(0, 1.05f, 0);

    [SerializeField]
    Vector3 tripleLaserOffset = new Vector3(-1.55f, 1.05f, 0);

    [SerializeField]
    private int lives = 3;

    private SpawnManager spawnManager;

    private bool isTripleShotActive = false;
    private bool isSpeedPowerupActive = false;
    private bool isShieldActive = false;
    private bool isPlayerDamaged = false;
   
    [SerializeField]
    GameObject shield; //variable reference to the shield visualizer

    [SerializeField]
    GameObject rightEngineDmg, leftEngineDmg;

    [SerializeField]
    private AudioSource soundSource;

    [SerializeField]
    private AudioClip laserShot;

    [SerializeField]
    private AudioClip _explosion;

    [SerializeField]
    private AudioClip _playerHit;

    [SerializeField]
    private AudioClip _powerUp;

    [SerializeField]
    private AudioClip _powerDown;

    [SerializeField]
    private AudioClip _noAmmo;

    [SerializeField]
    private AudioClip _reload;

    [SerializeField]
    GameObject playerExplosion;

    [SerializeField]
    private int score;

    private UI_Manager _uiManager;

    [SerializeField]
    private Thrusters thrusters;

    private float currentThrust = 0;

    [SerializeField]
    private float fuelBurningSpeed, fuelRecoverySpeed;

    [SerializeField]
    private int _playerAmmo = 15;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if (spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        rightEngineDmg.SetActive(false);
        leftEngineDmg.SetActive(false);

        soundSource = GetComponent<AudioSource>();
        soundSource.clip = laserShot;
    }
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        
        
        if (isSpeedPowerupActive)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {


                if (currentThrust < 1.0f)
                {
                    currentThrust += 1f * Time.deltaTime;
                    thrusters.SliderAmount(currentThrust);
                }
             
                
            transform.Translate(direction * powerUpSpeed * shiftSpeed * Time.deltaTime);
            } else
            {
                if (currentThrust > 0)
                {
                    currentThrust -= fuelRecoverySpeed * Time.deltaTime;
                    thrusters.SliderAmount(currentThrust);
                }
                transform.Translate(direction * powerUpSpeed * Time.deltaTime);
            }
            
            
        } else if (isSpeedPowerupActive == false && Input.GetKey(KeyCode.LeftShift))
        {
            
            if (currentThrust < 1.0f)
                currentThrust += 1f * Time.deltaTime;
                thrusters.SliderAmount(currentThrust);

            transform.Translate(direction * speed * shiftSpeed * Time.deltaTime);
        }  else
        {
            if (currentThrust > 0)
            {
                currentThrust -= fuelRecoverySpeed * Time.deltaTime;
            }
            
            thrusters.SliderAmount(currentThrust);
            transform.Translate(direction * speed * Time.deltaTime);
        }
        
                
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
        else if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        /*Limiting the y movement can also be done via "clamping." A function called
         * Mathf.Clamp can be used with a single line of code to limit movement 
         between two values.  We would code as follows:
         
         transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 0), 0)
         */
        }     
    }
    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            
            {
                
                canFire = Time.time + fireRate;
                if (isTripleShotActive)
                {
                    soundSource.Play();
                    Instantiate(tripleLaserPrefab, transform.position + tripleLaserOffset, Quaternion.identity);
                }
                else
                {
                    _uiManager.DisplayAmmo(_playerAmmo);
                    if(_playerAmmo > 0)
                    {
                        _playerAmmo--;
                        soundSource.Play();
                        Instantiate(laserPrefab, transform.position + offSet, Quaternion.identity);
                    }
                    else
                    {
                        soundSource.PlayOneShot(_noAmmo);
                    }
                    
                }
            }
            
        }
    }

    public void Damage()
    {
        cameraShake.CreateShake(1);

        if (isShieldActive == true)
        {
            if (_shieldDamage == 3)
            {
                _shieldDamage--;
                shield.GetComponent<SpriteRenderer>().color = Color.blue;
            }

            else if (_shieldDamage == 2)
            {
                _shieldDamage--;
                shield.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            else if (_shieldDamage == 1)
            {
                _shieldDamage--;
                shield.GetComponent<SpriteRenderer>().color = Color.grey;
            }
            else
            {
                isShieldActive = false;
                {
                    shield.SetActive(false);
                    soundSource.PlayOneShot(_powerDown);
                    return;
                }
            }
        }
        else
        lives -= 1;
        soundSource.PlayOneShot(_playerHit);
        _uiManager.UpdateLives(lives);

        if (lives == 2)
        {
            rightEngineDmg.SetActive(true);
        }

        if (lives == 1)
        {
            leftEngineDmg.SetActive(true);
        }

        if (lives < 1)
        {
            spawnManager.OnPlayserDeath();
            soundSource.PlayOneShot(_explosion);
            Instantiate(playerExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
    }

    public void SpeedPowerupActive()
    {
        isSpeedPowerupActive = true;
        StartCoroutine(SpeedPowerDownCoroutine());
    }

    IEnumerator SpeedPowerDownCoroutine()
    {
        yield return new WaitForSeconds(5f);
        isSpeedPowerupActive = false;
    }

    public void ShieldPowerUp()
    {
        if (isShieldActive == false)
        {
            soundSource.PlayOneShot(_powerUp);
        }
        isShieldActive = true;
        shield.SetActive(true);
        _shieldDamage = 3;
        shield.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void HealthPowerUp()
    {
        if (lives < 3) 
        { 
            lives++;
            _uiManager.UpdateLives(lives);
            if (lives == 3)
            {
             rightEngineDmg.SetActive(false);
            }

            if (lives == 2)
            {
              leftEngineDmg.SetActive(false);
            }
        }
    }

    public void AmmoPowerUp()
    {
        if (_playerAmmo < 15)
        {
            _playerAmmo = 15;
            _uiManager.DisplayAmmo(_playerAmmo);
            soundSource.PlayOneShot(_reload);
        }
            
    }

     public void AddScore(int points)
    {
        score += points;
        _uiManager.UpdateScore(score);
    }
    
}
