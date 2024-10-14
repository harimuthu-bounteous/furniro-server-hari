namespace furniro_server_hari.DTO.ResponseDTO
{
    public class ErrorResponseDTO
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public ErrorResponseDTO(string errorMessage, int statusCode)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }

}