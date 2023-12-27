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
    
    
    private static bool _showProjectionMesh = true;
    
    public static event EventHandler<OnShowProjectionMeshArgs> OnShowProjectionMeshChanged;

    public class OnShowProjectionMeshArgs : EventArgs
    {
        public bool Show => Settings.ShowProjectionMesh;
    }
    
    public static bool ShowProjectionMesh
    {
        get => _showProjectionMesh;
        set => SetShowProjectionMesh(value);
    }

    private static void SetShowProjectionMesh(bool value)
    {
        _showProjectionMesh = value;
        OnShowProjectionMeshChanged?.Invoke(null, new OnShowProjectionMeshArgs());
    }
    
    
    private static bool _showDeCasteljau = true;
    
    public static event EventHandler<OnShowDeCasteljauArgs> OnShowDeCasteljauChanged;

    public class OnShowDeCasteljauArgs : EventArgs
    {
        public bool Show => Settings.ShowDeCasteljau;
    }
    
    public static bool ShowDeCasteljau
    {
        get => _showDeCasteljau;
        set => SetShowDeCasteljau(value);
    }

    private static void SetShowDeCasteljau(bool value)
    {
        _showDeCasteljau = value;
        OnShowDeCasteljauChanged?.Invoke(null, new OnShowDeCasteljauArgs());
    }
}