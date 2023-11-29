using System.Collections.Generic;
using Portfolio.Core;
using UnityEngine;

namespace Portfolio.Unity
{
    public class UnityLegacyInput : IInput
    {
        private readonly Dictionary<InputKey, KeyCode> _keyBindings = new()
        {
            {InputKey.Left, KeyCode.A},
            {InputKey.Right, KeyCode.D},
            {InputKey.Up, KeyCode.W},
            {InputKey.Down, KeyCode.S},
        };

        public bool IsKeyDown(InputKey key)
        {
            return Input.GetKey(_keyBindings[key]);
        }

        public bool IsKeyPressed(InputKey key)
        {
            return Input.GetKeyDown(_keyBindings[key]);
        }

        public bool IsKeyReleased(InputKey key)
        {
            return Input.GetKeyUp(_keyBindings[key]);
        }
    }
}
