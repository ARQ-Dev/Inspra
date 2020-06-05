namespace ARQ.NeuroSym.UIKit {
    public interface IPopupStateSwitchable
    {
        void SwitchState(PopupState state);
    }

    public enum PopupState
    {
        Highlighted,
        NotHighlighted,
        Warning
    }
}