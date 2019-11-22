namespace RemoteControl.Proxy
{
    public static class ConnectionRequestExtensions
    {
        public static bool Compare(this ConnectionRequest request1, ConnectionRequest request2)
        {
            return request1.Address == request2.Address
                && request1.Name == request2.Name
                && request1.Type == request2.Type;
        }
    }
}