using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretZoneTrigger : MonoBehaviour
{
    PlayerAndroidController _playerController;
    SecretZone _secretZone;

    [SerializeField] RotationDirection _rotateTo;

    private void Awake()
    {
        _secretZone = GetComponentInParent<SecretZone>();
    }
    private void UnveilAndRotate(SwipeDirection direction)
    {
        _secretZone.UnveilZone();

        var allowedToRotate = _rotateTo == RotationDirection.Left ? SwipeDirection.Left : SwipeDirection.Right;

        if (allowedToRotate == direction)
        {
            if (direction == SwipeDirection.Left)
            {
                _secretZone.RotateTowards(RotationDirection.Left);
            }
            else if(direction == SwipeDirection.Right)
            {
                _secretZone.RotateTowards(RotationDirection.Right);
            }
        }
    }

    private void UnveilZone_OnSwipe(SwipeData swipeData) => UnveilAndRotate(swipeData.Direction);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerAndroidController>(out var playerController))
        {
            if(_playerController == null)
            {
                _playerController = playerController;
                SwipeDetector.OnSwipe += UnveilZone_OnSwipe;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerAndroidController>() == _playerController)
        {
            SwipeDetector.OnSwipe -= UnveilZone_OnSwipe;
            _playerController = null;
        }
    }
}
