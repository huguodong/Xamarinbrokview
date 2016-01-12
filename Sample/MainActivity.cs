using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Xamarinbrokview;
using Android.Graphics;
using Android.Content.Res;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Sample
{
    [Activity(Label = "Sample", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, SeekBar.IOnSeekBarChangeListener, CompoundButton.IOnCheckedChangeListener, Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
    {
        private static BrokenView mBrokenView;

        private RelativeLayout parentLayout;
        private ImageView imageView;
        private ListView listView;
        private MyView myView;
        private Button button;
        private Boolean hasAlpha;
        // public int resids[]=new int { };
        public List<int> resids;
        private static SeekBar complexitySeekbar;
        private static SeekBar breakSeekbar;
        private static SeekBar fallSeekbar;
        private static SeekBar radiusSeekbar;
        private Android.Support.V7.Widget.Toolbar toolbar;
        private static BrokenTouchListener colorfulListener;
        private static BrokenTouchListener whiteListener;
        private static Paint whitePaint;
        private static Boolean effectEnable = true;

        public static TextView complexityTv;
        public static TextView breakTv;
        public static TextView fallTv;
        public static TextView radiusTv;
        public BrokenCallback callback;
        public static MainActivity mcontext;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            mcontext = this;
            InitView();
            mBrokenView = BrokenView.Add2Window(this);

            whitePaint = new Paint();
            whitePaint.Color = Color.Red;
            colorfulListener = new BrokenTouchListener.Builder(mBrokenView).Build();
            whiteListener = new BrokenTouchListener.Builder(mBrokenView).
               SetPaint(whitePaint).Build();
            SetOnTouchListener();
        }
        private void InitView()
        {
            parentLayout = FindViewById<RelativeLayout>(Resource.Id.demo_parent);
            imageView = FindViewById<ImageView>(Resource.Id.demo_image);
            listView = FindViewById<ListView>(Resource.Id.demo_list);
            myView = FindViewById<MyView>(Resource.Id.demo_myview);
            button = FindViewById<Button>(Resource.Id.demo_button);

            button.Click += delegate
            { Toast.MakeText(this, "Button onClick", ToastLength.Short).Show(); };
            TypedArray ar = Resources.ObtainTypedArray(Resource.Array.imgArray);
            int len = ar.Length();
            resids = new List<int>();
            for (int i = 0; i < len; i++)
            { resids.Add(ar.GetResourceId(i, 0)); }

            InitSeekBar();
            InitToggleButton();
            InitToolbar();
            InitDrawerLayout();
            RefreshDate();

        }
        private void InitSeekBar()
        {
            complexitySeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_complexity);
            breakSeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_break);
            fallSeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_fall);
            radiusSeekbar = FindViewById<SeekBar>(Resource.Id.seekbar_radius);
            complexityTv = FindViewById<TextView>(Resource.Id.complexity_value);
            breakTv = FindViewById<TextView>(Resource.Id.break_value);
            fallTv = FindViewById<TextView>(Resource.Id.fall_value);
            radiusTv = FindViewById<TextView>(Resource.Id.radius_value);

            SeekBar.IOnSeekBarChangeListener listener = this;
            complexitySeekbar.SetOnSeekBarChangeListener(listener);
            breakSeekbar.SetOnSeekBarChangeListener(listener);
            fallSeekbar.SetOnSeekBarChangeListener(listener);
            radiusSeekbar.SetOnSeekBarChangeListener(listener);
        }
        private void InitToggleButton()
        {
            ToggleButton effectBtn = FindViewById<ToggleButton>(Resource.Id.toggle_effect);
            ToggleButton callbackBtn = FindViewById<ToggleButton>(Resource.Id.toggle_callback);
            callback = new MyCallBack();
            effectBtn.SetOnCheckedChangeListener(this);
            callbackBtn.SetOnCheckedChangeListener(this);
        }
        private void InitToolbar()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            toolbar.SetOnMenuItemClickListener(this);
        }
        private void SetViewVisible()
        {
            parentLayout.Visibility = ViewStates.Visible;
            imageView.Visibility = ViewStates.Visible;
            listView.Visibility = ViewStates.Visible;
            myView.Visibility = ViewStates.Visible;
            button.Visibility = ViewStates.Visible;
        }
        public void RefreshDate()
        {
            Random rand = new Random();

            List<ListItem> items = new List<ListItem>();
            for (int i = 0; i < 20; i++)
            {
                Color color = Color.Argb(0xff, rand.Next(0x99), rand.Next(0x99), rand.Next(0x99));
                ListItem item = new ListItem(color, "list item " + i);
                items.Add(item);
            }
            listView.SetAdapter(new SampleAdapter(this, Android.Resource.Layout.SimpleListItem1, items));

            int pos = rand.Next(resids.Count);
            imageView.SetImageResource(resids[pos]);
            if (pos == 0 || pos == 1 || pos == 2)
                hasAlpha = true;
            else
                hasAlpha = false;
        }

        private void InitDrawerLayout()
        {
            DrawerLayout mDrawerLayout = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);
            ActionBarDrawerToggle mDrawerToggle = new NewActionBarDrawerToggle(this, mDrawerLayout,
                toolbar, Resource.String.app_name, Resource.String.app_name);

            mDrawerToggle.SyncState();
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
        }

        public void SetOnTouchListener()
        {

            parentLayout.SetOnTouchListener(colorfulListener);
            button.SetOnTouchListener(colorfulListener);
            myView.SetOnTouchListener(whiteListener);
            listView.SetOnTouchListener(colorfulListener);
            if (hasAlpha)
                imageView.SetOnTouchListener(whiteListener);
            else
                imageView.SetOnTouchListener(colorfulListener);
        }
        private class MyCallBack : BrokenCallback
        {
            public override void OnStart(View p0)
            {
                mcontext.ShowCallback(p0, "onStart");
            }
            public override void OnCancel(View p0)
            {
                mcontext.ShowCallback(p0, "OnCancel");
            }
            public override void OnRestart(View p0)
            {
                mcontext.ShowCallback(p0, "OnRestart");
            }
            public override void OnFalling(View p0)
            {
                mcontext.ShowCallback(p0, "OnFalling");
            }
            public override void OnCancelEnd(View p0)
            {
                mcontext.ShowCallback(p0, "OnCancelEnd");
            }

        }
        public void ShowCallback(View v, String s)
        {
            switch (v.Id)
            {
                case Resource.Id.demo_parent:
                    Snackbar.Make(parentLayout, "RelativeLayout---" + s, Snackbar.LengthShort).Show();
                    break;
                case Resource.Id.demo_image:
                    Snackbar.Make(parentLayout, "ImageView---" + s, Snackbar.LengthShort).Show();
                    break;
                case Resource.Id.demo_list:
                    Snackbar.Make(parentLayout, "ListView---" + s, Snackbar.LengthShort).Show();
                    break;
                case Resource.Id.demo_myview:
                    Snackbar.Make(parentLayout, "CustomView---" + s, Snackbar.LengthShort).Show();
                    break;
                case Resource.Id.demo_button:
                    Snackbar.Make(parentLayout, "Button---" + s, Snackbar.LengthShort).Show();
                    break;
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return true;
        }
        private class SampleAdapter : ArrayAdapter<ListItem>
        {
            private LayoutInflater mInflater;
            private int mResource;
            private List<ListItem> items;

            public SampleAdapter(Context context, int layoutResourceId, List<ListItem> data) : base(context, layoutResourceId, data)
            {

                mInflater = LayoutInflater.From(context);
                mResource = layoutResourceId;
                items = data;
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    convertView = mInflater.Inflate(mResource, parent, false);
                }
                var col = items[position].color;

                convertView.SetBackgroundColor(col);
                ((TextView)convertView).Text = items[position].text;
                ((TextView)convertView).SetTextColor(Color.Red);

                return convertView;
            }
        }
        #region
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            switch (seekBar.Id)
            {
                case Resource.Id.seekbar_complexity:
                    complexityTv.Text = "" + (progress + 8);
                    break;
                case Resource.Id.seekbar_break:
                    breakTv.Text = (progress + 500) + "ms";
                    break;
                case Resource.Id.seekbar_fall:
                    fallTv.Text = (progress + 1000) + "ms";
                    break;
                case Resource.Id.seekbar_radius:
                    radiusTv.Text = (progress + 20) + "dp";
                    break;
            }
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {

        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            switch (buttonView.Id)
            {
                case Resource.Id.toggle_effect:
                    effectEnable = isChecked;
                    break;
                case Resource.Id.toggle_callback:
                    mBrokenView.SetCallback(isChecked ? callback : null);
                    break;
            }
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_refresh:
                    mBrokenView.Reset();
                    RefreshDate();
                    SetOnTouchListener();
                    SetViewVisible();
                    break;
            }
            return true;
        }
        #endregion
        public class NewActionBarDrawerToggle : ActionBarDrawerToggle
        {
            public NewActionBarDrawerToggle(Activity activity, DrawerLayout drawerLayout, Android.Support.V7.Widget.Toolbar toolbar, int openDrawerContentDescRes, int closeDrawerContentDescRes) : base(activity, drawerLayout, toolbar, openDrawerContentDescRes, closeDrawerContentDescRes)
            {

            }

            public override void OnDrawerOpened(View drawerView)
            {
                base.OnDrawerOpened(drawerView);
                mBrokenView.Enable = false;
            }
            public override void OnDrawerClosed(View drawerView)
            {
                base.OnDrawerClosed(drawerView);
                colorfulListener = new BrokenTouchListener.Builder(mBrokenView).
                       SetComplexity(complexitySeekbar.Progress + 8).
                       SetBreakDuration(breakSeekbar.Progress + 500).
                       SetFallDuration(fallSeekbar.Progress + 1000).
                       SetCircleRiftsRadius(radiusSeekbar.Progress + 20).
                       Build();
                whiteListener = new BrokenTouchListener.Builder(mBrokenView).
                        SetComplexity(complexitySeekbar.Progress + 8).
                       SetBreakDuration(breakSeekbar.Progress + 500).
                        SetFallDuration(fallSeekbar.Progress + 1000).
                        SetCircleRiftsRadius(radiusSeekbar.Progress + 20).
                        SetPaint(whitePaint).
                        Build();

                mcontext.SetOnTouchListener();

                mBrokenView.Enable = effectEnable;
            }
        }
        public class ListItem
        {
            public Color color;
            public String text;
            public ListItem(Color color, String text)
            {
                this.color = color;
                this.text = text;
            }
        }
    }
}

