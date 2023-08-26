using UnityEngine;

public class RaycastOnSelectableObjects : MonoBehaviour, IRaycast
{
    private Transform[] selectableObjectTransfrorms;

    private GameObject _selected;

    private float dotThreshold = 0.92f;

    private Vector2 rayDestination;

    private void Awake()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag(Tags.Tag.Outline.ToString());
        selectableObjectTransfrorms = new Transform[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            selectableObjectTransfrorms[i] = temp[i].transform;
        }

        rayDestination = Utility.GetCenteredScreenPosition();
    }

    private void FixedUpdate()
    {
        _selected = null;
        var ray = Camera.main.ScreenPointToRay(rayDestination);

        int closestTransformID = -1;
        float closestTransformValue = -1;

        for (int i = 0; i < selectableObjectTransfrorms.Length; i++)
        {
            float lookPercent = Vector3.Dot(ray.direction.normalized, (selectableObjectTransfrorms[i].position - ray.origin).normalized);

            if (lookPercent > closestTransformValue && lookPercent > dotThreshold)
            {
                closestTransformValue = lookPercent;
                closestTransformID = i;
            }
        }
        if (closestTransformID != -1)
            _selected = selectableObjectTransfrorms[closestTransformID].gameObject;
    }

    public GameObject GetRaycastResult()
    {
        if (_selected != null)
        {
            if(!_selected.gameObject.activeInHierarchy)
            {
                return null;
            }
        }
            
        return _selected;
    }
}
