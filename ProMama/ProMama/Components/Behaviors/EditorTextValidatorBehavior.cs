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
            string editorText = editor.Text;

            if (!Ferramentas.ValidarTextoRegex(editorText))
            {
                editor.TextChanged -= OnEditorTextChanged;
                editor.Text = e.OldTextValue;
                editor.TextChanged += OnEditorTextChanged;
            }
        }
    }
}
