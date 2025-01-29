using System;
using System.Collections;
using UnityEngine;

public class PanelTransitionController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TitlePanel titlePanel;
    [SerializeField] private GameController gameController;
    [SerializeField] private ReviewPanel reviewPanel;

    private Category _selectedCategory;

    private void Awake()
    {
        titlePanel.OnButtonPressed += HandleCategoryPressed;
        gameController.OnSequenceComplete += HandleGameSequenceComplete;
        reviewPanel.OnReplayPressed += HandleReplayPressed;
    }

    private void HandleCategoryPressed(Category category)
    {
        Debug.Log("selected " + category.ToString());
        _selectedCategory = category;
      
        animator.ResetTrigger("GameIn");
        animator.SetTrigger("GameIn");
    }

    private void HandleGameSequenceComplete()
    {
        animator.ResetTrigger("ReviewIn");
        animator.SetTrigger("ReviewIn");
    }

    private void HandleReplayPressed()
    {
        animator.ResetTrigger("TitleIn");
        animator.SetTrigger("TitleIn");
    }

    public void InitializeGame()
    {
        gameController.Initialize(_selectedCategory);
        EnableDrawing();
    }

    public void AnimateTitle()
    {
        titlePanel.AnimateTitleScreen();
    }

    public void ResetTitle()
    {
        titlePanel.ResetAnimation();
    }

    public void EnableDrawing()
    {
        gameController.EnableDrawing = true;
    }

    public void DisableDrawing()
    {
        gameController.EnableDrawing = false;
    }
}
