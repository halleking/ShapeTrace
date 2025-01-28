using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    [SerializeField] private Color[] petalColors;
    [SerializeField] private Image[] petals;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform seed;
    [SerializeField] private GameObject highlight;

    private float _delay;
    private Vector2 _position;

    public Vector2 Position => _position;

    private void Awake()
    {
      
        _position = transform.position;
        _delay = Random.Range(0, 0.5f);

        // randomize seed rotation
        var euler = transform.eulerAngles;
        euler.z = Random.Range(0f, 360f);
        seed.eulerAngles = euler;
    }

    public void Highlight(bool enable)
    {
        highlight.gameObject.SetActive(enable);
    }

    public void PlaceSeed()
    {
        seed.gameObject.SetActive(true);
    }

    public void Grow()
    {
        StartCoroutine(OffsetGrow());
    }

    private IEnumerator OffsetGrow()
    {
        yield return new WaitForSeconds(_delay);
        var petalColor = petalColors[Random.Range(0, petalColors.Length)];
        foreach (var petal in petals)
        {
            petal.color = petalColor;
        }

        animator.enabled = true;

    }
}
