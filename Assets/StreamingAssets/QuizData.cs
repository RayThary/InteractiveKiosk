using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizData
{
    public List<CorrectQuizData> correctQuizDatas = new List<CorrectQuizData>();
    public List<WrongQuizData> wrongQuizDatas = new List<WrongQuizData>();
}

[System.Serializable]
public class CorrectQuizData
{
    public string showName;
    public string question;
    public string answerText;
    public string resultDescription;
}

[System.Serializable]
public class WrongQuizData
{
    public string showName;
    public string answerText;
    public string resultDescription;
}