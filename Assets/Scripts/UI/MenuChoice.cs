namespace UI {
    public interface MenuChoice {
        public UIMenuItem currentSelection => items[index];
        public UIMenuItem[] items { get; }
        public int index { get; set; }

        public void Next() {
            index = index + 1;
        }

        public void Previous() {
            index = index - 1;
        }
        public void StopAnimation();
        public void ResumeAnimation();
    }
}
