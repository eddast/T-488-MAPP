using Android.Support.V4.App;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace AMDb.Droid
{
    // Represents each page as a Fragment as long as user can return to page
    public class TabsFragmentPagerAdapter : FragmentPagerAdapter
    {
        // Keeps track of fragments and their titles
        private readonly Fragment[] _fragments;
        private readonly ICharSequence[] _titles;

        public TabsFragmentPagerAdapter(FragmentManager fm, Fragment[] fragments, ICharSequence[] titles) : base(fm)
        {
            this._fragments = fragments;
            this._titles = titles;
        }

        // Override necessary functions for a working fragment pager adapter
        public override ICharSequence GetPageTitleFormatted(int position) { return this._titles[position]; }
        public override Fragment GetItem(int position) { return this._fragments[position]; }
        public override int Count => this._fragments.Length;

    }
}