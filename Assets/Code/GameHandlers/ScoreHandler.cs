using System.Collections.Generic;

public class ScoreHandler
{
    static int _totalLevelActions;
    static int _hitCounter;
    static float _score; public static float Score { get => _score; }

    public ScoreHandler()
    {
        LevelHandler.StartingLevel += ResetScore;  
        LevelHandler.StartingLevel += SetTotalLevelActions;
    }

    void ResetScore()
    {
        _hitCounter = 0;
        _score = 0;
    }

    void SetTotalLevelActions()
    {
        _totalLevelActions = 0;
        foreach (KeyValuePair<int, Exercise> exercise in ExerciseHandler.Exercises)
        {
            if (exercise.Value.level == LevelHandler.CurrentLevel)
            {
                _totalLevelActions += exercise.Value.pedagogicalActions;
            }
        }
    }

    public static void AddScore()
    {
        if (_score >= 1)
        {
            return;
        }

        _hitCounter++;
        
        _score = _hitCounter / (float)_totalLevelActions;
    }

    public static void ReduceScore()
    {
        if (_score <= 0)
        {
            return;
        }

        _hitCounter--;
        
        _score = _hitCounter / (float)_totalLevelActions;
    }
}
