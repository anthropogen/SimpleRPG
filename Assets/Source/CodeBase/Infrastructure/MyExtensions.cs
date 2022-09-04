using UnityEngine;


public static class MyExtensions
{
    public static T ToDeserialize<T>(this string json)
        => JsonUtility.FromJson<T>(json);

    public static string ToSerialize(this object objectToSerialize)
        => JsonUtility.ToJson(objectToSerialize);
}
