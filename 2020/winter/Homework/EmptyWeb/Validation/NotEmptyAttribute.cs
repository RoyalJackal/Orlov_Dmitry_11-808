namespace EmptyWeb.Validation
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null && value.ToString() != "")
                return true;

            return false;
        }
    }
}