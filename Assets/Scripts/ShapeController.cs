using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [SerializeField] private string shapeName;
    [SerializeField] private Transform[] checkpoints;
    public List<Vector2> Checkpoints { get; } = new List<Vector2>();
    public List<NodeController> NodeControllers = new List<NodeController>();
    public string ShapeName => shapeName;

    private void Awake()
    {
        foreach(Transform point in checkpoints)
        {
            Checkpoints.Add(point.position);
            var node = point.GetComponentInChildren<NodeController>();
            if (node != null)
            {
                NodeControllers.Add(point.GetComponentInChildren<NodeController>());
            }
        }
    }

    public void PlayGrowAnimation()
    {
        StartCoroutine(DelayGrow());
    }

    private IEnumerator DelayGrow()
    {
        yield return new WaitForSeconds(1f);

        foreach(var node in NodeControllers)
        {
            node.Grow();
        }
    }
 
}
