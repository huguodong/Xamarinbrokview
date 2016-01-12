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
using Com.Xamarinbrokview;

namespace Sample
{
    [Activity(Label = "Activity1", MainLauncher = true)]
    public class Activity1 : Activity
    {
        private BrokenView mBrokenView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            mBrokenView = BrokenView.Add2Window(this);
            FindViewById<Button>(Resource.Id.button1).SetOnTouchListener(new BrokenTouchListener.Builder(mBrokenView).Build());
            FindViewById<Button>(Resource.Id.button2).SetOnTouchListener(new BrokenTouchListener.Builder(mBrokenView).Build());
            var a = FindViewById<Button>(Resource.Id.button3);
            a.SetOnTouchListener(new BrokenTouchListener.Builder(mBrokenView).Build());
        }
    }
}