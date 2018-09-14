//------------------------------
// Meaningless editor UI
// © 2018 key-assets
//------------------------------

using UnityEditor;
using UnityEngine;

namespace MeaninglessEditorUI
{
    public class PieChart : EditorWindow
    {
        private static PieChart _window;

        [MenuItem("Tools/MeaninglessEditorUI/PieChart", false, 21)]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<PieChart>();
            }

            _window.titleContent.text = "PieChart";
            _window.Show();
        }

        void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);

            var radius = Screen.width > Screen.height ? Screen.height / 8f : Screen.width / 8f;
            var space = Screen.width > Screen.height ? Screen.height / 20f : Screen.width / 20f;

            var backgroundPos = new Vector3[]
            {
                new Vector3(area.x, area.y, 0f),
                new Vector3(area.x + area.width, area.y, 0f),
                new Vector3(area.x + area.width, area.y + area.height, 0f),
                new Vector3(area.x, area.y + area.height, 0f),
            };

            // background ----------------------------
            Handles.DrawSolidRectangleWithOutline(backgroundPos, Color.black, Color.black);

            // chart ---------------------------------
            Handles.color = Color.white;

            var centerPos = new Vector3(area.center.x, area.center.y);
            Handles.DrawSolidArc(centerPos, Vector3.forward, Vector3.down, (Mathf.Cos(Time.realtimeSinceStartup) - 1) * -180f, radius);
            Handles.DrawWireDisc(centerPos, new Vector3(0f, 0f, 1f), radius);

            var leftPos = new Vector3(area.center.x - (space + radius * 2), area.center.y);
            Handles.DrawSolidArc(leftPos, Vector3.forward, Vector3.down, (Mathf.Sin(Time.realtimeSinceStartup) - 1) * -180f, radius);
            Handles.DrawWireDisc(leftPos, new Vector3(0f, 0f, 1f), radius);

            var rightPos = new Vector3(area.center.x + (space + radius * 2), area.center.y);
            Handles.DrawSolidArc(rightPos, Vector3.forward, Vector3.down, (Mathf.Sin(Time.realtimeSinceStartup / 2) - 1) * -180f, radius);
            Handles.DrawWireDisc(rightPos, new Vector3(0f, 0f, 1f), radius);
        }
    }
}
