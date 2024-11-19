

namespace FICR.Cooperation.Humanism.CrossCutting.Wrapper
{
    public class ManagerException(
      string message,
      int statusCode = 400,
      IEnumerable<ValidationError> errors = null,
      string errorCode = "",
      string refLink = "")
      : Exception(message)
    {
        public int StatusCode { get; set; } = statusCode;

        public IEnumerable<ValidationError> Errors { get; set; } = errors;

        public string ReferenceErrorCode { get; set; } = errorCode;
        public string ReferenceDocumentLink { get; set; } = refLink;
    }
}
