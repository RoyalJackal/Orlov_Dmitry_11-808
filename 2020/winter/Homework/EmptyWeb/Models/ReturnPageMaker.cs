namespace EmptyWeb.Models
{
    public static class ReturnPageMaker
    {
	    public static string Make(string message)
	    {
		    return $@"
<!DOCTYPE html>
<html>
	<head>
		<meta charset='utf-8' />
		<title></title>
	</head>
	<body>
		{message}<br />
		<form action='/'>
			<input type='submit' value='Return' />
		</form>
	</body>
</html>";
	    }
    }
}