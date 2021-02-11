using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Hangman
{
//Custom listview to set data with two text views
  public  class PlayerAdapter:BaseAdapter<Player>
    {
        public readonly Activity context;
        public readonly List<Player> items;
        public PlayerAdapter(Activity context,List<Player> items)
        {
            this.context = context;
            this.items = items;
        }
        public override Player this[int position]
        {
            get
            {
               
                return items[position];
            }
        }
        public override int Count
        {
            get
            {
               
                return items.Count;
            }
        }
        public override long GetItemId(int position)
        {
           
            return position;
        }
        //it wil set views on custom listview
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
           
            var item = items[position];
            var view = convertView;
            if (view == null)

            {
                view = context.LayoutInflater.Inflate(Resource.Layout.list_adapter, null);
            }
            view.FindViewById<TextView>(Resource.Id.nametxt).Text = item.PlayerName;
            view.FindViewById<TextView>(Resource.Id.scoretxt).Text = "Best: " + item.BestStreak;
             return view;
        }
    }
}