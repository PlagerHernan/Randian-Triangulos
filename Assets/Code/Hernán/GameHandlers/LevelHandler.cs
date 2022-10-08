using UnityEngine;
using System.Collections.Generic;

public class LevelHandler : MonoBehaviour
{
    public delegate void VoidDelegate(); public static event VoidDelegate StartingLevel;
    static int _currentLevel; public static int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    static int _maxLevel; public static int MaxLevel { get => _maxLevel; }

    void Awake() 
    {
        ExerciseHandler.ReadExercises += SetMaxLevel;
        ExerciseHandler.ReadExercises += OnStartingLevel;
    }

    void SetMaxLevel()
    {
        foreach (KeyValuePair<int, Exercise> exercise in ExerciseHandler.Exercises)
        {
            if (exercise.Value.level > _maxLevel)
            {
                _maxLevel = exercise.Value.level;
            }
        }
    }

    public static void OnStartingLevel()
    {
        StartingLevel?.Invoke();
    }
}
