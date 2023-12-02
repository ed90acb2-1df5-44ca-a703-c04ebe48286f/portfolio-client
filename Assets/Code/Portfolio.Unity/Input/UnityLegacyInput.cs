using System.Collections.Generic;
using Portfolio.Core.Input;
using UnityEngine;

namespace Portfolio.Unity.Input
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
            return UnityEngine.Input.GetKey(_keyBindings[key]);
        }

        public bool IsKeyPressed(InputKey key)
        {
            return UnityEngine.Input.GetKeyDown(_keyBindings[key]);
        }

        public bool IsKeyReleased(InputKey key)
        {
            return UnityEngine.Input.GetKeyUp(_keyBindings[key]);
        }
    }
}
