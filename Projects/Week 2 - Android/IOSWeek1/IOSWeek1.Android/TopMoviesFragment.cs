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
        private ListView _listView;
        private List<MovieModel> _topMovies;

        public TopMoviesFragment(MovieDBService server) { this._server = server; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            //_topMovies = new List<MovieModel>();
            _rootView = inflater.Inflate(Resource.Layout.TopRatedMovies, container, false);
            _listView = _rootView.FindViewById<ListView>(Resource.Id.listView);
            //_spinner = _rootView.FindViewById<ProgressBar>(Resource.Id.spinner);

            new Thread(GenerateTopMoviesViewAsync).Start();

            this._listView.ItemClick += (sender, args) => {

                var intent = new Intent(this.Context, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(_topMovies[args.Position]));
                this.StartActivity(intent);
            };

            return _rootView;
        }

        public async void GenerateTopMoviesViewAsync()
        {
            var spinner = _rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
            Activity.RunOnUiThread(() => {
                _topMovies = new List<MovieModel>();
                spinner.Visibility = ViewStates.Visible;
            });

            _topMovies = await _server.GetTopMoviesViewAsync();

            Activity.RunOnUiThread(() => {
                _listView.Adapter = new MovieListAdapter(this.Activity, _topMovies);
                spinner.Visibility = ViewStates.Gone;
            });
        }
    }
}