using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeglessDriving
{
    public class DisableAfterStart : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
        }
    }
}
