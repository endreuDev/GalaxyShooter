using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    public bool gameOver = true;
    public GameObject player;
    public GameObject playerOne;
    public GameObject playerTwo;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if(gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(isCoopMode == false)
                {
                    Instantiate(player, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    Instantiate(playerOne, new Vector3(-3, 0), Quaternion.identity);
                    Instantiate(playerTwo, new Vector3(3, 0), Quaternion.identity);
                }
                gameOver = false;
                _uiManager.HideTitleScreen();
                if (_spawnManager != null)
                {
                    _spawnManager.StartSpawnRoutines();
                }
            }
        }
    }
}
