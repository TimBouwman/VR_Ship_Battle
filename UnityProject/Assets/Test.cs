//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform moveObject = null;
    private void LateUpdate()
    {
        moveObject.position = this.transform.position;
        moveObject.rotation = this.transform.rotation;
    }
}