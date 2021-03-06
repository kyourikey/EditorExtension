﻿//------------------------------
// Meaningless editor UI
// © 2018 key-assets
//------------------------------

using UnityEngine;
using UnityEditor;

namespace MeaninglessEditorUI
{
    public class CircularRotation : EditorWindow
    {
        private static CircularRotation _window;

        [MenuItem("Tools/MeaninglessEditorUI/CircularRotation", false, 22)]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<CircularRotation>();
            }

            _window.titleContent.text = "CircularRotation";
            _window.Show();
        }

        void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);

            var backgroundSpace = area.width > area.height ? area.height / 5 : area.width / 5;
            var backgroundRadius = area.width > area.height ? area.height / 2 - backgroundSpace : area.width / 2 - backgroundSpace;

            var outsideMaskSpace = area.width > area.height ? area.height / 3 : area.width / 3;
            var outsideMaskRadius = area.width > area.height ? area.height / 2 - outsideMaskSpace : area.width / 2 - outsideMaskSpace;

            var insideMaskSpace = area.width > area.height ? area.height / 2.2f : area.width / 2.2f;
            var insideMaskRadius = area.width > area.height ? area.height / 2 - insideMaskSpace : area.width / 2 - insideMaskSpace;

            var outsideDottedLineSpace = area.width > area.height ? area.height / 4 : area.width / 4;
            var outsideDottedLineRadius = area.width > area.height ? area.center.y - outsideDottedLineSpace : area.center.x - outsideDottedLineSpace;

            var insideDottedLineSpace = area.width > area.height ? area.height / 2.5f : area.width / 2.5f;
            var insideDottedLineRadius = area.width > area.height ? area.center.y - insideDottedLineSpace : area.center.x - insideDottedLineSpace;

            var backgroundPos = new Vector3[]
            {
                new Vector3(area.x, area.y, 0f),
                new Vector3(area.x + area.width, area.y, 0f),
                new Vector3(area.x + area.width, area.y + area.height, 0f),
                new Vector3(area.x, area.y + area.height, 0f),
            };

            // background 
            Handles.DrawSolidRectangleWithOutline(backgroundPos, Color.black, Color.black);
            
            // frame
            Handles.color = Color.white;
            Handles.DrawWireDisc(area.center, new Vector3(0f, 0f, 1f), backgroundRadius);

            // outside dotted line
            Handles.color = Color.white;
            for (var i = 1; i <= 6; i++)
            {
                Handles.DrawSolidArc(area.center, Vector3.forward, new Vector3(Mathf.Sin((float)i * Time.realtimeSinceStartup), Mathf.Cos((float)i * Time.realtimeSinceStartup), 0f), 30f, outsideDottedLineRadius);
            }

            // outside mask
            Handles.color = Color.black;
            Handles.DrawSolidDisc(area.center, Vector3.forward, outsideMaskRadius);

            // inside dotted line
            Handles.color = Color.white;
            for (var i = 1; i <= 6; i++)
            {
                Handles.DrawSolidArc(area.center, Vector3.forward, new Vector3(Mathf.Cos((float)i * Time.realtimeSinceStartup * 2), Mathf.Sin((float)i * Time.realtimeSinceStartup * 2), 0f), 30f, insideDottedLineRadius);
            }

            // inside mask
            Handles.color = Color.black;
            Handles.DrawSolidDisc(area.center, Vector3.forward, insideMaskRadius);
        }
    }
}
