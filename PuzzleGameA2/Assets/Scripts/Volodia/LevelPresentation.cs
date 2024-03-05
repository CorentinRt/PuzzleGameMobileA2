using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;

public class LevelPresentation : MonoBehaviour
{
    private Level _level;
    [SerializeField] private float _waitTime;
    [Header("References")]
    [SerializeField] private GameObject _presentationPanel;
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private TextMeshProUGUI _nbLivesText;
    [SerializeField] private Transform _shapeDisplayPanel;
    [SerializeField] private GameObject _shapeDisplayPrefab;

    private bool _hasPressedToSkip;

    private void Start()
    {
        LevelManager.Instance.OnLevelFinishedLoad += SetupPresentation;
        GameManager.Instance.OnLevelPresent += Present;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnLevelFinishedLoad -= SetupPresentation;
        GameManager.Instance.OnLevelPresent -= Present;
    }
    private void Update()
    {
        if (_hasPressedToSkip)
        {
            _hasPressedToSkip = false;
        }
        if (Input.GetMouseButtonDown(0) && _presentationPanel.activeSelf)
        {
            _hasPressedToSkip = true;
        }
    }
    private void Present()
    {
        _presentationPanel.SetActive(true);
        StartCoroutine(ClosePanel());
    }

    public IEnumerator ClosePanel()
    {
        //yield return new WaitForSeconds(_waitTime);

        float current = 0f;

        while (current < _waitTime && !_hasPressedToSkip)
        {
            current += Time.deltaTime;

            yield return null;
        }

        _presentationPanel.SetActive(false);
        GameManager.Instance.ChangeGamePhase(PhaseType.PlateformePlacement);

        yield return null;
    }


    private void SetupPresentation()
    {
        _level = LevelManager.Instance.GetCurrentLevel();
        _levelName.text = "Level" + _level.GetID;
        _nbLivesText.text = "x" + _level.LevelInfo.NbPlayerLives;
        List<Shape> shapes = _level.LevelInfo.Shapes;
        foreach (Shape shape in shapes)
        {
            ShapeDisplay shapeDisplay = Instantiate(_shapeDisplayPrefab, _shapeDisplayPanel).GetComponent<ShapeDisplay>();
            shapeDisplay.SetShape(shape);
        }
    }
}
