using TMPro;
using UnityEngine;
using Zenject;

public class SpeedometerUI : MonoBehaviour
{
    [Inject]
    private CurrentCarStats currentCarStats;

    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private TextMeshProUGUI rpmText;

    private void Update()
    {
        float speed = currentCarStats.speed * 10;
        speedText.text = Mathf.Abs((int)speed) + "km/h";
        rpmText.text = currentCarStats.rpm.ToString();
    }
}
