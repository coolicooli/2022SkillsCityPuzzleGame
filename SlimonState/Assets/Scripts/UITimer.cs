using System.Collections;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    [SerializeField] private float startTime; // initial time
    [SerializeField] private float decreaseTimeAmount; //every seconds amount
    [SerializeField] private float decreaseScore; // decrease (score)time by amount

    private float currentTimer; // current value to show

    // Start is called before the first frame update
    private void Start()
    {
        currentTimer = startTime;
        
        textTimer.text = currentTimer.ToString();
        IEnumerator coroutine = WaitTimer(decreaseTimeAmount);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitTimer(float decreaseAmount)
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseAmount);
            ShowTimer();
        }
    }

    // Show the timer on screen
    void ShowTimer()
    {
        currentTimer -= decreaseScore;
        if (currentTimer < 0)
        {
            currentTimer = 0;
        }
        textTimer.text = currentTimer.ToString();
    }
}
