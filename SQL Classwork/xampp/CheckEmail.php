<?php
// disconnect the email check from unity file

// takes email parsed into function in unity
// checks email against database if it exists
// returns debug information

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "sqlsystem";
	
	
	
	$email_to_check = $_POST["email_Post"]; // waiting for email to be sent
	
	
	// make connection
	
		// make connection
	$conn = new mysqli($servername, $server_username, $server_password, $dbname);
	// check connection and kill if fail
	if(!$conn)
	{
		die("Connection Failed.".mysql_connect_error());
	}
	
	$checkemail = "SELECT email FROM users"; // selects the email collumn from the users table
	$checkemailresult = mysqli_query($conn, $checkemail);
	$debug = "";
	if(mysqli_num_rows($checkemailresult) > 0)
	{
		while ($row = mysqli_fetch_assoc($checkemailresult))
		{
			if ($row['email'] == $email_to_check) // if email to check already exists in databease
			{
				$debug =  "Email exists";
			}
			else
			{
				$debug = "Check Email";
			}
		}
	}
	
	
	
	







?>