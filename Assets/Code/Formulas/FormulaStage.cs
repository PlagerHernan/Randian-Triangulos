using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulaStage : MonoBehaviour
{
    HoleTriggerHammer _triangle;

    public void SetTriangle(HoleTriggerHammer tr)
    {
        _triangle = tr;
    }

    public void SetWin()
    {
        /* if (_triangle != null)
            _triangle.TouchEnabled = true;

        gameObject.SetActive(false); */
    }
}
