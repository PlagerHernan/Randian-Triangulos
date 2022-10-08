using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingFormulaOpener : MonoBehaviour
{
    [SerializeField] GameObject _formulaScreen;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        _formulaScreen.SetActive(true);
    }
}