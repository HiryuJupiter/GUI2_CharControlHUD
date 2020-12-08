using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit: This script is based off a comment on Unity Answers website
//Credit: https://answers.unity.com/questions/772235/cannot-serialize-color.html


namespace MyNameSpace
{
    [System.Serializable]
    public class SerializableColor
    {

        public float[] colorStore = new float[4] { 1F, 1F, 1F, 1F };
        public Color Color
        {
            get { return new Color(colorStore[0], colorStore[1], colorStore[2], colorStore[3]); }
            set { colorStore = new float[4] { value.r, value.g, value.b, value.a }; }
        }

        public static implicit operator Color(SerializableColor c)
        {
            return c.Color;
        }

        public static implicit operator SerializableColor(Color color)
        {
            return new SerializableColor { Color = color };
        }
    }
}