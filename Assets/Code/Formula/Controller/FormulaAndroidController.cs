public class FormulaAndroidController : FormulaController
{
    SwipeDirection _swipeDirection;


    protected override void Start()
    {
        base.Start();
        SwipeDetector.OnSwipe += SwipeDetect;
    }

    void SwipeDetect(SwipeData data)
    {
        if (_disabled)
        {
            return;  
        }

        if (!_blockedSides)
        {
            if (data.Direction == SwipeDirection.Right)
            {
                _formulaHandler.ShowNextFormula();
            }
            else if (data.Direction == SwipeDirection.Left)
            {
                _formulaHandler.ShowPreviousFormula();
            }
        }
        
        if (!_blockedDown)
        {
            if (data.Direction == SwipeDirection.Down)
            {
                _roll.SlideDown();
            }
        }
    }
}
