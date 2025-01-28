using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    [SerializeField] private Color[] petalColors;
    [SerializeField] private Image[] petals;
    [SerializeField] private Animator animator;

    private float _delay;

    private void Awake()
    {
        _delay = Random.Range(0, 0.5f);
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
