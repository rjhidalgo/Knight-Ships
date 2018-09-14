<?php

	$inData = getRequestInfo();

	$searchResults = "";
	$searchCount = 0;

	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{
		if($inData["ready"] == "0" || $inData["ready"] == "1" )
		{
			$sql = "SELECT login, ready FROM Lobby where lobby_id = '" . $inData["lobby_id"] . "' and ready = '" . $inData["ready"] . "'";
		}
		else{
			$sql = "SELECT login, ready FROM Lobby where lobby_id = '" . $inData["lobby_id"] . "'";
		}

								$result = $conn->query($sql);
								if ($result->num_rows > 0)
								{
									$searchResults .= $result->num_rows*2+1;
									$searchResults .= ",";
									//fetch the first element from result
									while($row = $result->fetch_assoc())
									{
										if( $searchCount > 0 )
										{
											$searchResults .= ",";
										}
										$searchCount++;
										$searchResults .= '"' . $row["login"] . ' "," ' . $row["ready"] . '"';
									}
									$sql = "SELECT start FROM Lobbies where lobby_id = '" . $inData["lobby_id"] . "'";

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
											$searchResults .= '"' . $row["start"] . '"';
										}
											$conn->close();
											returnWithInfo( $searchResults );
									}
									else
									{
										$conn->close();
										returnWithError( "Nada" );
									}
								}
								else
								{
									$conn->close();
									returnWithError( "Nada" );
								}




		}



		function getRequestInfo()
		{
	    //takes JSON encoded string and converts to  a PHP var
			return json_decode(file_get_contents('php://input'), true);
		}

		function sendResultInfoAsJson( $obj )
		{
	    //Send a raw HTTP header
			header('Content-type: application/json');
	    // outputs $obj
			echo $obj;
		}

		function returnWithError( $err )
		{
			$retValue = '{"results":"' . $err . '"}';
			sendResultInfoAsJson( $retValue );
		}

		function returnWithInfo( $searchResults )
		{
			$retValue = '{"results":[' . $searchResults . ']}';
			sendResultInfoAsJson( $retValue );
		}

	?>
