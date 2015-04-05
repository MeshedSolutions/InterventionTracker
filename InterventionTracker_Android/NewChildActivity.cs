﻿
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

namespace InterventionTracker_Android
{
	[Activity (Label = "NewChildActivity")]			
	public class NewChildActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.NewChild);

			var addChildButton = FindViewById<Button> (Resource.Id.addChild);
			addChildButton.Click += AddChildClicked;
		}

		private void AddChildClicked(object sender, EventArgs e)
		{
			ChildRepository childRepository = new ChildRepository ();

			var name = FindViewById<EditText> (Resource.Id.nameText);
			var dob = FindViewById<EditText> (Resource.Id.dobText);
			var unit = FindViewById<EditText> (Resource.Id.unitText);
			var status = FindViewById<TextView> (Resource.Id.childAddStatus);

			Child child = new Child ();

			child.FullName = name.Text;
			child.DateOfBirth = dob.Text;
			child.Unit = unit.Text;

			try
			{
				childRepository.AddChildAsync (child);
				status.Text = "Child Added Successfully";
			}
			catch(Exception exception) {
			}
		}
	}
}

