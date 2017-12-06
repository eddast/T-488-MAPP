using Android.Views;
using Android.Widget;
using Android.OS;
using IOSWeek1.Services;
using Android.Content;
using Newtonsoft.Json;
using System.Collections.Generic;
using Fragment = Android.Support.V4.App.Fragment;

namespace IOSWeek1.Droid
{
    internal class TopMoviesFragment : Fragment
    {
        private MovieDBService _server;
        private View _rootView;
        private ListView _listView;
        private List<MovieModel> _topMovies;

        public TopMoviesFragment(MovieDBService server)
        {
            this._server = server;
            this._topMovies = new List<MovieModel>();
        }

        // Initiate the fragment view and listen to cell clicks
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            _rootView = inflater.Inflate(Resource.Layout.TopRatedMovies, container, false);
            _listView = _rootView.FindViewById<ListView>(Resource.Id.listView);
            ListenToCellClicks();


            return _rootView;
        }

        // Listens for cell click and starts the detail movie view activity
        // passes down movie information in json bundle for detail view
        public void ListenToCellClicks()
        {
            this._listView.ItemClick += (sender, args) => {

                var intent = new Intent(this.Context, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(_topMovies[args.Position]));
                this.StartActivity(intent);
            };
        }

        // Reloads data in fragment's listview
        public async void ReloadAsync()
        {
            // Activate spinner to indicate background process
            var spinner = _rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
            spinner.Visibility = ViewStates.Visible;

            // Clear list and set up empty view while loading
            _listView = _rootView.FindViewById<ListView>(Resource.Id.listView);
            _topMovies.Clear();
            _listView.Adapter = new MovieListAdapter(this.Activity, _topMovies);

            // Retrieve top movies from server and set up list view accordingly
            _topMovies = await _server.GetTopMoviesViewAsync();
            _listView.Adapter = new MovieListAdapter(this.Activity, _topMovies);

            // Dismiss spinner since process is done
            spinner.Visibility = ViewStates.Gone;
        }
    }
}