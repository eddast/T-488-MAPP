using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;
using IOSWeek1.Services;

namespace IOSWeek1.Droid
{
    [Activity(Label = "Movie Inspector", Theme = "@style/LightTheme")]
    public class MainActivity : FragmentActivity
    {
        public MovieDBService server = new MovieDBService();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var searchFragment = new MovieSearchFragment(server);
            var topFragment = new TopMoviesFragment(server);
            var fragments = new Fragment[] { searchFragment, topFragment  };

            var titles = CharSequence.ArrayFromStringArray(new[] { "Movie Search", "Top Rated" });

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Inspector";

            viewPager.PageSelected += (sender, args) => {
                //Toast.MakeText(ApplicationContext, "YOU SWITCHED TABS!", ToastLength.Long).Show();
                if( args.Position == 1 ) {
                    TopMoviesFragment topMoviesFragment = (TopMoviesFragment)fragments[args.Position];
                    topMoviesFragment.GenerateTopMoviesViewAsync();
                }
            };
        }
    }
}