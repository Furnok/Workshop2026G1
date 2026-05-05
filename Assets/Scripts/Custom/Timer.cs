using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [Header("References")]

    [SerializeField] private TextMeshProUGUI textChrono;

    [SerializeField] private GameObject timer;

    [Header("Parameters")]

    [SerializeField] private int second;

    [SerializeField] private int maxtime;

    private bool timerstarted = false;

    private Coroutine timerCoroutine = null;

    private void Awake()
    {
        Instance = this;

        timer.SetActive(false);
    }

    public void ChronoStart()
    {
        second = maxtime;

        UpdateDisplay();

        timer.SetActive(true);
        timerstarted = true;

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        timerCoroutine = StartCoroutine(Chronometers());
    }

    public void ChronoStop()
    {
        timer.SetActive(false);
        timerstarted = false;

        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private IEnumerator Chronometers()
    {
        while (timerstarted)
        {
            yield return new WaitForSeconds(1);

            second--;

            UpdateDisplay();

            if (second <= 0)
            {
                timerstarted = false;

                yield break;
            }
        }
    }

    private void UpdateDisplay()
    {
        int minutes = second / 60;
        int seconds = second % 60;
        textChrono.text = $"{minutes:00}:{seconds:00}";
    }
}
