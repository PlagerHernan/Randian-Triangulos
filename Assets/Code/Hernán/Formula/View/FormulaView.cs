using UnityEngine;
using UnityEngine.UI;

public class FormulaView : MonoBehaviour
{
    FormulaSheet _formulaSheet;
    FormulaButton _formulaButton;

    Animator _animator; 
    Image[] _images;

    enum Images
    {
        left,
        right
    }

    void Awake() 
    {
        _formulaSheet = FindObjectOfType<FormulaSheet>();
        _formulaButton = FindObjectOfType<FormulaButton>();

        _animator = GetComponent<Animator>();
        _images = GetComponentsInChildren<Image>();    
    } 

    public void MoveToSide(string side)
    {
        AudioHandler.PlaySound("ChangeFormula");
        
        _formulaButton.Hide();

        //al moverse hacia la izquierda, pasa al frente la imagen derecha
        if (side == "left")
        {
            MoveImageToFront(Images.right);
        }
        //al moverse hacia la derecha, pasa al frente la imagen izquierda
        else if (side == "right")
        {
            MoveImageToFront(Images.left);
        }

        _animator.SetTrigger(side);
    }

    void MoveImageToFront(Images image)
    {
        //cambia indíce para mostrar imagen por delante del resto
        _images[(int)image].transform.SetSiblingIndex(_images.Length);
    }

    public void SetTextButton(string formulaText)
    {
        _formulaButton.UpdateText(formulaText);
    }

    //llamado desde animación formulaMenu
    void ActivateButton()
    {
        _formulaButton.Show();
    }
}
