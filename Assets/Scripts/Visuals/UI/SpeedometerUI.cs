using TMPro;
using UnityEngine;

public class SpeedometerUI : MonoBehaviour
{
    [SerializeField]
    private CurrentCarStats currentCarStats;

    [SerializeField]
    private TextMeshProUGUI text;

    private void Update()
    {
        text.text = Mathf.Abs((int)currentCarStats.speed * 10) + "km/h";
    }
}
