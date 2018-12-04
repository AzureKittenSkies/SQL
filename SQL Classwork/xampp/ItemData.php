<?php

	$servername = "localhost";
	$server_username = "root";
	$server_password = "";
	$dbName = "sqlsystem";
	
	// Make Connection
	$conn = new mysqli($servername, $server_username, $server_password, $dbName);
	// check Connection
	if(!$conn)
	{
		die("Connection Failed.".mysql_connect_error());
	}
	
	// get all the data from our weapon database
	$getItem = "SELECT id, name, clipSize, damage, fireRate, weaponRange, ammoType, iconName, meshName FROM weapon_DB";
	// connect and search
	$result = mysqli_query($conn, $getItem);
	// if we have something in that search result
	if(mysqli_num_rows($result)>0)
	{
		// while we have rows, print info
		while($row = mysqli_fetch_assoc($result))
		{
			// we are going to pull a string of all the items
			// items are split by #
			// item data is split by |
			print $row['id']."|"
				.$row['name']."|"
				.$row['clipSize']."|"
				.$row['damage']."|"
				.$row['fireRate']."|"
				.$row['weaponRange']."|"
				.$row['ammoType']."|"
				.$row['iconName']."|"
				.$row['meshName']."#";
		}
	}

?>