using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private float _timer;
    [SerializeField] private UnityEvent _eventAfterLoseTime;

    private float _time;
    private bool _isPlaying = false;

    void Start()
    {
        _time = _timer;
    }

    void Update()
    {
  
        if( _isPlaying)
        {
            if (_time > 0)
            {
                 _time -= Time.deltaTime;
                _timerSlider.value = _time/_timer;
            }
            else
            {
                _eventAfterLoseTime.Invoke();
            }
        }
        else
        {
            if (Input.anyKeyDown)
                _isPlaying = true;
        }

    }
}
