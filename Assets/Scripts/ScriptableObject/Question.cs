using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable()]
public struct Answer
{
    [SerializeField] private string _info;
    public string Info { get { return _info; } }

    [SerializeField] private bool _isCorrect;
    public bool IsCorrect { get { return _isCorrect; } }
}
[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/new Question")]
public class Question : ScriptableObject {

    public enum                 AnswerType                  { Multi, Single }

    public enum                 AnswerStyle                  { text, video, audio, image }

    [SerializeField] private    String      _info           = String.Empty;
    public                      String      Info            { get { return _info; } }

    [SerializeField] private AudioClip _audio = null;
    public AudioClip audio { get { return _audio; } }

    [SerializeField] private UnityEngine.Video.VideoClip _video = null;
    public UnityEngine.Video.VideoClip video { get { return _video; } }

    [SerializeField] private Sprite _image = null;
    public Sprite image { get { return _image; } }


    [SerializeField]            Answer[]    _answers        = null;
    public                      Answer[]    Answers         { get { return _answers; } }

    //Parameters

    [SerializeField] private    bool        _useTimer       = false;
    public                      bool        UseTimer        { get { return _useTimer; } }

    [SerializeField] private    int         _timer          = 0;
    public                      int         Timer           { get { return _timer; } }

    [SerializeField] private    AnswerType  _answerType     = AnswerType.Multi;
    public                      AnswerType  GetAnswerType   { get { return _answerType; } }

    [SerializeField] private AnswerStyle _answerStyle = AnswerStyle.text;
    public AnswerStyle GetAnswerStyle { get { return _answerStyle; } }

    [SerializeField] private    int         _addScore       = 10;
    public                      int         AddScore        { get { return _addScore; } }

    [SerializeField] private UnityEngine.Object source = null;
    public UnityEngine.Object obj { get { return source; } }

    private void Awake()
    {
        _timer = PlayerPrefs.GetInt("tiempoPreguntas");
    }

    /// <summary>
    /// Function that is called to collect and return correct answers indexes.
    /// </summary>
    public List<int> GetCorrectAnswers ()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                CorrectAnswers.Add(i);
            }
        }
        return CorrectAnswers;
    }

    public int rCorrecta;

    public int RespuestaCorrecta()
    {
        for (int i = 0; i < Answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                rCorrecta = i;
            }
        }

        return rCorrecta;
    }

}