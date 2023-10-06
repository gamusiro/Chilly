using UnityEngine;

public class CustomLabelAttribute : PropertyAttribute
{
    public readonly string Value;

    public CustomLabelAttribute(string value)
    {
        Value = value;
    }
}

public class ColorMeAttribute : PropertyAttribute
{
    public Color color;

    public ColorMeAttribute(float r, float g, float b, float a)
    {
        color = new Color(r, g, b, a);
    }
}