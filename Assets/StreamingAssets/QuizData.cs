using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizData
{
    public List<ContentQuizData> contents = new List<ContentQuizData>();
}

[System.Serializable]
public class ContentQuizData
{
    public string showName;
    public List<QuestionData> questions = new List<QuestionData>();
}

[System.Serializable]
public class QuestionData
{
    public string questionText;
    public AnswerData correctAnswer;
    public List<AnswerData> wrongAnswers = new List<AnswerData>();
}

[System.Serializable]
public class AnswerData
{
    public string answerText;
    public string resultDescription;
}
