using System.Collections;
using UnityEngine;

public class SecretZone : MonoBehaviour
{
    SecretZoneView _secretZoneView;
    Road _road;

    private void Awake()
    {
        _secretZoneView = GetComponent<SecretZoneView>();
        _road = FindObjectOfType<Road>();
    }

    public void UnveilZone()
    {
        _secretZoneView.UnveilZone();
    }

    public void RotateTowards(RotationDirection direction)
    {
        if (_road != null)
        {
            Vector3[] dir;
            if (direction == RotationDirection.Left)
            {
                dir = new Vector3[] { -transform.right };
                _road.Directions = dir;
                _road.ForceRotate(false);
            }
            else
            {
                dir = new Vector3[] { transform.right };
                _road.Directions = dir;
                _road.ForceRotate(true);
            }
        }
    }
}

public enum RotationDirection
{
    Left,
    Right
}
