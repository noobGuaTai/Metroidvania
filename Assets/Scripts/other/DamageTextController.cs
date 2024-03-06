using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DamageTextController : MonoBehaviour
{
    public float floatSpeed = 5.0f; // 文本上浮的速度
    public float duration = 1.0f; // 文本在屏幕上保持的时间

    private TextMeshProUGUI textMesh; // 如果你使用TextMeshPro
    // private Text text; // 如果你使用的是Unity的UI系统

    private float alpha = 1.0f;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>(); // 如果你使用TextMeshPro
        // text = GetComponent<Text>(); // 如果你使用的是Unity的UI系统
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 startAnchoredPosition = rectTransform.anchoredPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newY = Mathf.Lerp(startAnchoredPosition.y, startAnchoredPosition.y + floatSpeed, elapsed / duration);
            rectTransform.anchoredPosition = new Vector2(startAnchoredPosition.x, newY);

            // 渐变透明度
            alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / duration);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }

}
