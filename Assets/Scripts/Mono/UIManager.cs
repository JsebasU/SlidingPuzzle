using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public struct UIManagerParameters
{
    [Header("Answers Options")]
    [SerializeField] float margins;
    public float Margins { get { return margins; } }

    [Header("Resolution Screen Options")]
    [SerializeField] Color correctBGColor;
    public Color CorrectBGColor { get { return correctBGColor; } }
    [SerializeField] Color incorrectBGColor;
    public Color IncorrectBGColor { get { return incorrectBGColor; } }
    [SerializeField] Color finalBGColor;
    public Color FinalBGColor { get { return finalBGColor; } }
}
[Serializable]
public struct UIElements
{
    

    [SerializeField] AudioSource questionInfoAudioObject;
    public AudioSource QuestionInfoAudioObject { get { return questionInfoAudioObject; } }

    [SerializeField] UnityEngine.Video.VideoPlayer questionInfoVideoObject;

    public UnityEngine.Video.VideoPlayer QuestionInfoVideoObject { get { return questionInfoVideoObject; } }

    [SerializeField] Image questionInfoImageObject;

    public Image QuestionInfoImageObject { get { return questionInfoImageObject; } }

    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    [Space]

    [SerializeField] GameObject botonContinuar;
    public GameObject BotonContinuar { get { return botonContinuar; } }

    [Space]

    [SerializeField] Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }

    [SerializeField] Image resolutionBG;
    public Image ResolutionBG { get { return resolutionBG; } }

    [SerializeField] TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText { get { return resolutionStateInfoText; } }

    [SerializeField] TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText { get { return resolutionScoreText; } }

    [Space]

    [SerializeField] TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoreText { get { return highScoreText; } }

    [SerializeField] CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }

    [SerializeField] RectTransform finishUIElements;
    public RectTransform FinishUIElements { get { return finishUIElements; } }

    [SerializeField] GameObject confingAudio;

    public GameObject ConfingAudio { get { return confingAudio; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObjectAudio;
    public TextMeshProUGUI QuestionInfoTextObjectAudio { get { return questionInfoTextObjectAudio; } }

    [SerializeField] GameObject confingVideo;

    public GameObject ConfingVideo { get { return confingVideo; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObjectVideo;
    public TextMeshProUGUI QuestionInfoTextObjectVideo { get { return questionInfoTextObjectVideo; } }

    [SerializeField] GameObject confingImage;
    public GameObject ConfingImage { get { return confingImage; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObjectImage;
    public TextMeshProUGUI QuestionInfoTextObjectImage { get { return questionInfoTextObjectImage; } }

}
public class UIManager : MonoBehaviour {

    #region Variables

    public enum ResolutionScreenType { Correct, Incorrect, Finish }

    [Header("References")]
    [SerializeField] GameEvents events = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField] AnswerData answerPrefab = null;

    [SerializeField] UIElements uIElements = new UIElements();

    [Space]
    [SerializeField] UIManagerParameters parameters = new UIManagerParameters();

    private List<AnswerData> currentAnswers = new List<AnswerData>();
    private int resStateParaHash = 0;

    private IEnumerator IE_DisplayTimedResolution = null;

    public GameManager gameManager;

    public Button bvideo;

    public Button baudio;

    public Button bvideoP;

    public Button baudioP;

    public Button bContinuar;

    public GameObject canvasAlterno;

    public GameObject AnswerContent;

    bool Istime = false;

    bool isNow = false;

    public static bool isplaying = false;

    public static bool firstCheckCorrect = false;

    float timer = 0;

    public static bool flagBegin = false;

    float timer2 = 0;

    [SerializeField] RectTransform answersContentArea;
    public RectTransform AnswersContentArea { get { return answersContentArea; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }

    public GameObject vistaGanador;

    public GameObject vistaPerdedor;

    public GameObject vistaJugador;

    public TextMeshProUGUI mensaje;

    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        events.UpdateQuestionUI         += UpdateQuestionUI;
        events.DisplayResolutionScreen  += DisplayResolution;
        events.ScoreUpdated             += UpdateScoreUI;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    void OnDisable()
    {
        events.UpdateQuestionUI         -= UpdateQuestionUI;
        events.DisplayResolutionScreen  -= DisplayResolution;
        events.ScoreUpdated             -= UpdateScoreUI;
    }

    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        /*UpdateScoreUI();
        resStateParaHash = Animator.StringToHash("ScreenState");*/
        flagBegin = false;
    }

    #endregion

    /// <summary>
    /// Function that is used to update new question UI information.
    /// </summary>
    void UpdateQuestionUI(Question question)
    {
        /*gameManager.pause = false;*/
        /*bContinuar.interactable = true;*/
        print("1");
        timer = 0;
        /*uIElements.QuestionInfoAudioObject.clip = null;
        Istime = false;
        uIElements.QuestionInfoVideoObject.clip = null;
        uIElements.ConfingAudio.gameObject.SetActive(false);
        uIElements.ConfingVideo.gameObject.SetActive(false);
        uIElements.ConfingImage.gameObject.SetActive(false);*/
        print("2");
        /*uIElements.QuestionInfoTextObject.gameObject.SetActive(true);*/
        print("3");
        print("Question" + question.Info);
        print(uIElements +"F");
        print(QuestionInfoTextObject + "Text");
        print(AnswersContentArea.transform.childCount + "Text2");

       QuestionInfoTextObject.text = question.Info;
        /*bvideo.gameObject.SetActive(false);
        baudio.gameObject.SetActive(false);
        baudioP.gameObject.SetActive(true);
        bvideoP.gameObject.SetActive(true);
        switch (question.GetAnswerStyle)
        {
           case Question.AnswerStyle.audio:
                uIElements.QuestionInfoAudioObject.Stop();
                uIElements.QuestionInfoTextObject.gameObject.SetActive(false);
                uIElements.ConfingAudio.gameObject.SetActive(true);
                uIElements.QuestionInfoTextObjectAudio.text = question.Info;
                Istime = true;
                uIElements.QuestionInfoAudioObject.clip = question.audio;
                gameManager.pause = true; 
                isNow = false;
                baudio.onClick.Invoke();
                break;

            case Question.AnswerStyle.video:
                uIElements.QuestionInfoVideoObject.Stop();
                uIElements.QuestionInfoTextObject.gameObject.SetActive(false);
                uIElements.ConfingVideo.gameObject.SetActive(true);
                uIElements.QuestionInfoTextObjectVideo.text = question.Info;
                Istime = true;
                isNow = false;
                uIElements.QuestionInfoVideoObject.clip = question.video;
                gameManager.pause = true;               
                bvideo.onClick.Invoke();
                break;

            case Question.AnswerStyle.image:
                uIElements.QuestionInfoTextObject.gameObject.SetActive(false);
                uIElements.ConfingImage.gameObject.SetActive(true);

                uIElements.QuestionInfoTextObjectImage.text = question.Info;
                uIElements.QuestionInfoImageObject.sprite = question.image;
                break;
        }*/
        print("5");
        CreateAnswers(question);
        print("6");
    }


    private void Update()
    {
        /*timer2 = timer2 + Time.deltaTime;
        if(timer2 > 3 && !flagBegin)
        {
            canvasAlterno.gameObject.SetActive(true);
            flagBegin = true;
        }*/
        if (!firstCheckCorrect)
        {
            Istime = false;
        }
        /*if (Istime) {
            if (isNow)
            {
                timer = timer + Time.deltaTime;
                if (uIElements.QuestionInfoVideoObject.clip != null)
                {
                    if (uIElements.QuestionInfoVideoObject.clip.length <= timer)
                    {
                        gameManager.pause = false;
                        timer = 0;
                        isNow = false;
                    }
                }
                if (uIElements.QuestionInfoAudioObject.clip != null)
                {
                    if (timer >= uIElements.QuestionInfoAudioObject.clip.length)
                    {
                        gameManager.pause = false;
                        timer = 0;
                        isNow = false;
                    }
                }
            }
        }*/
        /*Debug.Log(timer+"T");
        Debug.Log(uIElements.QuestionInfoAudioObject.clip.length+"AAAAA");
        Debug.Log(uIElements.QuestionInfoVideoObject.clip.length+"VVVVV");*/
    }

    public void play() {
        isNow = true;
    }

    public void pause()
    {
        isNow = false;
    }
    /// <summary>
    /// Function that is used to display resolution screen.
    /// </summary>
    void DisplayResolution(ResolutionScreenType type, int score)
    {
        UpdateResUI(type, score);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 2);
        uIElements.MainCanvasGroup.blocksRaycasts = false;

        if (type != ResolutionScreenType.Finish)
        {
            if (IE_DisplayTimedResolution != null)
            {
                StopCoroutine(IE_DisplayTimedResolution);
            }
        }
    }

    public void Continuar()
    {
        for (int i = 0; i < AnswerContent.transform.childCount; i++)
        {
            AnswerContent.transform.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
        IE_DisplayTimedResolution = DisplayTimedResolution();
        StartCoroutine(IE_DisplayTimedResolution);
        bContinuar.interactable = false;
        canvasAlterno.SetActive(false);      
        gameManager.Continuar();
    }

    IEnumerator DisplayTimedResolution()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 1);
        uIElements.MainCanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Function that is used to display resolution UI information.
    /// </summary>
    void UpdateResUI(ResolutionScreenType type, int score)
    {
        uIElements.QuestionInfoVideoObject.Stop();
        uIElements.QuestionInfoAudioObject.Stop();
        canvasAlterno.SetActive(true);

        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        switch (type)
        {
            case ResolutionScreenType.Correct:
                //uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "CORRECTO!";
                
                if (gameManager.publichelpfinishDoble)
                {
                    gameManager.publichelpfinish = true;
                    gameManager.publichelpfinishDoble = false;
                }
                /*uIElements.ResolutionScoreText.text = "+" + score;*/
                break;
            case ResolutionScreenType.Incorrect:
                //uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "INCORRECTO!";
                
                if (gameManager.publichelpfinishDoble)
                {
                    gameManager.publichelpfinish = true;
                }
                /*uIElements.ResolutionScoreText.text = "-" + score;*/
                break;
            case ResolutionScreenType.Finish:
                //uIElements.ResolutionBG.color = parameters.FinalBGColor;
                /*uIElements.ResolutionStateInfoText.text = "PUNTUACIÓN";*/
                if (gameManager.publichelpfinishDoble)
                {
                    gameManager.publichelpfinish = true;
                }
                /*uIElements.BotonContinuar.SetActive(false);*/
                vistaJugador.SetActive(false);
                StartCoroutine(CalculateScore());
                /*uIElements.FinishUIElements.gameObject.SetActive(true);
                uIElements.HighScoreText.gameObject.SetActive(true);*/
               /*uIElements.HighScoreText.text = ((highscore > events.StartupHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore: " + highscore;*/
                break;
        }
    }

    /// <summary>
    /// Function that is used to calculate and display the score.
    /// </summary>
    IEnumerator CalculateScore()
    {
        var scoreValue = 0;
        if(events.CurrentFinalScore < 20)
        {
            vistaPerdedor.SetActive(true);
            mensaje.text = events.CurrentFinalScore.ToString() + "/20";
        }
        if(events.CurrentFinalScore == 20)
        {
            vistaGanador.SetActive(true);
        }
        /*while (scoreValue < events.CurrentFinalScore)
        {
            scoreValue++;
            uIElements.ResolutionScoreText.text = scoreValue.ToString();

        }*/
        yield return null;
    }

    /// <summary>
    /// Function that is used to create new question answers.
    /// </summary>
    void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab,AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            AnswersContentArea.sizeDelta = new Vector2(AnswersContentArea.sizeDelta.x, offset * -1);

            currentAnswers.Add(newAnswer);
        }
    }
    /// <summary>
    /// Function that is used to erase current created answers.
    /// </summary>
    void EraseAnswers()
    {
        foreach (var answer in currentAnswers)
        {
            Destroy(answer.gameObject);
        }
        currentAnswers.Clear();
    }

    /// <summary>
    /// Function that is used to update score text UI.
    /// </summary>
    void UpdateScoreUI()
    {
        uIElements.ScoreText.text = events.CurrentFinalScore.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void reset()
    {
        SceneManager.LoadScene(0);
    }
}