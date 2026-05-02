using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialDampening : MonoBehaviour
{
    [Range(0f, 1f)]
    public float dampeningCoefficient;

    // 1f is fully reflective
    // 0f is completely absorpative
}
