using UnityEngine;
using UnityEditor;

namespace Agraris.Tools
{
    public sealed class CustomGizmos
    {
        public static void DrawString(string text, Vector3 position, Color? textColor = null, Color? backgroundColor = null)
        {
#if UNITY_EDITOR
            Handles.BeginGUI();
            Color restoreTextColor = GUI.color;
            Color restoreBackgroundColor = GUI.backgroundColor;

            Color bgColorWithAlpha = backgroundColor ?? Color.black;
            bgColorWithAlpha.a = .5f;

            GUI.color = textColor ?? Color.white;
            GUI.backgroundColor = bgColorWithAlpha;

            var view = SceneView.currentDrawingSceneView;
            if (view != null && view.camera != null)
            {
                Vector3 screenPos = view.camera.WorldToScreenPoint(position);
                if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
                {
                    GUI.color = restoreTextColor;
                    Handles.EndGUI();
                    return;
                }
                Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
                var r = new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height - 25, size.x + 4, size.y + 4);
                GUI.Box(r, text, EditorStyles.numberField);
                GUI.Label(r, text);
                GUI.color = restoreTextColor;
                GUI.backgroundColor = restoreBackgroundColor;
            }
            Handles.EndGUI();
#endif
        }
    }
}
