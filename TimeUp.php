<?php

	$inData = getRequestInfo();
	$lobby_id = $inData["lobby_id"];

	$searchResults = "";
	$searchCount = 0;

	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{

			$sql = "SELECT blocks, parent_id FROM Ships where lobby_id = $lobby_id AND NOT blocks =0";

								$result = $conn->query($sql);
								if ($result->num_rows > 0)
								{
									while($row = $result->fetch_assoc())
									{
										if( $searchCount > 0 )
										{
											$searchResults .= ",";
										}
										$searchCount++;
										$searchResults .= '" ' . $row["blocks"] . '"';
										$temp_parent_id = $row["parent_id"];
										$searchResults .= ",";
										$sql2 = "SELECT Login FROM Users where parent_id = $temp_parent_id";
										$result2 = $conn->query($sql2);
										if ($result2->num_rows > 0)
										{
											while($row2 = $result2->fetch_assoc())
											{
												$searchResults .='"' . $row2["Login"] . ' "';
											}
										}
									}
									$conn->close();
									returnWithInfo( $searchResults);
								}
								else
								{
									$conn->close();
									returnWithError( "No!" );
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
			$retValue = '{"Players&NumberBlocks":[' . $searchResults . ']}';
			sendResultInfoAsJson( $retValue );
		}

	?>
