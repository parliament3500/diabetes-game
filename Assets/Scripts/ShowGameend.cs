using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowGameend : MonoBehaviour
{
    public Text deadText;

    public void EnableText()
    {
        deadText.enabled = true;
    }

    public void DisableText()
    {
        deadText.enabled = false;
    }
}

