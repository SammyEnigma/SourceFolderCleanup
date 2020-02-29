using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SourceFolderCleanup.Abstract
{
    public abstract class DataGridViewSorter<T>
    {
        private readonly Dictionary<int, ColumnInfo> _sortDirections;

        private IEnumerable<T> _data;
        
        public DataGridViewSorter(DataGridView dataGridView, params ColumnInfo[] defaultSorts)
        {
            var defaultSortDictionary = (defaultSorts ?? Enumerable.Empty<ColumnInfo>()).ToDictionary(item => item.Column.Index);

            DataGridView = dataGridView;
            DataGridView.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
            _data = (dataGridView.DataSource as BindingSource).DataSource as IEnumerable<T>;

            _sortDirections = new Dictionary<int, ColumnInfo>();
            foreach (var col in dataGridView.Columns.OfType<DataGridViewColumn>().Where(col => col.SortMode == DataGridViewColumnSortMode.Programmatic))
            {
                bool ascending = (defaultSortDictionary.ContainsKey(col.Index)) ? defaultSortDictionary[col.Index].IsAscending : true;
                _sortDirections.Add(col.Index, new ColumnInfo() { Column = col, IsAscending = ascending });
            }
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_sortDirections.ContainsKey(e.ColumnIndex))
            {
                _sortDirections[e.ColumnIndex].IsAscending = !_sortDirections[e.ColumnIndex].IsAscending;
                _data = GetSortedData(_sortDirections[e.ColumnIndex].Column, _sortDirections[e.ColumnIndex].IsAscending, _data);

                BindingList<T> boundList = new BindingList<T>();
                foreach (var item in _data) boundList.Add(item);
                BindingSource bs = new BindingSource();
                bs.DataSource = boundList;
                DataGridView.DataSource = bs;     
            }
        }

        public DataGridView DataGridView { get; }

        protected abstract IEnumerable<T> GetSortedData(DataGridViewColumn column, bool ascending, IEnumerable<T> data);

        public class ColumnInfo
        {
            public DataGridViewColumn Column { get; set; }
            public bool IsAscending { get; set; }
        }
    }
}
