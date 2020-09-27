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
    [SerializeField] private string phrase = "";
    [SerializeField] private UnityEvent action = null;

    public string Phrase { get { return this.phrase; } }
    public UnityEvent Action { get { return this.action; } }
}