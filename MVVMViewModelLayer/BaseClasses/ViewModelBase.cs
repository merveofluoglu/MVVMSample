
using CommonLibrary.PagerClasses;
using MVVMEntityLayer;

namespace MVVMViewModelLayer.BaseClasses
{
    public class ViewModelBase
    {
        public string EventCommand { get; set; }
        public string SortDirection { get; set; } // acs or desc
        public string SortExpression { get; set; } // column name
        public string PreviousSortExpression { get; set; } // if SortDirection asc PreviousSortExpression desc v.v.
        public string EventArgument { get; set; } // for pagination
        public Pager Pager { get; set; }
        public PagerItemCollection Pages { get; set; }
        public bool IsDetailVisible { get; set; }

        public ViewModelBase() 
        {
            EventCommand = string.Empty;
            SortDirection = "asc";
            SortExpression = string.Empty;
            PreviousSortExpression = string.Empty;

            EventArgument = string.Empty;
            Pager = new Pager();
        }

        public virtual void HandleRequest() { }

        protected virtual void SetSortDirection() 
        {
            if(SortExpression == PreviousSortExpression)
            {
                SortDirection = (SortDirection == "asc" ? "desc" : "asc");
            }
            else
            {
                SortDirection = "asc";
            }
            PreviousSortExpression = SortExpression;
        }

        protected virtual void SetPagerObject(int totalRecords)
        {
            Pager.TotalRecords = totalRecords;
            Pager.SetPagerProperties(EventArgument);
            Pages = new PagerItemCollection(Pager);
        }
    }
}
