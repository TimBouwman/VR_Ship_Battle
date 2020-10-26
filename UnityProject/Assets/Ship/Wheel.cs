//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
[ExecuteInEditMode]
public class Wheel : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Transform turnPos = null;
    [SerializeField] private float turnForce = 1f;
    private Quaternion oldRotation = Quaternion.identity;
    #endregion

    #region Unity Methods
    private void Update()
    {
        ApplyRotation();
    }
    #endregion

    #region Custom Methods
    private void ApplyRotation()
    {
        turnPos.localRotation = this.transform.localRotation * oldRotation;
        //oldRotation = this.transform.localEulerAngles;
    }
    #endregion
}