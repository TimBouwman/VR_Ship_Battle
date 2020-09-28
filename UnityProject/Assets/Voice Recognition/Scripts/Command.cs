//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using System;
using UnityEngine.Events;
/// <summary>
/// 
/// </summary>
[Serializable]
public class Command
{
    [Tooltip("The name of thie command.")]
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