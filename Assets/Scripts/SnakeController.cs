using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity = Vector3.forward;
    [SerializeField] private Vector2 _coord;

    private MapGenerator _map;

    [SerializeField] private float msBetweenSteps = 300;
    private float _nextMoveTime;

    public Transform tailPrefab;
    private Queue<Transform> _tails;
    private Queue<Vector2> _tailCoords;
    private bool _newTailAdded;
    
    public event Action OnDeath;
    
    void Start()
    {
        _map = FindObjectOfType<MapGenerator>();
        transform.position = _map.CoordToPosition(0, 0) + Vector3.up * .5f;
        _tails = new Queue<Transform>();
        _tailCoords = new Queue<Vector2>();
    }

    public void FixedUpdate()
    {
        if (_velocity != Vector3.zero)
        {
            Move();
        }

        EatFood();
    }

    public void MoveField(Vector3 direction)
    {
        _velocity = direction.normalized;
    }
    
    private void Move()
    {
        if (Time.time > _nextMoveTime)
        {
            _nextMoveTime = Time.time + msBetweenSteps / 1000;
            
            //Dont move Tail, if new TailPart is Added
            if (!_newTailAdded)
            {
                MoveTail(transform.position, _coord);
            }
            _newTailAdded = false;
            
            _coord = UpdateCoord(_velocity);
            //death on hit yourself
            if (_map._occupiedCoords.Contains(_coord))
            {
                Die();
            }
            
            //Move SnakeHead
            transform.position = _map.CoordToPosition((int) _coord.x, (int) _coord.y) + Vector3.up * 0.5f;

            //_velocity = Vector3.zero; //-->Comment for Snake Movement!
           SetMapOccupiedCoords();
        }
    }

    private void EatFood()
    {
        if (_coord == _map.GetFoodCoord())
        {
            _map.SpawnNewFood();
            AddToTail();
            _newTailAdded = true;
        }
    }

    private Vector2 UpdateCoord(Vector3 velocity)
    {
        Vector2 newCoord = new Vector2(Utility.Mod((int) (_coord.x + velocity.x), (int) _map.mapSize.x),
            Utility.Mod((int) (_coord.y + velocity.z), (int) _map.mapSize.y));
        if (!_map.isCoordOnMap(newCoord))
        {
            throw new NotSupportedException();
        }

        return newCoord;
    }

    private void AddToTail()
    {
        Transform newTailPart =
            Instantiate(tailPrefab, transform.position, Quaternion.identity);
        Vector2 newTailCoords = _coord;
        _tails.Enqueue(newTailPart);
        _tailCoords.Enqueue(newTailCoords);
        newTailPart.parent = transform.parent;
    }

    private void MoveTail(Vector3 nextPosition, Vector2 nextCoord)
    {
        if (_tails.Count > 0)
        {
            Transform lastTailPart = _tails.Dequeue();
            _tailCoords.Dequeue();
            lastTailPart.position = nextPosition;
            _tails.Enqueue(lastTailPart);
            _tailCoords.Enqueue(nextCoord);
        }
    }

    private void SetMapOccupiedCoords()
    {
        List<Vector2> occupiedCoords = new List<Vector2>(_tailCoords);
        occupiedCoords.Add(_coord);
        _map._occupiedCoords = occupiedCoords;
    }
    
    [ContextMenu("Self Destruct")]
    void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }
    
}