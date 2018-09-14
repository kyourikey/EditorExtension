//------------------------------
// Meaningless editor UI
// © 2018 key-assets
//------------------------------

using UnityEngine;
using UnityEditor;

namespace MeaninglessEditorUI
{
    public class SearchSomething : EditorWindow
    {
        private static SearchSomething _window;

        [MenuItem("Tools/MeaninglessEditorUI/SearchSomething", false, 41)]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<SearchSomething>();
            }

            _window.titleContent.text = "SearchSomething";
            _window.Show();
        }

        void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);

            var searchPos = new Vector3(area.center.x + Mathf.Sin(Time.realtimeSinceStartup / 4f) * area.width / 3f,
                area.center.y + Mathf.Sin(Time.realtimeSinceStartup / 2f) * area.height / 3f, 0f);

            var reticleSize = area.width > area.height ? area.height / 8f : area.width / 8f;

            var backgroundPos = new Vector3[]
            {
                new Vector3(area.x, area.y, 0f),
                new Vector3(area.x + area.width, area.y, 0f),
                new Vector3(area.x + area.width, area.y + area.height, 0f),
                new Vector3(area.x, area.y + area.height, 0f),
            };

            var linePositions = new Vector2[]
            {
                new Vector2(searchPos.x, searchPos.y), new Vector2(searchPos.x - area.width, searchPos.y),
                new Vector2(searchPos.x, searchPos.y), new Vector2(searchPos.x + area.width, searchPos.y),
                new Vector2(searchPos.x, searchPos.y), new Vector2(searchPos.x, searchPos.y - area.height),
                new Vector2(searchPos.x, searchPos.y), new Vector2(searchPos.x, searchPos.y + area.height),
            };

            // x:pos, y:height
            var buildingPositions = new Vector2[]
            {
                new Vector3(area.center.x - (area.width / 1.2f), area.height / 3f),
                new Vector3(area.center.x - (area.width / 1.5f), area.height / 5f),
                new Vector3(area.center.x - (area.width / 2f), area.height / 2f),
                new Vector3(area.center.x - (area.width / 2f), area.height / 2f),
                new Vector3(area.center.x - (area.width / 4f), area.height / 4f),
                new Vector3(area.center.x, area.height / 2f),
                new Vector3(area.center.x + (area.width / 5f), area.height / 5f),
                new Vector3(area.center.x + (area.width / 3f), area.height / 3f),
                new Vector3(area.center.x + (area.width / 2f), area.height / 2f),
                new Vector3(area.center.x + (area.width / 1.2f), area.height / 5f),
                new Vector3(area.center.x + (area.width / 1.5f), area.height / 5f),
            };

            // background ----------------------------
            Handles.DrawSolidRectangleWithOutline(backgroundPos, Color.black, Color.black);

            // reticle ----------------------------
            Handles.color = Color.white;
            Handles.DrawSolidArc(searchPos, Vector3.forward,
                new Vector3(Mathf.Sin(Time.realtimeSinceStartup), Mathf.Cos(Time.realtimeSinceStartup), 0f), 30f,
                reticleSize * 1.3f);
            Handles.DrawSolidArc(searchPos, Vector3.forward,
                new Vector3(Mathf.Cos(Time.realtimeSinceStartup), Mathf.Sin(Time.realtimeSinceStartup), 0f), 30f,
                reticleSize * 1.3f);
            Handles.DrawSolidArc(searchPos, Vector3.forward,
                new Vector3(Mathf.Sin(Time.realtimeSinceStartup * 2), Mathf.Cos(Time.realtimeSinceStartup * 2), 0f), 60f,
                reticleSize * 1.3f);
            Handles.color = Color.black;
            Handles.DrawSolidDisc(searchPos, Vector3.forward, reticleSize * 1.2f);

            Handles.color = Color.white;
            Handles.DrawSolidArc(searchPos, Vector3.forward, new Vector3(1f, 1f, 0f),
                (Mathf.Sin(Time.realtimeSinceStartup) + 1) / 2 * -90f, reticleSize);
            Handles.DrawSolidArc(searchPos, Vector3.forward, new Vector3(-1f, 1f, 0f),
                (Mathf.Cos(Time.realtimeSinceStartup) + 1) / 2 * 90f, reticleSize);
            Handles.color = Color.black;
            Handles.DrawSolidDisc(searchPos, Vector3.forward, reticleSize / 1.1f);

            Handles.color = Color.white;
            for (var i = 0; i < linePositions.Length - 1; i += 2)
            {
                Handles.DrawLine(linePositions[i], linePositions[i + 1]);
            }

            for (var i = 1; i <= 2; i++)
            {
                Handles.DrawWireCube(searchPos, Vector3.one * i * reticleSize / 2 * Random.Range(0.9f, 1.1f));
            }

            Handles.DrawWireDisc(searchPos, Vector3.forward, reticleSize);

            // many buildings
            for (var i = 0; i < buildingPositions.Length; i++)
            {
                Handles.DrawWireCube(
                    new Vector3(buildingPositions[i].x - Mathf.Sin(Time.realtimeSinceStartup / 4f) * area.width / 3f,
                        -buildingPositions[i].y / 2 + area.height),
                    new Vector3(area.width / 10f, buildingPositions[i].y, 0f));
            }
        }
    }
}
