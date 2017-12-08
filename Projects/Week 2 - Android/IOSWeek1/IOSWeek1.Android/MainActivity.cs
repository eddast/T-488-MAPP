using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Fragment = Android.Support.V4.App.Fragment;
using IOSWeek1.Services;

namespace IOSWeek1.Droid
{
    [Activity(Label = "AMDB", Theme = "@style/LightTheme")]
    public class MainActivity : FragmentActivity
    {
        // Initialize server model to pass down to fragments that need it
        public static MovieDBService server;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); // call to base func

            // Set our view from the "main" layout resource
            // Initiate tab fragments and their titles
            SetContentView(Resource.Layout.Main);
            var fragments = new Fragment[] {    new MovieSearchFragment(server),
                                                new TopMoviesFragment(server)  };
            var titles = CharSequence.ArrayFromStringArray(new[] {  "Movie Search",
                                                                    "Top Rated" });

            // Bind fragments and titles to tabs to setup toolbar
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);
            TabLayout tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);
            tabLayout.TabGravity = TabLayout.GravityFill;
            tabLayout.TabMode = TabLayout.ModeFixed;

            // Set action bar to toolbar and set title
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar); this.ActionBar.Title = "AMDB";

            // Listens to toolbar
            FragmentClickListener(viewPager, fragments);
        }

        // Listens to toolbar for fragment (tab) click and takes necessary actions
        private void FragmentClickListener(ViewPager viewPager, Fragment[] fragments)
        {
            viewPager.PageSelected += (sender, args) => {

                // If position is not at movie search fragment
                // Keyboard maximized due to that fragments input field should hide
                if(args.Position != 0) {
                    
                    MovieSearchFragment movieSearchFragment = (MovieSearchFragment)fragments[0];
                    if (movieSearchFragment.manager != null) {
                        
                        movieSearchFragment.manager.HideSoftInputFromWindow(movieSearchFragment.movieField.WindowToken, 0);
                    }
                }

                // If a click triggered fragment position 1 (top rated movies fragments)
                // The fragment list view is "reloaded"
                if (args.Position == 1){
                    
                    TopMoviesFragment topMoviesFragment = (TopMoviesFragment)fragments[args.Position];
                    topMoviesFragment.ReloadAsync();
                }
            };
        }
    }
}