using SourceFolderCleanup.Abstract;
using SourceFolderCleanup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SourceFolderCleanup.Forms
{
    internal class FolderInfoSorter : DataGridViewSorter<FolderInfo>
    {
        public FolderInfoSorter(DataGridView dataGridView, params ColumnInfo[] defaultSort) : base(dataGridView, defaultSort)
        {
        }

        protected override IEnumerable<FolderInfo> GetSortedData(DataGridViewColumn column, bool ascending, IEnumerable<FolderInfo> data)
        {
            if (column.Name.Equals("colMaxDate"))
            {
                return (ascending) ?
                    data.OrderBy(item => item.MaxDate) :
                    data.OrderByDescending(item => item.MaxDate);
            }

            if (column.Name.Equals("colSize"))
            {
                return (ascending) ?
                    data.OrderBy(item => item.TotalSize) :
                    data.OrderByDescending(item => item.TotalSize);
            }

            throw new NotImplementedException($"Column {column.Name} has no sort instructions");
        }
    }
}
