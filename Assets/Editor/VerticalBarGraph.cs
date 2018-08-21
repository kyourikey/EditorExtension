using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor
{
    public class VerticalBarGraph : EditorWindow
    {
        static VerticalBarGraph _window;

        [MenuItem("Window/Graphs/VerticalBarGraph")]
        static void Open()
        {
            if (_window == null)
            {
                _window = CreateInstance<VerticalBarGraph>();
            }
            _window.titleContent.text = "VerticalBarGraph";
            _window.Show();
        }

        void Update()
        {
            Repaint();
        }

        void OnGUI()
        {
            var area = GUILayoutUtility.GetRect(Screen.width, Screen.height);
            var frameWidth = Screen.width / 6;
            var frameHeight = Screen.height - Screen.height / 5;
            var frameSize = new Vector2(frameWidth, frameHeight);
            var space = Screen.width / 10f;

            var barWidthOffset = 10f;
            var barWidth = frameWidth - barWidthOffset;
            
            // bar -----------------------------------
            var barPositions = new Vector2[]
            {
                new Vector2(area.center.x-(space+frameWidth), area.center.y + (frameHeight/2)),
                new Vector2(area.center.x, area.center.y + (frameHeight/2)),
                new Vector2(area.center.x+(space+frameWidth), area.center.y + (frameHeight/2)),
            };

            var barHeights = new float[]
            {
                (Mathf.Sin(Time.time)/2+0.5f)*frameHeight,
                (Mathf.Cos(Time.time)/2+0.5f)*frameHeight,
                (Mathf.Sin(Time.time*2)/2+0.5f)*frameHeight,
            };
            
            for (var i = 0; i < barPositions.Length; i++)
            {
                var barPosition = barPositions[i];
                var barSize = new Vector2(barWidth, barHeights[i]);

                barPosition.y -= barSize.y / 2;
                
                if(barHeights[i] >  (frameHeight/3))
                    Handles.color = Color.green;
                else if(barHeights[i] > (frameHeight / 5))
                    Handles.color = Color.yellow;
                else
                    Handles.color = Color.red;

                Handles.DrawWireCube(barPosition, barSize);
            }
            
            // frame -----------------------------------
            var framePositions = new Vector2[]
            {
                new Vector2(area.center.x-(space+frameWidth), area.center.y),
                area.center,
                new Vector2(area.center.x+(space+frameWidth), area.center.y),
            };
            Handles.color = Color.green;
            foreach (var framePosition in framePositions)
            {
                Handles.DrawWireCube(framePosition, frameSize);
            }
        }
    }
}
