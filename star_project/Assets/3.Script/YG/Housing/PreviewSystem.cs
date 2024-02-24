using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffset = 0.06f;

    [SerializeField] private GameObject cellindicator;
    public GameObject previewObject;

    [SerializeField] private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Awake()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellindicator.gameObject.gameObject.SetActive(false);
        cellIndicatorRenderer = cellindicator.GetComponentInChildren<Renderer>();
    }

    internal void StartShowingPlacementRemovePreview()
    {
        cellindicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false);
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellindicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellindicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        //materals previewMaterialInstance로 갈아끼우기
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        cellindicator.SetActive(false);
        if (previewObject != null)
            Destroy(previewObject);
    }

    public void UpdatePostition(Vector3 postition, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(postition);
            ApplyFeedbackToPreview(validity);
        }

        MoveCursor(postition);
        ApplyFeedbackToCursor(validity);
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;

        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    private void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;

        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
    }

    private void MovePreview(Vector3 postition)
    {
        previewObject.transform.position = new Vector3(postition.x, postition.y + previewYOffset, postition.z);
    }

    private void MoveCursor(Vector3 postition)
    {
        Vector3 cell_size = TCP_Client_Manager.instance.placement_system.grid.cellSize;
        cellindicator.transform.position = postition + new Vector3( (cellindicator.transform.localScale.x - 1) / 2f * cell_size.x, 0, (cellindicator.transform.localScale.z - 1) / 2f * cell_size.z);
    }
}
