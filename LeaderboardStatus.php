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

			$sql = "SELECT Userlogin,scores,lastDate FROM Leaderboard where BS = 1";

								$result = $conn->query($sql);
								if ($result->num_rows > 0)
								{
									$searchResults .= $result->num_rows * 3;
									$searchResults .= ",";
									//fetch the first element from result
									while($row = $result->fetch_assoc())
									{
										if( $searchCount > 0 )
										{
											$searchResults .= ",";
										}
										$searchCount++;
										$searchResults .= '"' . $row["Userlogin"] . ' "," ' . $row["scores"] . '"," ' . $row["lastDate"] . '"';
									}
									$conn->close();
									returnWithInfo( $searchResults);
								}
								else
								{
									$conn->close();
									returnWithError( "No_one_in_leaderboard" );
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
