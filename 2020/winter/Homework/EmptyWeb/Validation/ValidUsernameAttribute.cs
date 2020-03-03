namespace EmptyWeb.Validation
{
    public class ValidUsernameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.ToString().ToLower() != "admin")
                return true;
            return false;
        }
    }
}