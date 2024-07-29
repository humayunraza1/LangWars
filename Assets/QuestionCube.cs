using UnityEngine;

public class QuestionCube : MonoBehaviour
{
    public QuestionManager questionManager; // Reference to the QuestionManager
    public int questionIndex; // Index of the question to be asked

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Question " + questionIndex + " initiated");
            questionManager.TriggerQuestion(questionIndex);
            Destroy(gameObject); // Remove the question cube after triggering the question
        }
    }
}
