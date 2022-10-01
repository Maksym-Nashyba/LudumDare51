using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    public static class Utils
    {
        public static Vector3 GetPointInRadiusFlat(Vector3 center, float radius)
        {
            Vector2 offset = Vector2.up * Random.Range(0f, radius);
            offset = offset.RotatedBy(Random.Range(0f, 359f));
            return center + new Vector3(offset.x, 0, offset.y);
        }
    }
}