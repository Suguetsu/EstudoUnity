﻿
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int pos; //-12

    [SerializeField] float inBounds;

    private CharacterController _char;
    private GameManager _gm;



    void Start()
    {
        _char = GetComponent<CharacterController>();
        _gm = FindObjectOfType(typeof(GameManager)) as GameManager;

        transform.localPosition = new Vector3(0, 0, pos);
        transform.gameObject.tag = "Player";




    }



    // Update is called once per frame
    void Update()
    {
        if (_gm.PauseGame()) return;

        float move = Input.GetAxisRaw("Horizontal");



        if (transform.localPosition.x > inBounds && move > 0)
        {
            transform.localPosition = transform.localPosition;
            return;
        }
        else if (transform.localPosition.x < -inBounds && move < 0)
        {
            transform.localPosition = transform.localPosition;
            return;
        }
        else
        {
            _char.SimpleMove(transform.TransformVector(speed * move, 0, 0));
        }



        if (move != 0)
        {
            Debug.Log("Chamar animação"); //10
        }
    }





}
