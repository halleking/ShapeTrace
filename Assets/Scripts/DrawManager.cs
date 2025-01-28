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
    private int _checkpointIndex = 0;
    private bool _isComplete = false;

    private void Start()
    {
        var indefArticle = shapesController.CurrentShape.VowelSound ? "an" : "a";
        instructionsText.text = $"Plant the seeds in {indefArticle} {shapesController.CurrentShape.ShapeName}";
        _checkpoints = shapesController.CurrentShape.Checkpoints;
        HighlightNextNode(_checkpointIndex);
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
                newLine.OnCheckpointFilled += HandleCheckpointFilled;
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

    private void HandleCheckpointFilled()
    {
        if (shapesController.CurrentShape.NodeControllers.Count > _checkpointIndex)
        {
            shapesController.CurrentShape.NodeControllers[_checkpointIndex].PlaceSeed();
            shapesController.CurrentShape.NodeControllers[_checkpointIndex].Highlight(false);
            HighlightNextNode(_checkpointIndex + 1);
            _checkpointIndex++;
        }
    }

    private void HandleShapeFilled()
    {
        _isComplete = true;
        instructionsText.text = $"Great Job!";
        shapesController.CurrentShape.PlayGrowAnimation();


        StartCoroutine(Next());
    }

    private void HighlightNextNode(int nextIndex)
    {
        if (shapesController.CurrentShape.NodeControllers.Count > nextIndex)
        {
            shapesController.CurrentShape.NodeControllers[nextIndex].Highlight(true);
        }
    }

    private IEnumerator Next()
    {
        // delay before continuing
        yield return new WaitForSeconds(5f);

        ResetShape();
        shapesController.InitShape();
        if (shapesController.CurrentShape != null)
        {
            _checkpoints = shapesController.CurrentShape.Checkpoints;
            var indefArticle = shapesController.CurrentShape.VowelSound ? "an" : "a";
            instructionsText.text = $"Plant the seeds in {indefArticle} {shapesController.CurrentShape.ShapeName}";
            HighlightNextNode(_checkpointIndex);
        }
        else
        {
            _checkpoints.Clear();
            instructionsText.text = "";
        }
        _isComplete = false;

    }


    private void ResetShape()
    {
        foreach (var line in _lines)
        {
            line.OnShapeFilled -= HandleShapeFilled;
            DestroyImmediate(line.gameObject);
        }

        _lines.Clear();
        _checkpointIndex = 0;
    }

}
