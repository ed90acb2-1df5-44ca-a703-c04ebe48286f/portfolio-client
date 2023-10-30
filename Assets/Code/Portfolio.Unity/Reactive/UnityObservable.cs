using System;
using TMPro;

namespace Portfolio.Unity.Reactive
{
    public static class UnityObservable
    {
        public static IObservable<string> FromInput(TMP_InputField inputField)
        {
            return new ObservableInputField(inputField);
        }
    }
}
