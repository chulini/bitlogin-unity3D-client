namespace JSCSharpConnection
{
    public struct MessageToServer
    {
        public string message { get; private set; }

        public MessageToServer(string inMessage)
        {
            message = inMessage;
        }
    }
}