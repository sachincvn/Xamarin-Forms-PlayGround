using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FormsPlay.UI.Controls
{
    [Preserve(AllMembers = true)]
    public class ParallaxListView : ListView
    {

        public static readonly BindableProperty ItemContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(ParallaxListView), default(View));

        public View Content
        {
            get { return (View)GetValue(ItemContentProperty); }
            set { SetValue(ItemContentProperty, value); }
        }

        public ParallaxListView()
            : base(ListViewCachingStrategy.RetainElement)
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                this.ItemSelected += this.ParallaxListView_ItemSelected;
            }

            ItemTemplate = new DataTemplate(() =>
            {
                return new ViewCell { View = Content };
            });
            VerticalScrollBarVisibility = ScrollBarVisibility.Never;
            var item = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                item.Add("Hello");
            }
            ItemsSource = item;
        }

        public event EventHandler<SelectedItemChangedEventArgs> SelectionChanged;

        public event EventHandler<ScrollChangedEventArgs> ScrollChanged;

        public double WidthInPixel { get; set; }

        public static void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ((ParallaxListView)sender)?.ScrollChanged?.Invoke((ParallaxListView)sender, e);
        }

        public static void OnSelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            ParallaxListView listView = (ParallaxListView)sender;
            if (listView != null)
            {
                listView.SelectionChanged(sender, e);
                listView.SelectedItem = e?.SelectedItem;
                listView.SelectedItem = null;
            }
        }

        public static void OnSelectionChanged(object sender, int index)
        {
            if (sender is ParallaxListView)
            {
                var listView = sender as ParallaxListView;
                OnSelectionChanged(sender, new SelectedItemChangedEventArgs((listView.ItemsSource as IList)[index], index));
            }
        }

        private void ParallaxListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            OnSelectionChanged(this, e);
        }
    }

    public class ScrollChangedEventArgs : EventArgs
    {
        public ScrollChangedEventArgs(int position)
        {
            this.Position = position;
        }

        public int Position { get; set; }
    }
}