using System;

namespace SimpleRPG.Services.PersistentData
{
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data PlayerPosition;
        public QuaternionData PlayerRotation;

        public PositionOnLevel(string level, Vector3Data playerPosition)
        {
            Level = level;
            PlayerPosition = playerPosition;
        }

        public PositionOnLevel(string level, Vector3Data playerPosition, QuaternionData playerRotation) : this(level, playerPosition)
        {
            PlayerRotation = playerRotation;
        }
    }
}