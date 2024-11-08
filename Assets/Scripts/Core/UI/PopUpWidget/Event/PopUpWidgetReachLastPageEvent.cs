namespace Core.UI.PopUpWidget.Event
{
    public struct PopUpWidgetReachLastPageEvent
    {
        public PopUpWidgetView view { get; set; }
        public int currentPage { get; set; }

        public PopUpWidgetReachLastPageEvent(PopUpWidgetView view, int currentPage)
        {
            this.view = view;
            this.currentPage = currentPage;
        }
    }
}