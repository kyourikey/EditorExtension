using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Graphs
{
    public class PieChart : EditorWindow
    {
        private static PieChart _window;

        [MenuItem("Window/Graphs/PieChart")]
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
            var radius = 1f;
            var space = 1f;
            if (Screen.width > Screen.height)
            {
                radius = Screen.height / 8f;
                space = Screen.height / 20f;
            }
            else
            {
                radius = Screen.width / 8f;
                space = Screen.width / 20f;
            }

            Handles.color = Color.white;

            var centerPos = new Vector3(area.center.x, area.center.y);
            Handles.DrawWireDisc(centerPos, new Vector3(0f, 0f, 1f), radius);
            Handles.DrawSolidArc(centerPos, Vector3.forward, Vector3.down, (Mathf.Cos(Time.realtimeSinceStartup) - 1) * -180f, radius);

            var leftPos = new Vector3(area.center.x - (space + radius * 2), area.center.y);
            Handles.DrawWireDisc(leftPos, new Vector3(0f, 0f, 1f), radius);
            Handles.DrawSolidArc(leftPos, Vector3.forward, Vector3.down, (Mathf.Sin(Time.realtimeSinceStartup) - 1) * -180f, radius);

            var rightPos = new Vector3(area.center.x + (space + radius * 2), area.center.y);
            Handles.DrawWireDisc(rightPos, new Vector3(0f, 0f, 1f), radius);
            Handles.DrawSolidArc(rightPos, Vector3.forward, Vector3.down, (Mathf.Sin(Time.realtimeSinceStartup / 2) - 1) * -180f,
                radius);

        }
    }
}
