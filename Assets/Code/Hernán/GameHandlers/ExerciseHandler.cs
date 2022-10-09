using UnityEngine;
using System.Collections.Generic;

public class ExerciseHandler
{
    public delegate void VoidDelegate(); 
    public static event VoidDelegate ReadExercises, EstablishedCurrentExercise;
    static Dictionary<int,Exercise> _exercises; public static Dictionary<int, Exercise> Exercises { get => _exercises; }
    static Exercise _currentExercise; public static Exercise CurrentExercise { get => _currentExercise; }
    static int _exerciseCount;
    const string _csvName = "Test Plan - Problemas con triángulos - Ejercicios";

    public ExerciseHandler()
    {
        _exercises = new Dictionary<int, Exercise>(); 
    }

    public void SetExercises()
    {
        TextAsset textFile = Resources.Load<TextAsset>(_csvName);
        string[,] grid = CSVReader.SplitCsvGrid(textFile.text);

        int totalLines = grid.GetUpperBound(1)-1;
        int totalColumns = grid.GetUpperBound(0)-1;

        //recorre cada linea excepto linea 0 y 1 (encabezados)
        for (int y = 2; y < totalLines; y++) 
        {	
            string[] data = new string[totalColumns];

            //recorre cada campo de la linea
			for (int x = 0; x < totalColumns; x++) 
            {
                data[x] = grid[x,y]; 
			}

            Exercise exercise = new Exercise(data);
            _exercises.Add(exercise.id, exercise);
		}

        ReadExercises?.Invoke();

        /* foreach (KeyValuePair<int, Exercise> exercise in _exercises)
        {
            print(exercise.Value.ToString());
        } */
    }

    public static void SetCurrentExercise()
    {
        _currentExercise = _exercises[_exerciseCount];
        EstablishedCurrentExercise?.Invoke(); 
        _exerciseCount++;
    }
}

public struct Exercise
{
    public int id;
    public int level;
    public string triangleID;
    public Height height;
    public List<Formula> unclearFormulas;
    public List<Formula> clearFormulas;
    public List<string> equationSteps;
    public List<Side> sides;
    public int pedagogicalActions;

    public Exercise(string[] data)
    {
        int count = 0;

        int.TryParse(data[count++], out id);
        int.TryParse(data[count++], out level);
        triangleID = data[count++];

        //altura
        if (data[count] == "-")
        {
            height = new Height('0', 0f);
        }
        else
        {
            float heightValue;
            char baseSide;
            
            bool parsedOk = float.TryParse(data[count].Split('_')[0], out heightValue);

            if (!parsedOk)
            {
                Debug.LogError("Error al convertir string a float");
            }
            
            char.TryParse(data[count].Split('_')[1], out baseSide);
            
            height = new Height(baseSide, heightValue);
        }
        count++;

        //formulas no despejadas (si no las hay, queda en null)
        if (data[count] == "-")
        {
            unclearFormulas = null;
            count = count+3;
        }
        else
        {
            unclearFormulas = new List<Formula>();
            unclearFormulas.Add(new Formula(data[count++], true));
            unclearFormulas.Add(new Formula(data[count++], false));
            unclearFormulas.Add(new Formula(data[count++], false));
        }

        //formulas despejadas
        clearFormulas = new List<Formula>();
        clearFormulas.Add(new Formula(data[count++], true));
        clearFormulas.Add(new Formula(data[count++], false));
        clearFormulas.Add(new Formula(data[count++], false));

        //pasos de la ecuación
        equationSteps = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            if (data[count] != "-")
            {
                equationSteps.Add(data[count]);
            }

            count++;
        }

        //lados
        sides = new List<Side>();
        char variable;
        float sideValue;
        for (int i = count; i < count + 3; i++)
        {
            if (data[i].Contains("_"))
            {
                char.TryParse(data[i].Split('_')[0], out variable); 
                float.TryParse(data[i].Split('_')[1], out sideValue);
            }
            else
            {
                variable = ' ';
                float.TryParse(data[i], out sideValue);
            }
            
            sides.Add(new Side(variable, sideValue));
        }
        count = count + 3;

        pedagogicalActions = int.Parse(data[count]);
    }

    public override string ToString()
    {
        string text = "";

        //text = "Id: " + id + " - Level: " + level + " - triangleID: " + triangleID;

        foreach(var field in this.GetType().GetFields())
        {
            text += field.Name + ": " + field.GetValue(this) + "\n";
        }

        return text;
    }
}

public struct Formula
{
    public string equation;
    public bool correct;

    public Formula(string equationText, bool isCorrect)
    {
        equation = equationText;
        correct = isCorrect;
    }
}

public struct Side
{
    public char variable;
    public float value;

    public Side(char letter, float number)
    {
        variable = letter;
        value = number;
    }
}

public struct Height
{
    public char baseSide;
    public float value;

    public Height(char letter, float number)
    {
        baseSide = letter;
        value = number;
    }
}
