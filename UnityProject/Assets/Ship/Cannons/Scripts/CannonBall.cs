//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour, IPooledObject
{
    #region Variables
    private Rigidbody rb = null;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float lifeTime = 10f;
    #endregion
    
    #region Methods
    public void OnObjectSpawn()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
        StopCoroutine(LifeTime());
    }
    #endregion
}