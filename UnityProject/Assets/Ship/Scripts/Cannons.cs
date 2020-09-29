//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;

/// <summary>
/// 
/// </summary>
public class Cannons : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject[] cannons = null;
    private Transform[] muzzles;
    private VisualEffect[] Particles;
    private AudioSource[] audioSources;
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
        //Set Size of arrays.
        muzzles = new Transform[cannons.Length];
        Particles = new VisualEffect[cannons.Length];
        audioSources = new AudioSource[cannons.Length];

        for (int i = 0; i < cannons.Length; i++)
        {
            //Fill arrays.
            muzzles[i] = cannons[i].transform.GetChild(0);
            Particles[i] = muzzles[i].GetChild(0).GetComponent<VisualEffect>();
            audioSources[i] = muzzles[i].GetChild(0).GetComponent<AudioSource>();
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
            //Spawn and shoot cannon ball.
            Debug.Log("Fire!!!");

            //Play recoil animation.

            //Play sound.
            audioSources[i].Play();

            //Play particle effect.
            Particles[i].Play();

            //Add delay.
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
        StopCoroutine(test());
    }
    #endregion
}