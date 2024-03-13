using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFadeAppear : MonoBehaviour
{

    [SerializeField] private List<SpriteRenderer> _spriteRenderers;

    [SerializeField] private List<LineRenderer> _lineRenderers;
    [SerializeField] private List<GameObject> _objectFX;

    [SerializeField] private float _fadeSpeed;

    [SerializeField] private float _cooldownBeforeFadeAppear;
    private bool _isDefaultActive;


    private void Awake()
    {
        if (_spriteRenderers.Count != 0)
        {
            Color tempColor = _spriteRenderers[0].color;

            tempColor.a = 0f;

            foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer.color = tempColor;
            }
        }
        if (_lineRenderers.Count != 0)
        {
            Color tempColor = _lineRenderers[0].startColor;

            tempColor.a = 0f;

            foreach(var lineRenderer in _lineRenderers)
            {
                lineRenderer.startColor = tempColor;
                lineRenderer.endColor = tempColor;

                lineRenderer.sortingOrder = -5;
            }
        }
        if (_objectFX.Count != 0)
        {
            _isDefaultActive = _objectFX[0].activeSelf;
            foreach(var objectFX in _objectFX)
            {
                objectFX.SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Actual alpha : " + _spriteRenderers[0].color.a);
        StartCoroutine(FadeAppearAnim());
        Debug.Log("Start fade appear anim");
    }

    IEnumerator FadeAppearAnim()
    {
        yield return new WaitForSeconds(_cooldownBeforeFadeAppear);

        float percent = 0f;

        Color tempColor = _spriteRenderers[0].color;

        while (percent < 1f)
        {
            tempColor.a = percent;

            foreach (var renderer in _spriteRenderers)
            {
                renderer.color = tempColor;
            }
            foreach (var lineRenderer in _lineRenderers)
            {
                lineRenderer.startColor = tempColor;
                lineRenderer.endColor = tempColor;
            }

            percent += Time.deltaTime;

            yield return null;
        }

        tempColor.a = 1f;

        _spriteRenderers[0].color = tempColor;

        foreach (var lineRenderer in _lineRenderers)
        {
            lineRenderer.sortingOrder = 0;
        }

        foreach (GameObject objectFX in _objectFX)
        {
            objectFX.SetActive(_isDefaultActive);
        }

        yield return null;
    }
}
