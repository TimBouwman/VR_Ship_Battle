//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Wheel : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Transform turnPos = null;
    [SerializeField] private float turnForce = 1f;
    #endregion

    #region Unity Methods
    private void FixedUpdate()
    {
        ApplyRotation();
    }
    #endregion

    #region Custom Methods
    private void ApplyRotation()
    {
        /*
        float turnForceMultiplier = this.transform.rotation.z / 100;
        rb.angularVelocity = new Vector3(0f, turnForce * turnForceMultiplier * Time.fixedDeltaTime, 0f);
        */
        turnPos.localRotation = Quaternion.Euler(0, 0, this.transform.rotation.eulerAngles.z);
    }
    #endregion
}