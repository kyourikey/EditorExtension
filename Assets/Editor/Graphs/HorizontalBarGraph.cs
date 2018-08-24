using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Graphs
{
    public class HorizontalBarGraph : EditorWindow
    {
        private static HorizontalBarGraph _window;

        [MenuItem("Window/Graphs/HorizontalBarGraph")]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<HorizontalBarGraph>();
            }

            _window.titleContent.text = "HorizontalBarGraph";
            _window.Show();
        }

        void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);
            var frameWidth = area.width / 1.1f;
            var frameHeight = area.height / 5f;
            var slope = 10f;
            var gaugeSpace = 2f;
            var space = area.height / 10f;

            var gaugeWidths = new float[]
            {
                (frameWidth - gaugeSpace) * Mathf.Clamp01((Mathf.Sin(Time.realtimeSinceStartup)+1)/2),
                (frameWidth - gaugeSpace) * Mathf.Clamp01((Mathf.Cos(Time.realtimeSinceStartup)+1)/2),
                (frameWidth - gaugeSpace) * Mathf.Clamp01((Mathf.Sin(Time.realtimeSinceStartup*2)+1)/2),
            };
            var gaugeHeight = frameHeight - gaugeSpace;

            //gauge -------------------------------------
            for (var i = 0; i < 3; i++)
            {
                var plusHeight = (frameHeight + space) * i;
                var gaugePos = new Vector3[]
                {
                    new Vector3(slope+gaugeSpace, gaugeSpace+plusHeight, 0f),
                    new Vector3(gaugeSpace, gaugeHeight+plusHeight, 0f),
                    new Vector3(gaugeWidths[i], gaugeHeight+plusHeight, 0f),
                    new Vector3(gaugeWidths[i]+slope, gaugeSpace+plusHeight, 0f),
                };
                Handles.DrawSolidRectangleWithOutline(gaugePos, new Color(1f, 1f, 1f, 1f), new Color(0f, 0f, 0f, 1f));
            }

            //frame -------------------------------------
            for (var i = 0; i < 3; i++)
            {
                var plusHeight = (frameHeight + space) * i;

                var rectPos = new Vector3[]
                {
                    new Vector3(slope, plusHeight, 0f),
                    new Vector3(0f, frameHeight+plusHeight, 0f),
                    new Vector3(frameWidth, frameHeight+plusHeight, 0f),
                    new Vector3(frameWidth + slope, plusHeight, 0f),
                };

                Handles.DrawSolidRectangleWithOutline(rectPos, new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f));
            }
        }
    }
}
