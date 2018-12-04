<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "sqlsystem";
	
	$email = $_POST["email_Post"];
	$password = $_POST["password_Post"];
	
	// make connection
	$conn = new mysqli($servername, $server_username, $server_password, $dbname);
	// check connection and kill if fail
	if(!$conn)
	{
		die("Connection Failed.".mysql_connect_error());
	}
	
	
	$updatePassword = "UPDATE users SET password = '".$password."', resetcode = '' WHERE email = '".$email."'";
	$result = mysqli_query($conn, $updatePassword);
	
	if(!$result)
	{
		print "Error";
	}
	else
	{
		print "Password Changed";
	}
	
	


?>