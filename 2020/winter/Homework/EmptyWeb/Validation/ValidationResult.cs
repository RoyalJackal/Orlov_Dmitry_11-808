namespace EmptyWeb.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrMsg { get; }

        public ValidationResult(bool isValid, string errMsg = "")
        {
            IsValid = isValid;
            ErrMsg = errMsg;
        }
    }
}