using UnityEngine;

public class SafeAreaHandler : MonoBehaviour
{
    public Canvas Canvas;

    private RectTransform _panelSafeArea;
    private Rect _currentSafeArea = new();
    private ScreenOrientation _screenOrientation = ScreenOrientation.AutoRotation;

    // Start is called before the first frame update
    void Start()
    {
        _panelSafeArea = GetComponent<RectTransform>();
        _currentSafeArea = Screen.safeArea;
        _screenOrientation = Screen.orientation;

        ApplySafeArea();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSafeArea != Screen.safeArea || _screenOrientation != Screen.orientation) {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea() {
        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Canvas.pixelRect.width;
        anchorMin.y /= Canvas.pixelRect.height;

        anchorMax.x /= Canvas.pixelRect.width;
        anchorMax.y /= Canvas.pixelRect.height;

        _panelSafeArea.anchorMin = anchorMin;
        _panelSafeArea.anchorMax = anchorMax;

        _currentSafeArea = Screen.safeArea;
        _screenOrientation = Screen.orientation;
    }
}
