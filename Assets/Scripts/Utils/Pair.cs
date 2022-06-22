namespace Utils {
    public struct Pair<T1, T2> {
        public Pair(T1 first, T2 second) {
            this.first = first;
            this.second = second;
        }
        public readonly T1 first;
        public readonly T2 second;
    }
}
