using UnityEngine;

public class SetChildrenLayer : MonoBehaviour
{
    public int targetLayer = 6;
    void Start()
    {
        SetLayerRecursively(gameObject, targetLayer);
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
