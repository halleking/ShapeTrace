using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    [SerializeField] private string shapeName;
    [SerializeField] private bool vowelSound;
    [SerializeField] private Transform[] checkpoints;

    private TMP_Text _text;
    public List<Vector2> Checkpoints { get; } = new List<Vector2>();
    public List<NodeController> NodeControllers = new List<NodeController>();
    public string ShapeName => shapeName;
    public bool VowelSound => vowelSound;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
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
        StartCoroutine(FadeOutlineToZeroAlpha(0.5f, _text));
        StartCoroutine(DelayGrow());
    }

    private IEnumerator FadeOutlineToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
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
