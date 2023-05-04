using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private float _horizontalInput;
    private Animator _anim;

    [SerializeField]
    private Player _player;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(_player != null && _player.isPlayerOne)
        {
            _horizontalInput = Input.GetAxisRaw("HorizontalP1");
        }
        else if(_player != null && _player.isPlayerTwo)
        {
            _horizontalInput = Input.GetAxisRaw("HorizontalP2");
        }

        if (_horizontalInput < 0)
        {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        else if (_horizontalInput > 0)
        {
            _anim.SetBool("Turn_Right", true);
            _anim.SetBool("Turn_Left", false);
        }
        else
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }
    }
}
