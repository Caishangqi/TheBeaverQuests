namespace Core.UI.InteractButtonWidget.Event
{
    public struct InteractButtonClickEvent
    {
        public InteractButtonWidgetView view;

        public InteractButtonClickEvent(InteractButtonWidgetView view)
        {
            this.view = view;
        }
    }
}