/* DropDownAutoScrollOnChange
 * --------------------------
 * Updates scrolling when selection changes inside a drop down panel when selection changes.
 * This scripts patches the inability of ScrollRects to handle correct scrolling upon selection change.
 * 
 * Adepted From:
 * https://answers.unity.com/questions/1169028/unity-dropdown-doesnt-scroll-when-navigating-with.html
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameOptionsUtility.Utility
{
    [RequireComponent(typeof(ScrollRect))]
    internal class DropDownAutoScrollOnChange : MonoBehaviour
    {
        RectTransform scrollRectTransform;
        RectTransform contentPanel;
        RectTransform selectedRectTransform;
        GameObject lastSelected;

        void Start()
        {
            scrollRectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            // Lazy Get
            if (contentPanel == null)
                contentPanel = GetComponent<ScrollRect>().content;

            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (selected == null)
                return;

            if (selected.transform.parent != contentPanel.transform)
                return;

            if (selected == lastSelected)
                return;

            selectedRectTransform = selected.GetComponent<RectTransform>();

            float yPos = -(selectedRectTransform.localPosition.y) - (selectedRectTransform.rect.height / 2);
            float yContent = contentPanel.anchoredPosition.y;
            float maxHeight = scrollRectTransform.rect.height - selectedRectTransform.rect.height;
            float delta = yPos - yContent;

            if (delta < 0)
                yContent = yPos;
            else if (delta > maxHeight)
                yContent = yPos - maxHeight;
            else
                return;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, yContent);

            lastSelected = selected;
        }
    }
}