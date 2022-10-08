using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretZoneView : MonoBehaviour
{
    [SerializeField] SpriteRenderer _arrowIndicator;
    SpriteMask _mask;

    private void Awake()
    {
        _mask = GetComponentInChildren<SpriteMask>();
    }

    public void UnveilZone()
    {
        _mask.enabled = false;

        if (_arrowIndicator != null)
            _arrowIndicator.gameObject.SetActive(false);
    }
}
