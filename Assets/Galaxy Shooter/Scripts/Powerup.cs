using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]    private float _speed = 3f;
    [SerializeField]    private int powerupID; //0 = Triple Shot ; 1 = Speed boost ; 2 = Shield

    [SerializeField] private AudioClip _powerupClip;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.name + " collided with: " + collision.name);
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                AudioSource.PlayClipAtPoint(_powerupClip, Camera.main.transform.position, 1f);
                if(powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if(powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                else if (powerupID == 2)
                {
                    player.ShieldPowerupOn();
                }
            }

            Destroy(this.gameObject);
        }
    }
}
