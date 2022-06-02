namespace UI {
    public interface MenuChoice {
        public UIMenuItem currentSelectedMenuItem => menuItems[index];
        public UIMenuItem[] menuItems { get; }
        public int index { get; set; }
        public void Reset();

        public void Next() {
            index += 1;
        }

        public void Previous() {
            index -= 1;
        }
        public void StopAnimation();
        public void ResumeAnimation();
    }
}
