﻿public struct Observable<T> {
    public Observable(T value) {
        this.value = value;
        onChange = null;
    }
    private T value;
    public delegate void ValueChange(T oldValue, T newValue);

    public void Set(T newValue) {
        onChange?.Invoke(value, newValue);
        value = newValue;
    }

    public T Get() {
        return value;
    }

    public static implicit operator Observable<T>(T value) => new(value);
    public static implicit operator T(Observable<T> value) => value.value;

    public event ValueChange onChange;

}