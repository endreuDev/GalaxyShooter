using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _explosion;
    
    [SerializeField]private AudioClip _explosionClip;
    private UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.Log("Unable to locate UIManager on EnemyAI script");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if(transform.position.y < -6.8f)
        {
            float randomX = Random.Range(-7.78f, 7.78f);
            transform.position = new Vector3(randomX, 6.8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy collided with: " + collision.name);
        if (collision.tag == "Laser")
        {
            Laser laser = collision.GetComponent<Laser>();
            if (laser != null)
            {
                Destroy(laser.gameObject);
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _uiManager.UpdateScore(10);
                AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
                Destroy(this.gameObject);
            }
        }
        else if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_explosion, transform.position, Quaternion.identity);
            _uiManager.UpdateScore(5);
            AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
