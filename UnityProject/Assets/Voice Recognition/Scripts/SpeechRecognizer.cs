//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

/// <summary>
/// 
/// </summary>
public class SpeechRecognizer : MonoBehaviour
{
    [Serializable]
    public class Command
    {
        [Tooltip("The name of this command.")]
        [SerializeField] private string commandName = null;
        [Tooltip("The Phrase that need to be said to activate the corresponding Action.")]
        [SerializeField] private string[] activationPhrases = null;
        [Tooltip("The Action that is called when one of the ActivationPhrases is recognizet.")]
        [SerializeField] private UnityEvent action = null;

        public string CommandName { get { return this.commandName; } }
        /// <summary>
        /// Returns an array of all the possible Activation Phrases for this command.
        /// </summary>
        public string[] ActivationPhrases { get { return this.activationPhrases; } }
        public UnityEvent Action { get { return this.action; } }
    }

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
            Debug.Log(command.CommandName + " is successfully added to the Dictionary.");
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