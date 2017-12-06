using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using IOSWeek1.Services;
using Android.Content;
using Newtonsoft.Json;
using System.Collections.Generic;
using Fragment = Android.Support.V4.App.Fragment;
using ListFragment = Android.Support.V4.App.ListFragment;
using System.Threading;

namespace IOSWeek1.Droid
{
    internal class TopMoviesFragment : Fragment
    {
        private MovieDBService _server;
        private View _rootView;
        public ListView listView;
        public List<MovieModel> topMovies = new List<MovieModel>();

        public TopMoviesFragment(MovieDBService server) { this._server = server; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            _rootView = inflater.Inflate(Resource.Layout.TopRatedMovies, container, false);
            listView = _rootView.FindViewById<ListView>(Resource.Id.listView);

            this.listView.ItemClick += (sender, args) => {

                var intent = new Intent(this.Context, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(topMovies[args.Position]));
                this.StartActivity(intent);
            };

            return _rootView;
        }

        public async void ReloadAsync()
        {
            listView = _rootView.FindViewById<ListView>(Resource.Id.listView);

            var spinner = _rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
            Activity.RunOnUiThread(() => {
                topMovies = new List<MovieModel>();
                listView.Adapter = new MovieListAdapter(this.Activity, topMovies);
                spinner.Visibility = ViewStates.Visible;
            });

            topMovies = await _server.GetTopMoviesViewAsync();

            Activity.RunOnUiThread(() => {
                listView.Adapter = new MovieListAdapter(this.Activity, topMovies);
                spinner.Visibility = ViewStates.Gone;
            });
        }
    }
}