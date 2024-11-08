namespace Core.UI.PopUpWidget.Event
{
    public struct PopUpWidgetClickEvent
    {
        public PopUpWidgetView view { get; set; }

        public PopUpWidgetClickEvent(PopUpWidgetView view)
        {
            this.view = view;
        }
    }
}