using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float minDistance = 0.1f;
    [SerializeField] private float width = 0.5f;

    private Vector3 _previousPosition;
    private bool _isDrawing = false;
    private List<Vector2> _checkpoints;

    public event Action OnShapeFilled;

    private void Start()
    {
        line.startWidth = line.endWidth = width;
        line.positionCount = 1;
        _previousPosition = transform.position;
    }


    public void Draw(List<Vector2> checkpoints)
    {
        _checkpoints = checkpoints;
        _isDrawing = true;
    }

    public void StopDraw()
    {
        _isDrawing = false;
    }


    private void Update()
    {
        if (_isDrawing)
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, _previousPosition) > minDistance)
            {
                if (_previousPosition == transform.position)
                {
                    line.SetPosition(0, currentPosition);
                }
                else
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPosition);

                    if (_checkpoints.Count == 0)
                    {
                        StopDraw();
                        OnShapeFilled?.Invoke();
                        return;
                    }

                    if (Vector3.Distance(currentPosition, _checkpoints[0]) < 1f)
                    {
                        // visited checkpoint
                        _checkpoints.RemoveAt(0);
                    }
                }

                _previousPosition = currentPosition;
            }
        }
    }
}
