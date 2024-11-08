namespace Core.UI.PopUpWidget.Event
{
    public struct PopUpWidgetGoNextPageEvent
    {
        public PopUpWidgetView view { get; set; }
        public int currentPage { get; set; }

        public PopUpWidgetGoNextPageEvent(PopUpWidgetView view, int currentPage)
        {
            this.view = view;
            this.currentPage = currentPage;
        }
    }
}