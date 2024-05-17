namespace FinanceTracker.Utility
{
    public class Envelope<T>
    {
        public T Data { get; }

        public object Error { get; }        

        public string Message { get; }

        protected internal Envelope(T result, object errorMessage, string message)
        {
            Data = result;
            Error = errorMessage;
            Message = message;            
        }        
    }
}
