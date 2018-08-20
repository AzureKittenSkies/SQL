<?php // tells code that we're using PHP

	// Variable declaration requires $ before the variable names
	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "sqlsystem";
	
	$username = $_POST["username_Post"]; // waiting for username to be sent
	$password = $_POST["password_Post"];

	// make connection
	$conn = new mysqli($servername, $server_username, $server_password, $dbname);
	// check connection and kill if fail
	if(!$conn)
	{
		die("Connection Failed.".mysql_connect_error());
	}
	
	$checkaccount = "SELECT password FROM users WHERE username = '" .$username."'";
	$result = mysqli_query($conn,$checkaccount);
	
	// if we have usernames that match the username
	if(mysqli_num_rows($result)>0)
	{
		// check if passwords match
		while($row = mysqli_fetch_assoc($result))
		{
			if($row['password'] == $password)
			{
				print "Login Success";
			}
			else
			{
				print "Password Incorrect";
			}
		}
	}
	else
	{
		print "User not found";
	}
	
	
	
	
?>
	