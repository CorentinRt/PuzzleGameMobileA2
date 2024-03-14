using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseGooglePlayCheck : MonoBehaviour
{
    [SerializeField] private Color _colorValid;
    [SerializeField] private Color _colorInvalid;

    [SerializeField] private Image _image;

    private void Update()
    {
        if (PlayGamesManager.Instance != null)
        {
            if (PlayGamesManager.Instance.UseGooglePlay)
            {
                _image.color = _colorValid;
            }
            else
            {
                _image.color = _colorInvalid;
            }
        }
    }

}
