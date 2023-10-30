using System;
using TMPro;

namespace Portfolio.Unity.Reactive
{
    public class ObservableInputField : IObservable<string>, IDisposable
    {
        private readonly TMP_InputField _inputField;

        private IObserver<string>? _observer;

        public ObservableInputField(TMP_InputField inputField)
        {
            _inputField = inputField;
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            _observer = observer;
            _inputField.onValueChanged.AddListener(OnInputValueChanged);

            return this;
        }

        public void Dispose()
        {
            _inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        }

        private void OnInputValueChanged(string value)
        {
            _observer?.OnNext(value);
        }
    }
}
