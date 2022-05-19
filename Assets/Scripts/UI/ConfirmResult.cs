namespace UI {
    public struct ConfirmResult {
        public static readonly ConfirmResult DoNothing = new(ConfirmResultType.DoNothing);
        public static readonly ConfirmResult Return = new(ConfirmResultType.Return);

        public static ConfirmResult ChangeFocus(Focusable focus) => new(ConfirmResultType.ChangeFocus, focus);
        public ConfirmResult(ConfirmResultType type, Focusable newFocusTarget = null) {
            this.type = type;
            this.newFocusTarget = newFocusTarget;
        }
        public readonly ConfirmResultType type;
        public readonly Focusable newFocusTarget;
    }
}
