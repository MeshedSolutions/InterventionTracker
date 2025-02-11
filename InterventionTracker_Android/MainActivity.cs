﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterventionTracker_Android
{
	[Activity (Label = "Intervention Tracker", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ListView childListView;
		List<Child> children;
		
		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get the data for the layout
			children = await new ChildRepository ().GetAllAsync ();


			// Control References
			var addChildButton = FindViewById<Button>(Resource.Id.AddChild);
			childListView = FindViewById<ListView>(Resource.Id.ChildList);

			// Events
			addChildButton.Click += OnAddChildClick;
			childListView.ItemClick += OnChildListItemClick;

			// Data Binding
			childListView.Adapter = new ChildAdapter(this, children);
		}

		void OnAddChildClick(object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(NewChildActivity));
			StartActivity (intent);
		}

		void OnChildListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var intent = new Intent (this, typeof(ChildDetailActivity));
			intent.PutExtra ("childID", e.Id);
			StartActivity (intent);
		}

		async Task<List<Child>> GetChildList()
		{
			List<Child> childList;

			childList = await new ChildRepository ().GetAllAsync ();

			return childList;
		}

		protected async override void OnResume ()
		{
			base.OnResume ();

			children = await new ChildRepository ().GetAllAsync ();
			childListView.Adapter = new ChildAdapter (this, children);
		}
	}
}


