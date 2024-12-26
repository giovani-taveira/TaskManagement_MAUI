namespace TaskManagement.MVVM.Views._Components
{
    public class CircularProgressDrawable : BindableObject, IDrawable
    {
        // Propriedade Bindable para o progresso
        public static readonly BindableProperty ProgressProperty =
             BindableProperty.Create(nameof(Progress), typeof(double), typeof(CircularProgressDrawable), 0.0, propertyChanged: OnProgressChanged);

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        // Propriedade Bindable para o texto no centro
        public static readonly BindableProperty TextProperty =
             BindableProperty.Create(nameof(Text), typeof(string), typeof(CircularProgressDrawable), string.Empty, propertyChanged: OnProgressChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CircularProgressDrawable drawable)
            {
                drawable.Invalidate?.Invoke(); // Redesenha quando o progresso muda
            }
        }

        // Ação para invalidar o desenho (chamada pelo GraphicsView)
        public Action Invalidate { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            //Desenha o progresso da tarefa
            var theme = Application.Current.RequestedTheme;
            Application.Current.Resources.TryGetValue("Primary", out var primaryColor);
            Application.Current.Resources.TryGetValue("PrimaryDark", out var primaryDarkColor);

            float centerX = dirtyRect.Center.X;
            float centerY = dirtyRect.Center.Y;
            float radius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2;

            float strokeWidth = 2;
            canvas.StrokeSize = strokeWidth;

            canvas.StrokeColor = Colors.LightGray;
            canvas.DrawCircle(centerX, centerY, radius - strokeWidth / 2);

            canvas.StrokeColor = theme is AppTheme.Dark ? (Color)primaryDarkColor : (Color)primaryColor;
            float angle = (float)(360 * Progress);
            canvas.DrawArc(
                centerX - radius + strokeWidth / 2, 
                centerY - radius + strokeWidth / 2,
                centerX + radius - strokeWidth / 2, 
                centerY + radius - strokeWidth / 2,
                0, angle , false, false);

            //Exibindo a qauntidade de tarefas no meio do circulo
            canvas.FontColor = theme is AppTheme.Dark ? Colors.White : Colors.Black;
            canvas.FontSize = 16;
            canvas.Font = Microsoft.Maui.Graphics.Font.Default;               

            var text = Text ?? "0/0";
            var textMetrics = canvas.GetStringSize(text, Microsoft.Maui.Graphics.Font.Default, 16);
            float textX = centerX - (textMetrics.Width / 2);
            float textY = centerY - (textMetrics.Height / 2) + (textMetrics.Height * 0.85f); ;

            canvas.DrawString(text, textX, textY, HorizontalAlignment.Left);
        }
    }
}
