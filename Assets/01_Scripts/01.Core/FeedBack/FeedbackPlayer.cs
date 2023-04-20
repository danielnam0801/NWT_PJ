using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField] List<Feedback> feedbacks;

    private void Awake()
    {
        GetComponents<Feedback>(feedbacks);
    }

    public virtual void PlayFeedback()
    { 
        foreach(Feedback feedback in feedbacks)
        {
            feedback.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach(Feedback feedback in feedbacks)
        {
            feedback.DestroyFeedback();
        }
    }
}
