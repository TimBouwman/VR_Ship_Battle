//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Sails : MonoBehaviour
{
    #region Variables
    [SerializeField] private float fullSailForce = 250f, fullSailScale = 1f, halfSailForce = 100f, halfSailScale = 0.5f, raisedSailScale = 0.1f;
    [SerializeField] private float force = 1f, sailScale = 0.1f, lerpSpeed = 1f;
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Transform wheel = null;
    [SerializeField] private Transform[] sails;
    private enum Sail { FullSail, HalfSail, RaisedSail};
    #endregion

    #region Unity Methods
    private void FixedUpdate()
    {
        Movement();
    }
    #endregion

    #region Custom Methods
    private void Movement()
    {
        if (sails[0].localScale.y != sailScale)
        {
            foreach(Transform sail in sails)
            {
                sail.localScale = Vector3.Lerp(sail.localScale,
                                               new Vector3(sail.localScale.x, Mathf.Clamp(sailScale, raisedSailScale, fullSailScale), sail.localScale.z),
                                               lerpSpeed);
            }
        }

        rb.AddForce(this.transform.forward * force * Time.deltaTime);
        rb.AddTorque(Vector3.up * (-force / 4f) * wheel.localRotation.z * Time.deltaTime);
    }
    public void FullSail()
    {
        force = fullSailForce;
        sailScale = fullSailForce;
    }
    public void HalfSail()
    {
        force = halfSailForce;
        sailScale = halfSailScale;
    }
    public void RaiseSail()
    {
        force = 0f;
        sailScale = raisedSailScale;
    }
    #endregion
}