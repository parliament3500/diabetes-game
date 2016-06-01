using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    private Image _flashImage;
    public Color flashColor = new Color(1f, 0f, 0f, 0.25f);
    public float flashSpeed;

    void Start()
    {
        _flashImage = GetComponent<Image>();
    }

    void Update()
    {
        if (InsulinEffect.damaged || EnemyEffect.damaged)
        {
            _flashImage.color = flashColor;
        }
        if(Player.IsGameoverTextVisible == true)
        {
            _flashImage.color = new Color(1f,0f,0f,1f);
        }
        else
        {
            _flashImage.color = Color.Lerp(_flashImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        Player.IsGameoverTextVisible = false;
        InsulinEffect.damaged = false;
        EnemyEffect.damaged = false;
    }
}

