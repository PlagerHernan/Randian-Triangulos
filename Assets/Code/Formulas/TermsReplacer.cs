using TMPro;
using UnityEngine;

public class TermsReplacer : MonoBehaviour
{
    TextMeshProUGUI _textComp;
    string _formulaText;

    private void Awake()
    {
        _textComp = GetComponent<TextMeshProUGUI>();
        _formulaText = _textComp.text;
    }

    public void ReplaceToNumber(string target, float number)
    {
        ReplaceToNumber(target, number.ToString());
    }
    public void ReplaceToNumber(string target, string number)
    {
        if (_formulaText == "") return;

        _formulaText = _formulaText.Replace(target, number);
        _textComp.text = _formulaText;
    }
}
