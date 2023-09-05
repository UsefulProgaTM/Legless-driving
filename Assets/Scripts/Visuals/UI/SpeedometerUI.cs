using LeglessDriving;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpeedometerUI : MonoBehaviour
{
    [Inject]
    private CurrentCarStats _currentCarStats;

    [Inject]
    private CarStats _carStats;

    [SerializeField]
    private Image[] milisecondsImages;
    [SerializeField]
    private Image[] secondsImages;
    [SerializeField] 
    private Image[] minutesImages;

    [SerializeField]
    private Sprite[] numbers;

    private float _countingTimeStart;
    private bool _timerWorking = false;


    [SerializeField]
    private GameObject _handbrakeIcon;

    private float maxNeddleAngle = 280;

    [SerializeField]
    private GameObject speedNeedle;

    [SerializeField]
    private GameObject rpmNeedle;

    private void Awake()
    {
        StartTimer();
    }

    private void FixedUpdate()
    {
        speedNeedle.transform.localRotation = Quaternion.Euler(0, _currentCarStats.speed / _carStats.maxSpeed * maxNeddleAngle, 0);
        rpmNeedle.transform.localRotation = Quaternion.Euler(0, _currentCarStats.rpm / _carStats.maxRPM * maxNeddleAngle, 0);
        _handbrakeIcon.SetActive(_currentCarStats.handbrakePulled);
        if(_timerWorking)
            UpdateTimer();
    }

    private void SetMiliseconds()
    {
        milisecondsImages[0].sprite = numbers[CalculateMiliseconds0100()];
        milisecondsImages[1].sprite = numbers[CalculateMiliseconds1000()];
        milisecondsImages[2].sprite = numbers[Random.Range(0, numbers.Length)];
        milisecondsImages[3].sprite = numbers[Random.Range(0, numbers.Length)];
    }
    private void SetSeconds()
    {
        secondsImages[0].sprite = numbers[CalculateSeconds01()];
        secondsImages[1].sprite = numbers[CalculateSeconds10()];
    }
    private void SetMinutes()
    {
        minutesImages[0].sprite = numbers[CalculateMinutes01()];
        minutesImages[1].sprite = numbers[CalculateMinutes10()];
    }

    private void UpdateTimer()
    {
        SetMiliseconds();
        SetSeconds();
        SetMinutes();
    }

    public void StartTimer()
    {
        _countingTimeStart = Time.time;
        _timerWorking = true;
    }
    public void StopTimer()
    {
        if(!_timerWorking)
        {
            ResetTime();
        }
        _timerWorking = false;
    }

    private void ResetTime()
    {
        milisecondsImages[0].sprite = 
        milisecondsImages[1].sprite = 
        milisecondsImages[2].sprite =
        milisecondsImages[3].sprite = 
        secondsImages[0].sprite = 
        secondsImages[1].sprite = 
        minutesImages[0].sprite = 
        minutesImages[1].sprite = numbers[0];
    }

    private int CalculateMiliseconds0100()
    {
        float temp = (Time.time - _countingTimeStart) % 1;
        temp *= 100;
        temp %= 10;
        return (int)temp;
    }
    private int CalculateMiliseconds1000()
    {
        float temp = (Time.time - _countingTimeStart) % 1;
        temp *= 100;
        temp /= 10;
        return (int)temp;
    }

    private int CalculateSeconds01()
    {
        return (int)(Time.time - _countingTimeStart) % 60 % 10;
    }
    private int CalculateSeconds10()
    {
        return (int)(Time.time - _countingTimeStart) % 60 / 10;
    }

    private int CalculateMinutes01()
    {
        return (int)(Time.time - _countingTimeStart) / 60 % 10;
    }
    private int CalculateMinutes10()
    {
        return (int)(Time.time - _countingTimeStart) / 60 / 10;
    }
}
