using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Flea.Utility
{
    public class CustomValidator : ComponentBase
    {
        private ValidationMessageStore? _messageStore;

        [CascadingParameter] private EditContext? CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext is null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CustomValidator)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. " +
                    $"For example, you can use {nameof(CustomValidator)} " +
                    $"inside an {nameof(EditForm)}.");
            }

            _messageStore = new ValidationMessageStore(CurrentEditContext);

            CurrentEditContext.OnValidationRequested += (_, _) =>
                _messageStore?.Clear();
            CurrentEditContext.OnFieldChanged += (_, e) =>
            {
                if (e == null) throw new ArgumentNullException(nameof(e));
                _messageStore?.Clear(e.FieldIdentifier);
            };
        }

        public void DisplayErrors(Dictionary<string, List<string>> errors)
        {
            if (CurrentEditContext is not null)
            {
                foreach (var (key, value) in errors)
                {
                    _messageStore?.Add(CurrentEditContext.Field(key), value);
                }

                CurrentEditContext.NotifyValidationStateChanged();
            }
        }

        public void ClearErrors()
        {
            _messageStore?.Clear();
            CurrentEditContext?.NotifyValidationStateChanged();
        }
    }
}