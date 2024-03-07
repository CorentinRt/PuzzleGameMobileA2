using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFadeAppear : MonoBehaviour
{

    [SerializeField] private List<SpriteRenderer> _spriteRenderers;

    [SerializeField] private float _fadeSpeed;

    [SerializeField] private float _cooldownBeforeFadeAppear;


    private void Awake()
    {
        Color tempColor = _spriteRenderers[0].color;

        tempColor.a = 0f;

        _spriteRenderers[0].color = tempColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeAppearAnim());
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

            percent += Time.deltaTime;

            yield return null;
        }

        tempColor.a = 1f;

        _spriteRenderers[0].color = tempColor;

        yield return null;
    }
}
