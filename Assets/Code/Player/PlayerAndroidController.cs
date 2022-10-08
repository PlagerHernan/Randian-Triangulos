using UnityEngine;

public class PlayerAndroidController : MonoBehaviour
{
    Road _road;

    Character _player;

    private void Awake()
    {
        _road = FindObjectOfType<Road>();

        _player = GetComponent<Character>();
    }

    private void Start()
    {
        SwipeDetector.OnSwipe += SwipeDodge;
        SwipeDetector.OnSwipe += SwipeRotation;
    }

    //Swipe
    SwipeDirection _swipeDirection;
    //bool _left = false;
    //bool _right = false;
    //bool _up = false;
    private void SwipeRotation(SwipeData data)
    {
        _swipeDirection = data.Direction;

        if (_swipeDirection == SwipeDirection.Right) _road.Rotate(true);
        else if (_swipeDirection == SwipeDirection.Left) _road.Rotate(false);

        if (_swipeDirection == SwipeDirection.Up)
        {
            _road.Resume();
            _player.ResumeMovement();
        }
    }

    private void SwipeDodge(SwipeData data)
    {
        _swipeDirection = data.Direction;

        if(_swipeDirection == SwipeDirection.Right)
            _player.MovingToRight = true;
        else if(_swipeDirection == SwipeDirection.Left)
            _player.MovingToLeft = true;
    }
}
