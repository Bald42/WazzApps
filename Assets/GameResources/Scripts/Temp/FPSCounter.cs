using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    int counter = 0;

    void Start()
    {
        StartCoroutine(Check());
    }

    void Update()
    {
        counter++;
    }

    private IEnumerator Check()
    {
        while (true)
        {
            text.text = counter.ToString();
            counter = 0;
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
