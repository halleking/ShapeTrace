using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPanel : MonoBehaviour
{
    [SerializeField] private Button replayButton;

    public event Action OnReplayPressed;

    private void Awake()
    {
        replayButton.onClick.AddListener(() => OnReplayPressed?.Invoke());

    }
}
