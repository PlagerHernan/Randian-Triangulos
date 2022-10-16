using System.Collections.Generic;

[System.Serializable]
public struct TriangleList
{
    public List<TriangleType> triangleTypes;
}

[System.Serializable]
public struct TriangleType
{
    public int index; 
    public SideImage[] sideImages;
    public RulerImage ruler;
}

[System.Serializable]
public struct SideImage
{
    public float xPosition;
    public float yPosition;
}

[System.Serializable]
public struct RulerImage
{
    public float xPosition;
    public float yPosition;
    public float rotation;
    public bool shortRuler;
}


