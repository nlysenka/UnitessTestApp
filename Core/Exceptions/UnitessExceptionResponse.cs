namespace UnitessTestApp.Api.Core.Exceptions
{
    public class UnitessExceptionResponse
    {
        public string Message { get; set; }

        public UnitessExceptionResponse(Exception ex)
        {
            Message = ex.Message;
        }
    }
}
