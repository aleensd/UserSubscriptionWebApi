namespace UserSubscriptionWebApi.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException(string msg) : base(msg)
        {
        }
    }
}
