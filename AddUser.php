
	<?php
		$inData = getRequestInfo();

		$Login = $inData["Login"];
		$Password= $inData["Password"];
	 	$scores = 0;
		$newPlayer_id= "";

		//$conn = new mysqli("localhost", "leinecke_SaRcc", "Wash9Lives!", "leinecke_COP4331");
		$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");

		if ($conn->connect_error)
		{
			returnWithError( $conn->connect_error );
		}
		else
		{
		//	$sql = "insert into Contacts (City)
			//VALUES ('". $City . "')";
			$sql = "insert into Users (Login,Password)	VALUES ('". $Login . "','" . $Password . "')";
			if( $result = $conn->query($sql) != TRUE )
			{
				$conn->close();
				returnWithError("That username is already taken");
			}
			else{
				$conn->query($sql);
		}


		$sql = "SELECT parent_id FROM Users where Login = '" . $inData["Login"] . "'";
		$result = $conn->query($sql);
		if ($result->num_rows > 0)
		{
			//fetch the first element from result
			while($row = $result->fetch_assoc())
			{
				$newPlayer_id = $row["parent_id"];

			}
		}

		$sql = "insert into Leaderboard (parent_id,scores,Userlogin)	VALUES ('". $newPlayer_id . "','" . $scores . "','" . $Login . "')";
		if( $result = $conn->query($sql) != TRUE )
		{
			$conn->close();
			returnWithError("That username is already taken");
		}
		else{


			$conn->close();
			returnWithError("Account Created");

		}

		}



		function getRequestInfo()
		{
			return json_decode(file_get_contents('php://input'), true);
		}

		function sendResultInfoAsJson( $obj )
		{
			header('Content-type: application/json');
			echo $obj;
		}

		function returnWithError( $err )
		{
			$retValue = '{"error":"' . $err . '"}';
			sendResultInfoAsJson( $retValue );
		}

	?>
