using Xamarin.Forms;

namespace ProMama.Components.Behaviors
{
    public class EntryNameValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!Ferramentas.ValidarNomeRegex(entry.Text))
            {
                entry.TextChanged -= OnEntryTextChanged;
                entry.Text = e.OldTextValue;
                entry.TextChanged += OnEntryTextChanged;
            } else
            {
                if (entry.Text.Length == 1)
                {
                    entry.TextChanged -= OnEntryTextChanged;
                    entry.Text = entry.Text.ToUpper();
                    entry.TextChanged += OnEntryTextChanged;
                }
            }
        }
    }
}
