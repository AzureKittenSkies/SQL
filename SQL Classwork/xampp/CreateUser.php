<?php // tells code that we're using PHP

	// Variable declaration requires $ before the variable names
	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbname = "sqlsystem";
	
	$username = $_POST["username_Post"]; // waiting for username to be sent
	$email = $_POST["email_Post"];
	$password = $_POST["password_Post"];
	
	// make connection
	$conn = new mysqli($servername, $server_username, $server_password, $dbname);
	// check connection and kill if fail
	if(!$conn)
	{
		die("Connection Failed.".mysql_connect_error());
	}
	
	$checkuser = "SELECT username FROM users"; // Select the username collumn from the users table
	$checkuserresult = mysqli_query($conn, $checkuser);
	$debug ="";
	if(mysqli_num_rows($checkuserresult) > 0) // if results in table exist to prevent duplication
	{
		while ($row = mysqli_fetch_assoc($checkuserresult))
		{
			if ($row['username'] == $username) // if input username already exists
			{
				print "User Already Exists \n";
			}
			else
			{
				$debug = "Check Email";
				//print "Check Email";
			}
		}
	}
	else if (mysqli_num_rows($checkuserresult)<=0) // otherwise input new data
	{
		$createuser = "INSERT INTO users(username,email, password) VALUES('".$username."', '".$email."', '".$password."')";
		$createuserresult = mysqli_query($conn, $createuser);
		
		if(!$createuserresult)
		{
			print "Error";
		}
		else
		{
			print "Create First User";
		}
	}
	if($debug == "Check Email")
	{
		$checkemail = "SELECT email FROM users";
		$checkemailresult = mysqli_query($conn, $checkemail);
		
		if (mysqli_num_rows($checkemailresult)>0)
		{
			while($row = mysqli_fetch_assoc($checkemailresult))
			{
				if($row['email'] == $email)
				{
					print "Email Already Exists \n";
				}
				else
				{
					$createuser = "INSERT INTO users(username,email, password) VALUES('".$username."', '".$email."', '".$password."')";
					$createuserresult = mysqli_query($conn, $createuser);
					
					if(!$createuserresult)
					{
						//print "Error";
					}
					else
					{
						print "Create User";
					}
				}
			}
			
		}
		
		
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
?>