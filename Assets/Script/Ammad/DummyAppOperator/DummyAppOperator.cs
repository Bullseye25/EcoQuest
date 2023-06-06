using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DummyAppOperator : MonoBehaviour
{
    [SerializeField] private float colorChangeSpeed = 1f;

    [Space]
    [SerializeField] private Image screen;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color endColor;

    private void Awake()
    {
        screen.color = endColor;
        UponGameStart();
    }

    public void UponReset()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void UponGameStart()
    {
        StartCoroutine(ManageScreenColor(true));
    }

    public void UponGameEnd()
    {
        StartCoroutine(ManageScreenColor(false));
    }

    IEnumerator ManageScreenColor(bool isStarting)
    {
        Color currentColor = screen.color;
        float t = 0f;

        while ((isStarting && currentColor != defaultColor) || (!isStarting && currentColor != endColor))
        {
            t += Time.deltaTime * colorChangeSpeed;
            if (isStarting)
            {
                currentColor = Color.Lerp(currentColor, defaultColor, t);
            }
            else
            {
                currentColor = Color.Lerp(currentColor, endColor, t);
            }

            screen.color = currentColor;
            yield return null;
        }
    }
}
