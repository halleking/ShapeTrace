using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private ShapesController shapesController;
    [SerializeField] private GameObject line;
    [SerializeField] private TMP_Text instructionsText;

    private List<LineController> _lines = new List<LineController>();
    private LineController _currentLine;
    private List<Vector2> _checkpoints;
    private bool _isComplete = false;

    private void Start()
    {
        _checkpoints = shapesController.CurrentShape.Checkpoints;
        instructionsText.text = $"Plant the seeds in a {shapesController.CurrentShape.ShapeName}";
    }

    private void Update()
    {
        if (_isComplete)
        {
            return;
        }


        if (Input.GetMouseButton(0))
        {
           if (_currentLine == null)
            {
                // Instantiate a new Line Renderer object at the mouse position
                GameObject lineObj = Instantiate(line, transform);
                var newLine = lineObj.GetComponent<LineController>();
                newLine.Draw(_checkpoints);
                _lines.Add(newLine);
                _currentLine = newLine;
                newLine.OnShapeFilled += HandleShapeFilled;
            }
        }
        else
        {
            if (_currentLine != null)
            {
                _currentLine.StopDraw();
                _currentLine = null;
            }
        }
    }

    private void HandleShapeFilled()
    {
        _isComplete = true;
        instructionsText.text = $"Great Job!";
        shapesController.CurrentShape.PlayGrowAnimation();


        StartCoroutine(Next());
    }

    private IEnumerator Next()
    {
        // delay before continuing
        yield return new WaitForSeconds(5f);

        ResetLines();
        shapesController.InitShape();
        if (shapesController.CurrentShape != null)
        {
            _checkpoints = shapesController.CurrentShape.Checkpoints;
            instructionsText.text = $"Plant the seeds in a {shapesController.CurrentShape.ShapeName}";
        }
        else
        {
            _checkpoints.Clear();
            instructionsText.text = "";
        }
        _isComplete = false;

    }


    private void ResetLines()
    {
        foreach (var line in _lines)
        {
            line.OnShapeFilled -= HandleShapeFilled;
            DestroyImmediate(line.gameObject);
        }

        _lines.Clear();
    }

}
