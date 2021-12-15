using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hmmmm
{
    [Serializable]
    public sealed class SavedData
    {
        public string Name { get; set; }
        public Vector3Serializable Position { get; set; }
        public bool IsEnabled { get; set; }
        public List<Vector3> CollectNessesary { get; set; }
        public List<Vector3> BadBonus { get; set; }
        public List<Vector3> GoodBonus { get; set; }

        public override string ToString() => $"Name {Name} Position {Position} IsVisible {IsEnabled}";
    }
        
    [Serializable]
    public struct Vector3Serializable
    {
        public float X;
        public float Y;
        public float Z;

        private Vector3Serializable(float valueX, float valueY, float valueZ)
        {
            X = valueX;
            Y = valueY;
            Z = valueZ;
        }

        public static implicit operator Vector3(Vector3Serializable value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }
    
        public static implicit operator Vector3Serializable(Vector3 value)
        {
            return new Vector3Serializable(value.x, value.y, value.z);
        }
        
        public override string ToString() => $" (X = {X} Y = {Y} Z = {Z})";
    }

}