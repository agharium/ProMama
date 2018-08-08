using Xamarin.Forms;

namespace ProMama.Components.Behaviors
{
    class EditorTextValidatorBehavior : Behavior<Editor>
    {
        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEditorTextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEditorTextChanged;
        }

        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            var editor = (Editor)sender;

            if (!Ferramentas.ValidarTextoRegex(editor.Text))
            {
                editor.TextChanged -= OnEditorTextChanged;
                editor.Text = e.OldTextValue;
                editor.TextChanged += OnEditorTextChanged;
            } else
            {
                if (editor.Text.Length == 1)
                {
                    editor.TextChanged -= OnEditorTextChanged;
                    editor.Text = editor.Text.ToUpper();
                    editor.TextChanged += OnEditorTextChanged;
                }
            }
        }
    }
}
