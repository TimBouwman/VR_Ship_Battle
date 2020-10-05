//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System.Security.Permissions;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WaveManager : MonoBehaviour
{
    #region Variables
    public static WaveManager instance;

    [SerializeField] private float amplitude = 1f, lenght = 2f, speed = 1f, offset = 0f;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        offset += Time.deltaTime * speed;
    }
    #endregion

    #region Custom Methods
    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / lenght + offset);
    }
    public float Frequenty(float depth, Vector3 direction, float gravity)
    {
        float x = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
        float y = (float)System.Math.Tanh((x * depth) * gravity);
        return Mathf.Sqrt(x * y);
    }
    public float Theta(Vector3 position, float phase, float time, float gravity, float depth, float amplitude, Vector3 direction)
    {
        float x = position.x * direction.x + position.z * direction.z;
        float y = Frequenty(depth, direction, gravity) * time;
        return x - y- phase;
    }
    public Vector3 GerstnerWave(Vector3 position, float phase, float time, float gravity, float depth, float amplitude, Vector3 direction)
    {
        float theta = Theta(position, phase, time, gravity, depth, amplitude, direction);

        //x
        float x1 = Mathf.Sin(theta);
        float x2 = direction.x / Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
        float x3 = (float)System.Math.Tanh(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2)) * depth);
        float x4 = (x3 * amplitude) * x2;
        float x = -1f * (x1 * x4);

        //y
        float y = Mathf.Cos(theta) * amplitude;

        //z
        float z1 = Mathf.Sin(theta);
        float z2 = direction.x / Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
        float z3 = (float)System.Math.Tanh(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2)) * depth);
        float z4 = (z3 * amplitude) * z2;
        float z = -1f * (z1 * z4);

        return new Vector3(x, y, z);
    }
    public Vector3 GerstnerDisplacement(Vector3 position, float phase, Vector4 time, float gravity, float depth, float amplitude1, float amplitude2, float amplitude3, float amplitude4, Vector3 direction1, Vector3 direction2, Vector3 direction3, Vector3 direction4)
    {
        Vector3 x = GerstnerWave(position, phase, time.x, gravity, depth, amplitude1, direction1) + GerstnerWave(position, phase, time.x, gravity, depth, amplitude2, direction2);
        Vector3 y = GerstnerWave(position, phase, time.x, gravity, depth, amplitude3, direction3) + GerstnerWave(position, phase, time.x, gravity, depth, amplitude4, direction4);
        return (x + y) + position;
    }

    #endregion
}