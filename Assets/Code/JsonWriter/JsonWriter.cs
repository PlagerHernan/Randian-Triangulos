using UnityEngine;
using System.IO;

public class JsonWriter
{
    public static void SetListElementsToJson(TriangleList triangleList, string jsonFileName)
    {
        string _jsonString = JsonUtility.ToJson(triangleList);
        File.WriteAllText("Assets/Resources/Json/" + jsonFileName, _jsonString);

        Debug.Log(jsonFileName + " guardado");
    }
}
