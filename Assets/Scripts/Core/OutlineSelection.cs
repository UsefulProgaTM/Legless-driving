using System.Collections.Generic;
using UnityEngine;

public class OutlineSelection : MonoBehaviour, ISelectable
{
    [SerializeField]
    private Material selectedMaterial;

    private Material materialInstance;

    private string OUTLINE_THICKNESS = "_Thickness";

    private float targetThickness = 0f;
    private float currentThickness = 0f;
    private float defaultThickness = 0f;
    [SerializeField]
    private float selectedThickness = 0.001f;

    private void Awake()
    {
        currentThickness = 0;
        selectedMaterial.SetFloat(OUTLINE_THICKNESS, currentThickness);
        List<Material> materials = new List<Material>();
        GetComponent<Renderer>().GetMaterials(materials);
        materialInstance = Instantiate<Material>(selectedMaterial);
        materials.Add(materialInstance);
        GetComponent<Renderer>().SetMaterials(materials);
    }

    private void Start()
    {
        SelectionManager.OnSelected += SelectionManager_OnSelected;
    }
    private void OnDestroy()
    {
        SelectionManager.OnSelected -= SelectionManager_OnSelected;
    }
    private void SelectionManager_OnSelected(ISelectable obj)
    {
        ChangeMaterialOnSelect(obj == this);
    }

    public void ChangeMaterialOnSelect(bool hit)
    {
        targetThickness = hit ? selectedThickness : defaultThickness;
        materialInstance.SetFloat(OUTLINE_THICKNESS, targetThickness);
    }
}
