using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class UpgradeHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image sprite;
    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] TextMeshProUGUI equipableLocation;
    [SerializeField] Image background;

    private GameObject upgradeItem;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas rootCanvas;
    private Transform originalParent;
    private int originalSiblingIndex;
    private Vector2 originalAnchoredPos;
    private bool consumed;

    public GameObject UpgradeItem => upgradeItem;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>();
    }

    public void LoadUpgrade(GameObject upgrade)
    {
        upgradeItem = upgrade;

        sprite.sprite = upgrade.GetComponent<SpriteRenderer>().sprite;
        upgradeNameText.text = upgrade.GetComponent<ObjectID>().itemName;
        equipableLocation.text = upgrade.GetComponent<ObjectID>().location;
    }

    public void MarkConsumed()
    {
        consumed = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
        originalAnchoredPos = rectTransform.anchoredPosition;
        background.enabled = false;
        equipableLocation.gameObject.SetActive(false);
        upgradeNameText.gameObject.SetActive(false);

        transform.SetParent(rootCanvas.transform, true);
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!consumed)
        {
            transform.SetParent(originalParent, true);
            transform.SetSiblingIndex(originalSiblingIndex);
            rectTransform.anchoredPosition = originalAnchoredPos;
            background.enabled = true;
            equipableLocation.gameObject.SetActive(true);
            upgradeNameText.gameObject.SetActive(true);
        }
    }
}