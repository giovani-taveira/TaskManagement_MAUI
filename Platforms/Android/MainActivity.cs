using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;

namespace TaskManagement
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ApplyTheme(Resources.Configuration.UiMode);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            ApplyTheme(newConfig.UiMode);
        }

        private void ApplyTheme(UiMode uiMode)
        {
            var nightModeFlags = uiMode & UiMode.NightMask;

            if (nightModeFlags == UiMode.NightYes)
            {
                SetTheme(Resource.Style.NightTheme);
            }
            else
            {
                SetTheme(Resource.Style.MainTheme);
            }
        }
    }
}
