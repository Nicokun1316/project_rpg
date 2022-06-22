using UnityEngine;

public class TimeBasedEffect : MonoBehaviour
{
    private static readonly int TimeOffset = Shader.PropertyToID("_time_offset");

    private void OnEnable() {
        GetComponent<SpriteRenderer>().material.SetFloat(TimeOffset, Time.time);
    }
}
