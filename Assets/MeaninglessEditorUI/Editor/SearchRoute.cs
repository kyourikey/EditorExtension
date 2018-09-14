//------------------------------
// Meaningless editor UI
// © 2018 key-assets
//------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor.Graphs
{
    public class SearchRoute : EditorWindow
    {
        private static SearchRoute _window;

        [MenuItem("Tools/MeaninglessEditorUI/SearchRoute", false, 42)]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<SearchRoute>();
            }

            _window.titleContent.text = "SearchRoute";
            _window.Show();
        }

        void Update()
        {
            if (Time.realtimeSinceStartup % 2 > 1f)
                Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);

            var backgroundPos = new Vector3[]
            {
                new Vector3(area.x, area.y, 0f),
                new Vector3(area.x + area.width, area.y, 0f),
                new Vector3(area.x + area.width, area.y + area.height, 0f),
                new Vector3(area.x, area.y + area.height, 0f),
            };

            var linePositions = new List<Vector2>();
            var space = 10;
            var spaceWidth = area.width / space;
            var spaceHeight = area.height / space;

            for (var i = 1; i < 10; i++)
            {
                for (var j = 1; j < 10; j++)
                {
                    linePositions.Add(new Vector2(spaceWidth * j, spaceHeight * i));
                }
            }

            var dotSize = area.width > area.height ? area.height / 100f : area.width / 100f;
            var squareSize = area.width > area.height ? area.height / 25f : area.width / 25f;

            // background -----------------------
            Handles.DrawSolidRectangleWithOutline(backgroundPos, Color.black, Color.black);

            // lines ----------------------------
            for (var i = 0; i < linePositions.Count; i++)
            {
                Handles.color = Color.white;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        if (linePositions.Count > i + 9)
                        {
                            Handles.DrawLine(linePositions[i], linePositions[i + 9]);
                        }
                        break;
                    case 1:
                        if (0 < i - 10)
                        {
                            Handles.DrawLine(linePositions[i], linePositions[i - 9]);
                            Handles.DrawWireCube(linePositions[i], Vector2.one * squareSize);
                        }
                        break;
                    case 2:
                        if (linePositions.Count > i + 1 &&
                            (i + 1) % 9 != 0)
                        {
                            Handles.DrawLine(linePositions[i], linePositions[i + 1]);
                        }
                        break;
                    case 3:
                        if (0 < i - 1 &&
                            (i - 1) % 9 != 8)
                        {
                            Handles.DrawLine(linePositions[i], linePositions[i - 1]);
                        }
                        break;
                }
                Handles.DrawSolidDisc(linePositions[i], Vector3.forward, dotSize);
            }
        }
    }
}
