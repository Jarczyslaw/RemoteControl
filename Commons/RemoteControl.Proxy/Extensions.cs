namespace RemoteControl.Proxy
{
    public static class Extensions
    {
        public static bool HasError(this CommonResponse commonResponse)
        {
            return !string.IsNullOrEmpty(commonResponse.Error);
        }
    }
}