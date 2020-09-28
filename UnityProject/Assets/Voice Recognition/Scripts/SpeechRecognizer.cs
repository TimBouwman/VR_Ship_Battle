//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// 
/// </summary>
public class SpeechRecognizer : MonoBehaviour
{
    #region Variables
    [SerializeField] private ConfidenceLevel minimumConfidence = ConfidenceLevel.Medium;
    [SerializeField] private Command[] commands = null;
    private readonly Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
    private KeywordRecognizer recognizer = null;
    #endregion

    #region Unity Methods
    private void Start()
    {
        SetupDictionary();
        SetupRecognizer();
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Adds all the commands to the Dictionary.
    /// </summary>
    private void SetupDictionary()
    {
        foreach (Command command in commands)
        {
            foreach (string phrase in command.ActivationPhrases)
            {
                keywordActions.Add(phrase, command.Action.Invoke);
            }
        }
    }
    /// <summary>
    /// Adds the Dictionary to the KeywordRecognizer, adds the OnKeywordRecognition method to the OnPhraseRecognized event
    /// and starts the KeywordRecognizer
    /// </summary>
    private void SetupRecognizer()
    {
        //add keywords to recognizer
        recognizer = new KeywordRecognizer(keywordActions.Keys.ToArray(), minimumConfidence);
        recognizer.OnPhraseRecognized += OnKeywordRecognition;

        //start recognizer
        recognizer.Start();
    }

    private void OnKeywordRecognition(PhraseRecognizedEventArgs phrase)
    {
        Debug.Log("Keyword: " + phrase.text);
        keywordActions[phrase.text].Invoke();
    }
    #endregion
}