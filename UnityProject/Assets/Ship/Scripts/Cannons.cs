//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Cannons : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject[] cannons = null;
    private List<Transform> muzzles = new List<Transform>();
    private bool reloaded = true;
    [SerializeField] float minDelay = 0.05f, maxDelay = 0.2f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        ConfigureCannons();
    }
    #endregion

    #region Custom Methods
    private void ConfigureCannons()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            //Add muzzles to list
            foreach (Transform muzzle in cannons[i].transform.GetComponentsInChildren<Transform>())
            {
                muzzles.Add(muzzle);
            }

            //Add animator to array
        }
    }

    public void Fire()
    {
        if (reloaded)
        {
            StartCoroutine(test());
        }
    }

    private IEnumerator test()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            //spawn and shoot cannon ball
            Debug.Log("Fire!!!");

            //play recoil animation

                
            //play sound

                
            //play particle effect

            //add delay
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
        StopCoroutine(test());
    }
    #endregion
}