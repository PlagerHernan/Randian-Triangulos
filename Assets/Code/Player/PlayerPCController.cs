using UnityEngine;

public class PlayerPCController : MonoBehaviour
{
    Road _road;

    Character _player;

    void Awake()
    {
        _road = FindObjectOfType<Road>();

        _player = GetComponent<Character>();
    }

    void Update()
    {
        KeysDetect();
    }

    void KeysDetect()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            _player.MovingToLeft = true;
            _road.Rotate(false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            _player.MovingToRight = true;
            _road.Rotate(true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            _road.Resume();
        }
    }
}
