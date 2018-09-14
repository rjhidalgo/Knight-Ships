<?php
	$inData = getRequestInfo();
				$gameName = $inData["gameName"];
        $time = $inData["time"];
        $player_limit = $inData["player_limit"];
        $host_lat = $inData["host_lat"];
				$host_lon = $inData["host_lon"];

	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
  //  $conn = new mysqli("107.180.40.120", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{

		$sql = "insert into Lobbies (time, player_limit,host_lat,host_lon,gameName) VALUES ('" . $time . "','" . $player_limit . "','". $host_lat ."','". $host_lon ."','". $gameName ."')";
		if( $result = $conn->query($sql) != TRUE )
		{
		$conn->close();
		returnWithError("Already Created");
		}
		else{

			$sql = "SELECT lobby_id FROM Lobbies where gameName = '" . $inData["gameName"] . "'";

											$result = $conn->query($sql);
											if ($result->num_rows > 0)
											{
												//fetch the first element from result
												while($row = $result->fetch_assoc())
												{
													if( $searchCount > 0 )
													{
														$searchResults .= ",";
													}
													$searchCount++;
													$searchResults .= '" ' . $row["lobby_id"] .' "';
												}
													$conn->close();
													returnWithInfo( $searchResults );
											}
											else
											{
												$conn->close();
												returnWithError( "gameName used! Try again" );
											}



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
		function returnWithInfo( $searchResults )
		{
			$retValue = '{"Host_Lobby_id":[' . $searchResults . ']}';
			sendResultInfoAsJson( $retValue );
		}
	?>
