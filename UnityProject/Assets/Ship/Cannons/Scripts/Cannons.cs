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
    private VisualEffect[] particles;
    private AudioSource[] audioSources;
    private Animator[] animators;
    private bool reloaded = true;
    [SerializeField] float minDelay = 0.05f, maxDelay = 0.2f;
    private ObjectPooler objectPooler = null;
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
        objectPooler = ObjectPooler.Instance;

        //Set Size of arrays.
        muzzles = new Transform[cannons.Length];
        particles = new VisualEffect[cannons.Length];
        audioSources = new AudioSource[cannons.Length];
        animators = new Animator[cannons.Length];

        for (int i = 0; i < cannons.Length; i++)
        {
            //Fill arrays.
            muzzles[i] = cannons[i].transform.GetChild(0);
            particles[i] = muzzles[i].GetChild(0).GetComponent<VisualEffect>();
            audioSources[i] = muzzles[i].GetChild(0).GetComponent<AudioSource>();
            animators[i] = cannons[i].transform.GetChild(1).GetComponent<Animator>();

            //Remove particle parent
            //particles[i].transform.parent = null;
        }
        Debug.Log(cannons.Length + " cannons successfully configured.");
    }

    public void Fire()
    {
        if (reloaded)
        {
            StartCoroutine(FireCoroutine());
        }
    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            //Spawn and shoot cannon ball.
            objectPooler.SpawnFromPool("CannonBalls", muzzles[i].position, muzzles[i].rotation);

            //Play particle effect.
            //particles[i].transform.position = muzzles[i].position;
            //particles[i].transform.rotation = muzzles[i].rotation;
            particles[i].Play();

            //Play sound.
            audioSources[i].Play();

            //Play random recoil animation.
            int rnd = Random.Range(1, 3);
            animators[i].SetInteger("Index", rnd);
            animators[i].SetTrigger("Fire");

            //Add delay.
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
        StopCoroutine(FireCoroutine());
    }
    #endregion
}