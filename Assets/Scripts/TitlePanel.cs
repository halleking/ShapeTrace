using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Category
{
    Numbers,
    Alphabet,
    Shapes
}

public class TitlePanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button numbersButton;
    [SerializeField] private Button alphabetButton;
    [SerializeField] private Button shapesButton;

    public event Action<Category> OnButtonPressed;

    private void Awake()
    {
        numbersButton.onClick.AddListener(() => OnButtonPressed?.Invoke(Category.Numbers));
        alphabetButton.onClick.AddListener(() => OnButtonPressed?.Invoke(Category.Alphabet));
        shapesButton.onClick.AddListener(() => OnButtonPressed?.Invoke(Category.Shapes));
    }

    public void AnimateTitleScreen()
    {
        animator.SetTrigger("Show");
    }

    public void ResetAnimation()
    {
        animator.SetTrigger("Reset");
    }
}
