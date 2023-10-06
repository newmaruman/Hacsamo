using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;
using Com.TheFallenGames.OSA.DataHelpers;

// You should modify the namespace to your own or - if you're sure there won't ever be conflicts - remove it altogether
namespace Kmr.Grids
{
	// There is 1 important callback you need to implement, apart from Start(): UpdateCellViewsHolder()
	// See explanations below
	public class BasicGridAdapter : GridAdapter<GridParams, MyGridItemViewsHolder>
	{
		// Helper that stores data and notifies the adapter when items count changes
		// Can be iterated and can also have its elements accessed by the [] operator
		public SimpleDataHelper<MainGridItemModel> Data { get; private set; }


		#region GridAdapter implementation
		protected override void Start()
		{
			Data = new SimpleDataHelper<MainGridItemModel>(this);

			// Calling this initializes internal data and prepares the adapter to handle item count changes
			base.Start();


			//RetrieveDataAndUpdate(5);
			// Retrieve the models from your data source and set the items count
			/*
			RetrieveDataAndUpdate(1500);
			*/
		}

		// This is called anytime a previously invisible item become visible, or after it's created, 
		// or when anything that requires a refresh happens
		// Here you bind the data from the model to the item's views
		// *For the method's full description check the base implementation
		protected override void UpdateCellViewsHolder(MyGridItemViewsHolder newOrRecycled)
		{
			// In this callback, "newOrRecycled.ItemIndex" is guaranteed to always reflect the
			// index of item that should be represented by this views holder. You'll use this index
			// to retrieve the model from your data set

			//MainGridItemModel model = Data[newOrRecycled.ItemIndex];
			MainGridItemModel model = Data[newOrRecycled.ItemIndex];
			newOrRecycled.menuItem.Show(model.listItem);

			//newOrRecycled.backgroundImage.color = model.color;
			//newOrRecycled.titleText.text = model.title + " #" + newOrRecycled.ItemIndex;
			//Debug.Log("Is it alwyas Updated?");
		}

		// This is the best place to clear an item's views in order to prepare it from being recycled, but this is not always needed, 
		// especially if the views' values are being overwritten anyway. Instead, this can be used to, for example, cancel an image 
		// download request, if it's still in progress when the item goes out of the viewport.
		// <newItemIndex> will be non-negative if this item will be recycled as opposed to just being disabled
		// *For the method's full description check the base implementation
		/*
		protected override void OnBeforeRecycleOrDisableCellViewsHolder(MyGridItemViewsHolder inRecycleBinOrVisible, int newItemIndex)
		{
			base.OnBeforeRecycleOrDisableCellViewsHolder(inRecycleBinOrVisible, newItemIndex);
		}
		*/
		#endregion

		// These are common data manipulation methods
		// The list containing the models is managed by you. The adapter only manages the items' sizes and the count
		// The adapter needs to be notified of any change that occurs in the data list. 
		// For GridAdapters, only Refresh and ResetItems work for now
		#region data manipulation

		/*
		public void AddItemsAt(int index, IList<MainGridItemModel> items)
		{
			//Commented: this only works with Lists. ATM, Insert for Grids only works by manually changing the list and calling NotifyListChangedExternally() after
			//Data.InsertItems(index, items);
			Debug.Log("When is this call??");
			Data.List.InsertRange(index, items);
			Data.NotifyListChangedExternally();
			
		}
		*/

		/*
		public void RemoveItemsFrom(int index, int count)
		{
			//Commented: this only works with Lists. ATM, Remove for Grids only works by manually changing the list and calling NotifyListChangedExternally() after
			//Data.RemoveRange(index, count);
			Debug.Log("When is this call?? 2");
			Data.List.RemoveRange(index, count);
			Data.NotifyListChangedExternally();
		}
		*/

		/*
		public void SetItems(IList<MainGridItemModel> items)
		{
			Debug.Log("When is this call?? 3");
			Data.ResetItems(items);
		}
		*/
		#endregion


		// Here, we're requesting <count> items from the data source
		/*
		void RetrieveDataAndUpdate(int count)
		{
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate(count));
			
		}
		*/

		// Retrieving <count> models from the data source and calling OnDataRetrieved after.
		// In a real case scenario, you'd query your server, your database or whatever is your data source and call OnDataRetrieved after


		/*
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate(int count)
		{
			// Simulating data retrieving delay
			yield return new WaitForSeconds(.5f);
			
			var newItems = new MainGridItemModel[count];

			// Retrieve your data here
			
			for (int i = 0; i < count; ++i)
			{
				var model = new MainGridItemModel()
				{
					title = "Random item ",
					color = new Color(
								UnityEngine.Random.Range(0f, 1f),
								UnityEngine.Random.Range(0f, 1f),
								UnityEngine.Random.Range(0f, 1f),
								UnityEngine.Random.Range(0f, 1f)
							)
				};
				newItems[i] = model;
			}
			

			////OnDataRetrieved(newItems);
		}
		*/

		/*

		void OnDataRetrieved(MainGridItemModel[] newItems)
		{
			//Commented: this only works with Lists. ATM, Insert for Grids only works by manually changing the list and calling NotifyListChangedExternally() after
			// Data.InsertItemsAtEnd(newItems);

			Data.List.AddRange(newItems);
			Data.NotifyListChangedExternally();
			Debug.Log("When is this updated??");
		} 
		*/

	}


	// Class containing the data associated with an item
	public class MainGridItemModel
	{
		
		public ListItem listItem;
		//public DelegateParam cb;
		
		/*
		public string title;
		public Color color;
		*/
	}


	// This class keeps references to an item's views.
	// Your views holder should extend BaseItemViewsHolder for ListViews and CellViewsHolder for GridViews
	// The cell views holder should have a single child (usually named "Views"), which contains the actual 
	// UI elements. A cell's root is never disabled - when a cell is removed, only its "views" GameObject will be disabled
	public class MyGridItemViewsHolder : CellViewsHolder
	{
		
		public BasicGridItem menuItem;
		/*
		public Text titleText;
		public Image backgroundImage;
		*/


		// Retrieving the views from the item's root GameObject
		public override void CollectViews()
		{
			base.CollectViews();

			menuItem = root.GetComponent<BasicGridItem>();
			

			//views.GetComponentAtPath("TitleText", out menuItem);

			// GetComponentAtPath is a handy extension method from frame8.Logic.Misc.Other.Extensions
			// which infers the variable's component from its type, so you won't need to specify it yourself
			/*
			views.GetComponentAtPath("TitleText", out titleText);
			views.GetComponentAtPath("BackgroundImage", out backgroundImage);
			*/
		}

		// This is usually the only child of the item's root and it's called "Views". 
		// That's what the default implementation will look for, but just for flexibility, 
		// this callback is provided, in case it's named differently or there's more than 1 child 
		// *See GridExample.cs for more info
		/*
		protected override RectTransform GetViews()
		{ return root.Find("Views").transform as RectTransform; }
		*/

		// Override this if you have children layout groups. They need to be marked for rebuild when this callback is fired
		/*
		public override void MarkForRebuild()
		{
			base.MarkForRebuild();

			LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout1);
			LayoutRebuilder.MarkLayoutForRebuild(yourChildLayout2);
			AChildSizeFitter.enabled = true;
		}
		*/

		// Override this if you've also overridden MarkForRebuild()
		/*
		public override void UnmarkForRebuild()
		{
			AChildSizeFitter.enabled = false;

			base.UnmarkForRebuild();
		}
		*/
	}
}