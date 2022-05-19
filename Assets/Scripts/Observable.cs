public struct Observable<T> {
    public Observable(T value) {
        this.value = value;
        onChange = null;
    }
    private T value;
    public delegate void ValueChange(T oldValue, T newValue);

    public void set(T newValue) {
        onChange?.Invoke(value, newValue);
        value = newValue;
    }

    public T get() {
        return value;
    }

    public static implicit operator Observable<T>(T value) => new(value);

    public event ValueChange onChange;

}