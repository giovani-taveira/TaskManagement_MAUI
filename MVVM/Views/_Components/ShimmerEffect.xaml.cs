namespace TaskManagement.MVVM.Views._Components;

public partial class ShimmerEffect : ContentView
{
    private Animation shimmerAnimation;

    public static readonly BindableProperty IsAnimatingProperty = BindableProperty.Create(
        nameof(IsAnimating),
        typeof(bool),
        typeof(ShimmerEffect),
        defaultValue: true,
        propertyChanged: OnIsAnimatingChanged);

    public bool IsAnimating
    {
        get => (bool)GetValue(IsAnimatingProperty);
        set => SetValue(IsAnimatingProperty, value);
    }

    public ShimmerEffect()
    {
        InitializeComponent();
        SetupShimmerAnimation();
    }

    private void SetupShimmerAnimation()
    {
        shimmerAnimation = new Animation
            {
                { 0, 0.5, new Animation(v => shimmerContainer.Opacity = v, 0.3, 1.0, Easing.CubicInOut) }, // Rise
                { 0.5, 1.0, new Animation(v => shimmerContainer.Opacity = v, 1.0, 0.3, Easing.CubicInOut) }  // Fall
            };
    }

    private static void OnIsAnimatingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ShimmerEffect)bindable;

        if ((bool)newValue)
        {
            control.StartShimmerAnimation();
        }
        else
        {
            control.StopShimmerAnimation();
        }
    }

    private void StartShimmerAnimation()
    {
        shimmerAnimation?.Commit(this, "ShimmerEffect", length: 2000, repeat: () => true);
    }

    private void StopShimmerAnimation()
    {
        this.AbortAnimation("ShimmerEffect");
    }
}