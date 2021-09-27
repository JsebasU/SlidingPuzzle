using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Question)), CanEditMultipleObjects]
public class Question_Editor : Editor {

    #region Variables

    #region Serialized Properties
    SerializedProperty  questionInfoProp        = null;
    SerializedProperty  answersProp             = null;
    SerializedProperty  useTimerProp            = null;
    SerializedProperty  timerProp               = null;
    SerializedProperty  answerTypeProp          = null;
    SerializedProperty  AnswerStyleProp          = null;
    SerializedProperty  addScoreProp            = null;
    SerializedProperty audio = null;
    SerializedProperty video = null;
    SerializedProperty image = null;
     Object source;

    SerializedProperty  arraySizeProp
    {
        get
        {
            return answersProp.FindPropertyRelative("Array.size");
        }
    }
    #endregion

    private bool        showParameters          = false;

    #endregion

    #region Default Unity methods

    void OnEnable ()
    {
        #region Fetch Properties
        questionInfoProp    = serializedObject.FindProperty("_info");
        answersProp         = serializedObject.FindProperty("_answers");
        useTimerProp        = serializedObject.FindProperty("_useTimer");
        timerProp           = serializedObject.FindProperty("_timer");
        answerTypeProp      = serializedObject.FindProperty("_answerType");
        AnswerStyleProp     = serializedObject.FindProperty("_answerStyle");
        addScoreProp        = serializedObject.FindProperty("_addScore");
        audio               = serializedObject.FindProperty("_audio");
        video               = serializedObject.FindProperty("_video");
        image               = serializedObject.FindProperty("_image");
        #endregion

        #region Get Values
        showParameters = EditorPrefs.GetBool("Question_showParameters_State");
        #endregion
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.Update();
        GUILayout.Label("Question", EditorStyles.miniLabel);
        GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea)
        {
            fontSize = 15,
            fixedHeight = 30,
            alignment = TextAnchor.MiddleLeft
        };
        questionInfoProp.stringValue = EditorGUILayout.TextArea(questionInfoProp.stringValue, textAreaStyle);
        GUILayout.Space(7.5f);

        GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout)
        {
            fontSize = 10
        };
        EditorGUI.BeginChangeCheck();
        showParameters = EditorGUILayout.Foldout(showParameters, "Parameters", foldoutStyle);
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetBool("Question_showParameters_State", showParameters);
        }
        if (showParameters)
        {
            EditorGUILayout.PropertyField(useTimerProp, new GUIContent("Use Timer", "Should this question have a timer?"));
            if (useTimerProp.boolValue)
            {
                timerProp.intValue = EditorGUILayout.IntSlider(new GUIContent("Time"), timerProp.intValue, 1, 120);
            }
            GUILayout.Space(2);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(answerTypeProp, new GUIContent("Answer Type", "Specify this question answer type."));
            if (EditorGUI.EndChangeCheck())
            {
                if (answerTypeProp.enumValueIndex == (int)Question.AnswerType.Single)
                {
                    if (GetCorrectAnswersCount() > 1)
                    {
                        UncheckCorrectAnswers();
                    }
                }
            }
            addScoreProp.intValue = EditorGUILayout.IntSlider(new GUIContent("Add Score"), addScoreProp.intValue, 0, 100);

            EditorGUILayout.PropertyField(audio, new GUIContent("Answer audio", "Specify this question answer type."));

            EditorGUILayout.PropertyField(video, new GUIContent("Answer video", "Specify this question answer type."));

            EditorGUILayout.PropertyField(image, new GUIContent("Answer image", "Specify this question answer type."));

            EditorGUILayout.PropertyField(AnswerStyleProp, new GUIContent("Answer Type", "Specify this question answer type."));


            Question.AnswerStyle style = (Question.AnswerStyle)AnswerStyleProp.enumValueIndex;

            switch (style)
            {
                case Question.AnswerStyle.text:
 
                    break;

                case Question.AnswerStyle.audio:
                    source = EditorGUILayout.ObjectField(source, typeof(AudioClip), true);
                    break;

                case Question.AnswerStyle.video:
                    source = EditorGUILayout.ObjectField(source, typeof(VideoClipImporter), true);
                    break;

            }

        }
        GUILayout.Space(7.5f);
        GUILayout.Label("Answers", EditorStyles.miniLabel);
        DrawAnswers();

        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    void DrawAnswers ()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(arraySizeProp);
        GUILayout.Space(5);

        EditorGUI.indentLevel++;
        for (int i = 0; i < arraySizeProp.intValue; i++)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(answersProp.GetArrayElementAtIndex(i));
            if (EditorGUI.EndChangeCheck())
            {
                if (answerTypeProp.enumValueIndex == (int)Question.AnswerType.Single)
                {
                    SerializedProperty isCorrectProp = answersProp.GetArrayElementAtIndex(i).FindPropertyRelative("_isCorrect");

                    if (isCorrectProp.boolValue)
                    {
                        UncheckCorrectAnswers();
                        answersProp.GetArrayElementAtIndex(i).FindPropertyRelative("_isCorrect").boolValue = true;

                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
            GUILayout.Space(5);
        }

        EditorGUILayout.EndVertical();
        EditorGUI.indentLevel--;
    }

    void UncheckCorrectAnswers ()
    {
        for (int i = 0; i < arraySizeProp.intValue; i++)
        {
            answersProp.GetArrayElementAtIndex(i).FindPropertyRelative("_isCorrect").boolValue = false;
        }
    }

    int GetCorrectAnswersCount ()
    {
        int count = 0;
        for (int i = 0; i < arraySizeProp.intValue; i++)
        {
            if (answersProp.GetArrayElementAtIndex(i).FindPropertyRelative("_isCorrect").boolValue)
            {
                count++;
            }
        }
        return count;
    }
}