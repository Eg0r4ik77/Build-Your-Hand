using System.Collections;
using UnityEngine;

public static class VictimPusher
{
    public static IEnumerator Push(Transform victimTransform, Vector3 pushVector, float pushTime)
    {
        Vector3 start = victimTransform.position;
        Vector3 end = new Vector3((start + pushVector).x, start.y, (start + pushVector).z);
            
        float timer = pushTime;
            
        while (timer > 0)
        {
            float interpolant = (pushTime - timer) / pushTime;
            victimTransform.position = Vector3.Lerp(start, end, interpolant);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}