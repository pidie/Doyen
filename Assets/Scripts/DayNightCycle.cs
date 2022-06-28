using UnityEngine;

/// <summary>
/// Rotates a game object that controls the directional light, simulating a day/night cycle
/// </summary>
public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float cycleSpeed;
    [SerializeField] private float timeToFadeMusic;

    private bool _musicStopped;
    private void Update()
    {
        var rot = transform.rotation;
        
        if (rot.x > 180f)
            transform.rotation = Quaternion.Euler(rot.x - 360, rot.y, rot.z);
        
        transform.Rotate(new Vector3(cycleSpeed * Time.deltaTime, 0, 0));
        
        if (Mathf.Abs(transform.rotation.x) > 90)
        {
            _musicStopped = true;
            Audio.AudioManager.onFadeMusic(timeToFadeMusic);
        }

        if (_musicStopped && Mathf.Abs(transform.rotation.x) < 90)
        {
            _musicStopped = false;
            Audio.AudioManager.onPlaySound("Level Music", false);
        }
    }
}
