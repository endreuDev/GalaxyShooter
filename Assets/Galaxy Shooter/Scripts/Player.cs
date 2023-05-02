using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public or Private identifier
    //Data type (int, float, bool, string)
    //Every variable has a name
    //Optional value assigned

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool hasShieldActive = false;

    [SerializeField]    private GameObject _laserPrefab;
    [SerializeField]    private GameObject _tripleShotPrefab;
    [SerializeField]    private float _fireRate = 0.25f;
                        private float _canFire = 0.0f;

    [SerializeField]    private float _speed;
                        public int lives = 3;

    [SerializeField]    private GameObject[] _engines;
    [SerializeField]    private GameObject _explosion;
    [SerializeField]    private GameObject _shield;

    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;

    private int hitCount = 0;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        hitCount = 0;

        if (_gameManager.isCoopMode == false)
        {
            //Current position = new position
            transform.position = new Vector3(0, 0, 0);
        }
    }

    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.98f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedBoost == false)
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        else if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }


        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.26f)
        {
            transform.position = new Vector3(transform.position.x, -4.26f, 0);
        }

        if (transform.position.x > 8.33f)
        {
            transform.position = new Vector3(8.33f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.33f)
        {
            transform.position = new Vector3(-8.33f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if(hasShieldActive == true)
        {
            hasShieldActive = false;
            _shield.SetActive(false);
            return;
        }

        hitCount++;
        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

        lives--;
        _uiManager.UpdateLives(lives);

        if (lives <= 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
        
    }

    //Triple shot powerup controller
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
        
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    //Speed boost powerup controller
    public void SpeedBoostPowerupOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

    //Shield powerup controller
    public void ShieldPowerupOn()
    {
        hasShieldActive = true;
        _shield.SetActive(true);
    }
}
