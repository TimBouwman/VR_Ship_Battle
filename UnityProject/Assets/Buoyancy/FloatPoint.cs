//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class FloatPoint : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float depthBeforeSubmerged = 1f;
    [SerializeField] private float displacementAmount = 3f;
    [SerializeField] private float waveHeight = 0f;
    [SerializeField] private int FloatPointAmount = 1;
    [Header("Drag")]
    [SerializeField] private float waterDrag = 0.99f;
    [SerializeField] private float waterAngularDrag = 0.5f;
    #endregion

    #region Unity Methods

    private void FixedUpdate()
    {
        Buoyancy();
    }
    #endregion

    #region Custom Methods
    private void Buoyancy()
    {
        rb.AddForceAtPosition(Physics.gravity / FloatPointAmount, this.transform.position, ForceMode.Acceleration);

        if (this.transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - this.transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), this.transform.position, ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
    #endregion
}