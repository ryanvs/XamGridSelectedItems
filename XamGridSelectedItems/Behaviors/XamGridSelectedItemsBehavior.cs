using Caliburn.Micro;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

namespace XamGridSelectedItems
{
    public class XamGridSelectedItemsBehavior : Behavior<XamGrid>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItem",
                typeof(object),
                typeof(XamGridSelectedItemsBehavior),
                new FrameworkPropertyMetadata(default(object)));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItems",
                typeof(BindableCollection<object>),
                typeof(XamGridSelectedItemsBehavior),
                new FrameworkPropertyMetadata(default(BindableCollection<object>)));

        public XamGrid Grid
        {
            get { return AssociatedObject as XamGrid; }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public BindableCollection<object> SelectedItems
        {
            get { return (BindableCollection<object>)GetValue(SelectedItemsProperty); }
            set { AssociatedObject.SetValue(SelectedItemsProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectedCellsCollectionChanged += OnSelectedCellsCollectionChanged;
            AssociatedObject.SelectedRowsCollectionChanged += OnSelectedRowsCollectionChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.SelectedCellsCollectionChanged -= OnSelectedCellsCollectionChanged;
            AssociatedObject.SelectedRowsCollectionChanged -= OnSelectedRowsCollectionChanged;
            base.OnDetaching();
        }

        private void OnSelectedCellsCollectionChanged(object sender, SelectionCollectionChangedEventArgs<SelectedCellsCollection> e)
        {
            /*
            var collection = SelectedItems;
            if (collection != null)
            {
                var addItems = e.NewSelectedItems.Select(cell => cell.Row)
                                                 .Distinct()
                                                 .Select(row => row.Data)
                                                 .ToList();
                var removeItems = e.PreviouslySelectedItems.Select(cell => cell.Row)
                                                           .Distinct()
                                                           .Select(row => row.Data)
                                                           .ToList();
                // Remove the duplicates, since it means no change in selected item
                var intersect = removeItems.Intersect(addItems).ToList();
                foreach (var item in intersect)
                {
                    removeItems.Remove(item);
                    addItems.Remove(item);
                }
                collection.RemoveRange(removeItems);
                collection.AddRange(addItems);
            }
            SelectedItem = AssociatedObject.ActiveItem ?? collection.FirstOrDefault();
            */
        }

        private void OnSelectedRowsCollectionChanged(object sender, SelectionCollectionChangedEventArgs<SelectedRowsCollection> e)
        {
            var collection = SelectedItems;
            if (collection != null)
            {
                if (e.PreviouslySelectedItems.Count > 0)
                {
                    var data = e.PreviouslySelectedItems.Select(row => row.Data).ToList();
                    collection.RemoveRange(data);
                }
                if (e.NewSelectedItems.Count > 0)
                {
                    var data = e.NewSelectedItems.Select(row => row.Data).ToList();
                    collection.AddRange(data);
                }
            }
            SelectedItem = AssociatedObject.ActiveItem ?? collection.FirstOrDefault();
        }

        private void UpdateSelectedItems()
        {
            var collection = SelectedItems;
            if (collection != null)
            {
                collection.Clear();
                var selectedItems = new List<object>();
                UpdateSelectedItems(selectedItems, AssociatedObject.Rows);
                if (selectedItems.Count > 0)
                    collection.AddRange(selectedItems);
            }
        }

        private void UpdateSelectedItems(List<object> selectedItems, RowCollection rows)
        {
            foreach (var row in AssociatedObject.Rows)
            {
                if (row.IsSelected)
                    selectedItems.Add(row.Data);
                if (row.HasChildren)
                    UpdateSelectedItems(selectedItems, row.ChildBands);
            }
        }

        private void UpdateSelectedItems(List<object> selectedItems, ChildBandCollection childBands)
        {
            foreach (var childBand in childBands)
            {
                UpdateSelectedItems(selectedItems, childBand.Rows);
            }
        }
    }
}
