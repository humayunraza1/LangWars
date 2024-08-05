using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] options;
        public int correctAnswerIndex;
    }

    [System.Serializable]
    public class LevelQuestions
    {
        public Question[] questions;
    }

    public LevelQuestions[] levelQuestions; // Array of questions for each level
    public GameObject questionPanel; // UI Panel for displaying the question
    public TMP_Text questionText; // Text element for the question
    public Button[] optionButtons; // Buttons for the options
    public TMP_Text timerText; // Text element for the timer
    public TMP_Text resumeTimerText; // Text element for the resume timer
    public TMP_Text QuestionNumber; // Text element for the resume timer
    public GameObject loseUIPanel; // UI Panel to show when the player loses

    [SerializeField] private CoinCollector coinCollector;
    private float questionTime = 10f;
    private float resumeDelay = 3f;
    private int currentQuestionIndex;
    private int currentLevelIndex;
    private int wrongAnswersCount = 0;
    private const int maxWrongAnswers = 2;

    public LevelManager levelManager;

    void Start()
    {
        ResetWrongAnswers();
        questionPanel.SetActive(false); // Ensure the question panel is initially hidden
        loseUIPanel.SetActive(false); // Ensure the lose panel is initially hidden

        levelManager = FindObjectOfType<LevelManager>();
        currentLevelIndex = levelManager.GetCurrentLevelIndex();
        Debug.Log("Current level is " + currentLevelIndex);

        levelQuestions = new LevelQuestions[]
        {
            new LevelQuestions
            {
                questions = new Question[]
                {
                    new Question
                    {
                        questionText = "こんにちは(Konbanwa) means:",
                        options = new string[] {"Goodbye", "Thank you", "Hello", "Good morning"},
                        correctAnswerIndex = 2
                    },
                    new Question
                    {
                        questionText = "あ stands for:",
                        options = new string[] {"ka", "a", "e", "o"},
                        correctAnswerIndex = 1
                    },
                    new Question
                    {
                        questionText = "Water in Japanese is:",
                        options = new string[] {"みず (Mizu)", "ひ (Hi)", "つき (Tsuki)", "くるま (Kuruma)"},
                        correctAnswerIndex = 0
                    }
                }
            },
            new LevelQuestions
            {
                questions = new Question[]
                {
                    new Question
                    {
                        questionText = "Good evening in Japanese is:",
                        options = new string[] {"さようなら (Sayonara)", "おはよう (Ohayou)", "こんばんは (Konbanwa)", "こんにちは (Konnichiwa)"},
                        correctAnswerIndex = 2
                    },
                    new Question
                    {
                        questionText = "じょう stands for:",
                        options = new string[] {"jyu", "jo", "ji", "ja"},
                        correctAnswerIndex = 1
                    },
                    new Question
                    {
                        questionText = "チ stands for:",
                        options = new string[] {"te", "ta", "to", "chi"},
                        correctAnswerIndex = 3
                    }
                }
            }
        };

        Debug.Log("Level questions initialized. Number of levels: " + levelQuestions.Length);
        for (int i = 0; i < levelQuestions.Length; i++)
        {
            Debug.Log("Level " + i + " has " + levelQuestions[i].questions.Length + " questions.");
        }
    }

    public void TriggerQuestion(int questionIndex)
    {
        currentQuestionIndex = questionIndex;
        currentLevelIndex = levelManager.GetCurrentLevelIndex();
        Debug.Log("Triggering question. Level: " + currentLevelIndex + ", Question: " + currentQuestionIndex);
        Time.timeScale = 0f; // Pause the game
        ShowQuestion();
    }

    void ShowQuestion()
    {
        if (currentLevelIndex < 0 || currentLevelIndex >= levelQuestions.Length)
        {
            Debug.LogError("Invalid currentLevelIndex: " + currentLevelIndex);
            return;
        }

        if (currentQuestionIndex < 0 || currentQuestionIndex >= levelQuestions[currentLevelIndex].questions.Length)
        {
            Debug.LogError("Invalid currentQuestionIndex: " + currentQuestionIndex);
            return;
        }

        QuestionNumber.text = $"Question {currentQuestionIndex + 1}";
        Question question = levelQuestions[currentLevelIndex].questions[currentQuestionIndex];
        questionText.text = question.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // Capture the index for the closure
            TMP_Text optionText = optionButtons[i].GetComponentInChildren<TMP_Text>();
            optionText.text = question.options[i];
            optionText.raycastTarget = true; // Ensure TMP_Text is a raycast target

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
            optionButtons[i].interactable = true; // Ensure buttons are interactable
        }

        questionPanel.SetActive(true);
        StartCoroutine(QuestionTimer());
    }

    IEnumerator QuestionTimer()
    {
        float timeRemaining = questionTime;

        while (timeRemaining > 0)
        {
            timerText.text = timeRemaining.ToString("0");
            yield return new WaitForSecondsRealtime(1f);
            timeRemaining--;
        }

        OnOptionSelected(-1); // No option selected, timer ran out
    }

    void OnOptionSelected(int index)
    {
        StopAllCoroutines();
        questionPanel.SetActive(false);

        // Handle the selected answer (index)
        if (index == levelQuestions[currentLevelIndex].questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Correct answer selected!");
            coinCollector.AddToScore(30);
            // Handle correct answer logic
        }
        else
        {
            Debug.Log("Incorrect answer selected!");
            coinCollector.AddToScore(-20);
            // Handle incorrect answer logic
            wrongAnswersCount++;
            if (wrongAnswersCount >= maxWrongAnswers)
            {
                ShowLoseUI();
                return;
            }
        }

        StartCoroutine(ResumeGameCountdown());
    }

    IEnumerator ResumeGameCountdown()
    {
        float timeRemaining = resumeDelay;
        resumeTimerText.gameObject.SetActive(true);

        while (timeRemaining > 0)
        {
            resumeTimerText.text = timeRemaining.ToString("0");
            yield return new WaitForSecondsRealtime(1f);
            timeRemaining--;
        }

        resumeTimerText.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    void ShowLoseUI()
    {
        Time.timeScale = 0f; // Stop the game
        loseUIPanel.SetActive(true); // Show the lose UI
    }

    public void ResetWrongAnswers()
    {
        wrongAnswersCount = 0;
    }
}
