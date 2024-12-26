using CommunityToolkit.Maui.Views;

namespace TaskManagement.MVVM.Views._Components
{
    public class DropdownPopup : Popup
    {
        public DropdownPopup(List<string> values, Action<string> onItemSelected, string title)
        {
            var theme = Application.Current.RequestedTheme;
            Application.Current.Resources.TryGetValue("Background2", out var primaryColor);
            Application.Current.Resources.TryGetValue("BackgroundDark2", out var primaryDarkColor);

            var color = theme is AppTheme.Dark ? (Color)primaryDarkColor : (Color)primaryColor;

            var titleLabel = new Label
            {
                Text = title,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 5, 10, 15)
            };

            var options = new StackLayout
            {
                BackgroundColor = color,
                Padding = 1
            };

            foreach (var item in values)
            {
                var optionButton = new Button
                {
                    Text = item,
                    BackgroundColor = Colors.Transparent,
                    TextColor = theme is AppTheme.Dark ? Colors.White : Colors.Black,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                optionButton.Clicked += (s, e) =>
                {
                    onItemSelected?.Invoke(item);
                    Close();
                };

                options.Children.Add(optionButton);
            }

            var popupContent = new StackLayout
            {
                Children = { titleLabel, options }
            };

            Content = new Frame
            {
                Content = popupContent,
                BackgroundColor = color,
                WidthRequest = 280,
                Margin = 10,
                CornerRadius = 0
            };
        }
    }
}
