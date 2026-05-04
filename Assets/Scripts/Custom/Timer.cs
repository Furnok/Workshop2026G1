using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    #region References
    [Header("References")]

    [SerializeField] private TextMeshProUGUI textChrono;

    [SerializeField] private GameObject timer;
    #endregion


    #region Parameters
    [Header("Parameters")]

    [SerializeField] private int second;

    [SerializeField] private int maxtime;
    #endregion

    private bool timerstarted = false;

    private Coroutine timerCoroutine;

    private void Awake()
    {
        Instance = this;
        timer.SetActive(false);
    }

    public void ChronoStart()
    {
        second = maxtime;

        System.TimeSpan time = System.TimeSpan.FromSeconds(second);

        string formatted = time.ToString(@"mm\:ss");

        textChrono.text = formatted;

        timer.SetActive(true);
        timerstarted = true;
        timerCoroutine = StartCoroutine(Chronometers());
    }
    public IEnumerator Chronometers()
    {
        while (timerstarted)
        {
            yield return new WaitForSeconds(1);

            second--;

            System.TimeSpan time = System.TimeSpan.FromSeconds(second);

            string formatted = time.ToString(@"mm\:ss");

            textChrono.text = formatted;

            if (second <= 0)
            {
                timerstarted = false;

                yield break;
            }
        }
    }
}
