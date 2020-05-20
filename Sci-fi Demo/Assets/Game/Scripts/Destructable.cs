using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyedCrate;

    public void DestroyCrate()
    {
        Instantiate(_destroyedCrate, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
