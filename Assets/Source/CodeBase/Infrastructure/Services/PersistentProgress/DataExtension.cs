using UnityEngine;

namespace EpicRPG.Services.PersistentData
{
    public static class DataExtension
    {
        public static Vector3Data ToVector3Data(this Vector3 vector)
           => new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 ToVector3(this Vector3Data vector3Data)
            => new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
        public static QuaternionData ToQuaternionData(this Quaternion quaternion)
           => new QuaternionData(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        public static Quaternion ToQuaternion(this QuaternionData quaternion)
          => new Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

    }
}
