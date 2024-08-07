using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BearStatusHandler))]
[RequireComponent(typeof(BearAnimatorManager))]

public class BearMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private WaitForSeconds _waitForSeconds;
    private BearStatusHandler _bearStatusHandler;
    private Coroutine _turnCoroutine;

    private void Start()
    {
        float turnDelay = 1;
        _waitForSeconds = new WaitForSeconds(turnDelay);
        _bearStatusHandler = GetComponent<BearStatusHandler>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction;

        if(_bearStatusHandler.IsWaiting == false)
        {
            if (_bearStatusHandler.IsMovingRight)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
            }

            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            if (_bearStatusHandler.IsGroundNear == false && _turnCoroutine == null)
            {
                _turnCoroutine = StartCoroutine(Turn());
            }
        }
    }

    private IEnumerator Turn()
    {
        yield return _waitForSeconds;
        _bearStatusHandler.ChangeMovingSideStatus();
        yield return _waitForSeconds;
        _turnCoroutine = null;
    }
}