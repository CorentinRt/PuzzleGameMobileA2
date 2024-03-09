using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class TransitionLifeDisplay : MonoBehaviour
{
    [SerializeField] private float _cooldownBeforeDisplay;

    [SerializeField] private RectTransform _lifeTransitionPanel;

    [SerializeField] private TextMeshProUGUI _lastNumberText;
    [SerializeField] private TextMeshProUGUI _currentNumberText;

    [SerializeField] private RectTransform _textContainer;
    [SerializeField] private RectTransform _target;

    [SerializeField] private int _currentNumber;
    private int _lastNumber;

    private Coroutine _slideNumberCoroutine;

    [SerializeField] private float _slideSpeed;

    private bool _hasSkipped;

    public event Action OnTransitionLifeEnded;



    [Button]
    private void DisplayLifeTransitionPanel()
    {
        Vector3 tempVector = _textContainer.localPosition;

        tempVector.y = 0;

        _textContainer.localPosition = tempVector;

        _lifeTransitionPanel.gameObject.SetActive(true);

        ChangeCurrentNumber(PlayerManager.Instance.GetLivesNumber());

        SlideNumber();
    }
    [Button]
    private void HideLifeTransitionPanel()
    {
        Vector3 tempVect = _textContainer.localPosition;

        tempVect.y = 0;

        _textContainer.localPosition = tempVect;

        _lifeTransitionPanel.gameObject.SetActive(false);
    }
    private void ChangeCurrentNumber(int value)
    {
        _lastNumberText.text = _lastNumber.ToString();
        _currentNumber = value;
        _lastNumber = _currentNumber;
        _currentNumberText.text = _currentNumber.ToString();

    }
    [Button]
    private void TestChangeNumber()
    {
        _lastNumberText.text = _lastNumber.ToString();
        _currentNumberText.text = _currentNumber.ToString();

        _lastNumber = _currentNumber;
    }
    [Button]
    private void SlideNumber()
    {
        if (_slideNumberCoroutine != null)
        {
            _slideNumberCoroutine = null;
        }
        _slideNumberCoroutine = StartCoroutine(SlideNumberCoroutine(_currentNumber));
    }

    private void StartCooldown()
    {
        StartCoroutine(CooldownBeforeDisplayCoroutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnPhase2Ended += StartCooldown;

        _currentNumber = PlayerManager.Instance.GetLivesNumber();
        _lastNumber = _currentNumber;

        _lastNumberText.text = _lastNumber.ToString();
        _currentNumberText.text = _currentNumber.ToString();

        Vector3 tempVector = _textContainer.localPosition;

        tempVector.y = 0;

        _textContainer.localPosition = tempVector;

        _lifeTransitionPanel.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnPhase2Ended -= StartCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        _hasSkipped = false;
        if (Input.GetMouseButtonDown(0) && _lifeTransitionPanel.gameObject.activeSelf)
        {
            _hasSkipped = true;
        }
    }

    IEnumerator SlideNumberCoroutine(int number)
    {
        Debug.Log("Start slide");

        Vector3 currentPosition = _textContainer.localPosition;

        Vector3 targetPosition = _target.localPosition;

        float percent = 0f;

        while (!_hasSkipped && percent < 1f)
        {
            Debug.Log("Sliding");

            currentPosition = Vector3.Lerp(currentPosition, targetPosition, percent);

            _textContainer.localPosition = currentPosition;

            percent += Time.deltaTime * _slideSpeed;

            yield return null;
        }

        _textContainer.localPosition = targetPosition;

        Debug.Log("End slide");

        OnTransitionLifeEnded?.Invoke();

        _lifeTransitionPanel.gameObject.SetActive(false);

        _slideNumberCoroutine = null;

        yield return null;
    }

    IEnumerator CooldownBeforeDisplayCoroutine()
    {
        yield return new WaitForSeconds(_cooldownBeforeDisplay);

        DisplayLifeTransitionPanel();

        yield return null;
    }
}
