using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Assertions.Must;

namespace Assets.Editor.Graphs
{
    public class RadarChart : EditorWindow
    {
        private static RadarChart _window;

        [MenuItem("Window/Graphs/RadarChart")]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<RadarChart>();
            }

            _window.titleContent.text = "RadarChart";
            _window.Show();
        }

        void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);

            var graphSize = area.width > area.height ? area.height / 3 : area.width / 3; ;
            var graphShape = 6;

            var scorePositions = new Vector3[]
            {
                GetPolygonPosition(0, graphShape, (Mathf.Clamp((Mathf.Sin(Time.realtimeSinceStartup)+1f) / 2f, 0.2f, 0.8f)) * graphSize)+area.center,
                GetPolygonPosition(1, graphShape, (Mathf.Clamp((Mathf.Cos(Time.realtimeSinceStartup)+1f) / 2f, 0.2f, 0.8f)) * graphSize)+area.center,
                GetPolygonPosition(2, graphShape, (Mathf.Clamp((Mathf.Sin(Time.realtimeSinceStartup*2f)+1f) / 2f, 0.2f, 0.8f)) * graphSize)+area.center,
                GetPolygonPosition(3, graphShape, (Mathf.Clamp((Mathf.Cos(Time.realtimeSinceStartup*2f)+1f) / 2f, 0.2f, 0.8f)) * graphSize)+area.center,
                GetPolygonPosition(4, graphShape, (Mathf.Clamp((Mathf.Sin(Time.realtimeSinceStartup*3f)+1f) / 2f, 0.2f, 0.8f)) * graphSize)+area.center,
                GetPolygonPosition(5, graphShape, (Mathf.Clamp((Mathf.Sin(Time.realtimeSinceStartup*4f)+1f) / 2f, 0.2f, 0.8f)) * graphSize)+area.center,
            };

            var basePolygonPositions = new Vector3[]
            {
                GetPolygonPosition(0, graphShape, graphSize)+area.center,
                GetPolygonPosition(1, graphShape, graphSize)+area.center,
                GetPolygonPosition(2, graphShape, graphSize)+area.center,
                GetPolygonPosition(3, graphShape, graphSize)+area.center,
                GetPolygonPosition(4, graphShape, graphSize)+area.center,
                GetPolygonPosition(5, graphShape, graphSize)+area.center,
            };

            var scoreLinePositions = new Vector3[]
            {
                scorePositions[0], scorePositions[1],
                scorePositions[1], scorePositions[2],
                scorePositions[2], scorePositions[3],
                scorePositions[3], scorePositions[4],
                scorePositions[4], scorePositions[5],
                scorePositions[5], scorePositions[0],
            };

            var basePolygonLinePositions = new Vector3[]
            {
                basePolygonPositions[0], basePolygonPositions[1],
                basePolygonPositions[1], basePolygonPositions[2],
                basePolygonPositions[2], basePolygonPositions[3],
                basePolygonPositions[3], basePolygonPositions[4],
                basePolygonPositions[4], basePolygonPositions[5],
                basePolygonPositions[5], basePolygonPositions[0],
            };

            var baseLinePositions = new Vector3[]
            {
                area.center, basePolygonPositions[0],
                area.center, basePolygonPositions[1],
                area.center, basePolygonPositions[2],
                area.center, basePolygonPositions[3],
                area.center, basePolygonPositions[4],
                area.center, basePolygonPositions[5],
            };

            // polygon background color
            for (var i = 0; i < scoreLinePositions.Length - 1; i++)
            {
                var rectPos = new Vector3[]
                {
                    area.center,
                    area.center,
                    scoreLinePositions[i],
                    scoreLinePositions[i+1],
                };

                var backColor = new Color(0.6f, 0.6f, 0.6f, 0.5f);
                Handles.DrawSolidRectangleWithOutline(rectPos, backColor, backColor);
            }

            // polygon
            Handles.color = Color.white;
            Handles.DrawLines(basePolygonLinePositions);
            Handles.DrawLines(baseLinePositions);
            Handles.DrawLines(scoreLinePositions);

            // dot
            var dotRadius = area.width > area.height ? area.height / 50 : area.width / 50;

            Handles.color = Color.white;
            for (var i = 0; i < scorePositions.Length; i++)
            {
                Handles.DrawSolidDisc(scorePositions[i], new Vector3(0f, 0f, 1f), dotRadius);
            }
        }

        // Base is here
        // http://rick08.hatenablog.com/entry/2013/07/24/082842
        private Vector2 GetPolygonPosition(int angleCount, int shapeTypeNum, float radius)
        {
            var x = radius * Mathf.Cos(2 * angleCount * Mathf.PI / shapeTypeNum - Mathf.PI / 2);
            var y = radius * Mathf.Sin(2 * angleCount * Mathf.PI / shapeTypeNum - Mathf.PI / 2);
            return new Vector3(x, y, 0f);
        }
    }
}
