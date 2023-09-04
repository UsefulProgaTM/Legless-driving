using LeglessDriving;
using TMPro;
using UnityEngine;
using Zenject;

public class SpeedometerUI : MonoBehaviour
{
    [Inject]
    private CurrentCarStats _currentCarStats;

    [Inject]
    private CarStats _carStats;

    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private TextMeshProUGUI rpmText;

    private float maxNeddleAngle = 280;

    [SerializeField]
    private GameObject speedNeedle;

    [SerializeField]
    private GameObject rpmNeedle;

    private void Update()
    {
        //float speed = _currentCarStats.speed * 10;
        //speedText.text = Mathf.Abs((int)speed) + "km/h";
        //rpmText.text = _currentCarStats.rpm.ToString();

        speedNeedle.transform.localRotation = Quaternion.Euler(0,_currentCarStats.speed / _carStats.maxSpeed * maxNeddleAngle,0);
        rpmNeedle.transform.localRotation = Quaternion.Euler(0,_currentCarStats.rpm / _carStats.maxRPM * maxNeddleAngle,0);

    }
}
