using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnim : MonoBehaviour
{
    public AnimationCurve showcurve;
    public AnimationCurve hidecurve;
    public float animSpeed;
    public GameObject panel;

    public void ShowPanel()
    {
        if(panel != null)
        {
            Debug.Log("Starting ShowPanel Coroutine");
            StartCoroutine(ShowPanel(panel));
        }
        else
        {
            Debug.LogError("Panel is not assigned in PanelAnim script");
        }
    }

    public void HidePanel()
    {
        StartCoroutine(HidePanel(panel));
    }
    
    IEnumerator ShowPanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * showcurve.Evaluate(timer);
            timer += Time.deltaTime * animSpeed;
            yield return null;
        }
    }
    
    IEnumerator HidePanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * hidecurve.Evaluate(timer);
            timer += Time.deltaTime * animSpeed;
            yield return null;
        }
    }

    // public void Update()
    // {
    //     if (pauseButton.onClick)
    //     {
    //         StartCoroutine(ShowPanel(panel));
    //     }
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         StartCoroutine(HidePanel(panel));
    //     }
    // }
}
