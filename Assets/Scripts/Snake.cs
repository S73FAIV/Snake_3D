using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeController))]
public class Snake : MonoBehaviour
{
    private SnakeController _snakeController;

    void Start()
    {
        _snakeController = GetComponent<SnakeController>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 direktion = new Vector3((int) Input.GetAxisRaw("Horizontal"), 0, 0);
            _snakeController.MoveField(direktion);
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            Vector3 direktion = new Vector3(0, 0, (int) Input.GetAxisRaw("Vertical"));
            _snakeController.MoveField(direktion);
        }
    }
    
}