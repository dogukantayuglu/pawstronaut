using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        fixedDeltaTime = Time.fixedDeltaTime;
    }
    
    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
        // Adjust fixed delta time according to timescale
        // The fixed delta time will now be 0.02 real-time seconds per frame
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
    }

    public float GetTimeScale()
    {
        var time = Time.timeScale;
        return time;
    }
}
