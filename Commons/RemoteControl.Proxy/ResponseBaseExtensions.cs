namespace RemoteControl.Proxy
{
    public static class ResponseBaseExtensions
    {
        public static bool HasError(this ResponseBase @this)
        {
            return !string.IsNullOrEmpty(@this.Error);
        }
    }
}