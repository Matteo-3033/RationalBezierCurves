using System;

public static class Settings
{
    private static bool _showControlPolygon = true;
    
    public static event EventHandler<OnShowControlPolygonArgs> OnShowControlPolygonChanged;

    public class OnShowControlPolygonArgs : EventArgs
    {
        public bool Show => Settings.ShowControlPolygon;
    }
    
    public static bool ShowControlPolygon
    {
        get => _showControlPolygon;
        set => SetShowControlPolygon(value);
    }

    private static void SetShowControlPolygon(bool value)
    {
        _showControlPolygon = value;
        OnShowControlPolygonChanged?.Invoke(null, new OnShowControlPolygonArgs());
    }
}