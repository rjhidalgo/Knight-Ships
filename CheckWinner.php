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

			$sql = "SELECT winner FROM Ships where lobby_id = '" . $inData["lobby_id"] . "'";

								$result = $conn->query($sql);
								if ($result->num_rows > 0)
								{
									while($row = $result->fetch_assoc())
									{
												//	$searchResults .= '"' . $row["login"] . ' "," ' . $row["ready"] . '"';
											//	$winner_Login =	 $row["winner"] ;
												$winner_Login .= '"' . $row["winner"] . ' ",","';
											//	$winner_Login .=  ",";

										break;
									}
									// $winner_Login .=  ",";
									//
									$sql = "SELECT blocks FROM Ships where lobby_id = '" . $inData["lobby_id"] . "' AND parent_id = '" . $inData["parent_id"] . "'
													AND NOT blocks = 0";

														$result = $conn->query($sql);
														if ($result->num_rows > 0)
														{
															while($row = $result->fetch_assoc())
															{
																$winner_Login .= '"' . $row["winner"] . ' ",","';
																break;
															}
															$conn->close();
															returnWithInfo( $winner_Login);
														}
														else
														{
															$winner_Login .='"' . $row["winner"] . ' ",","';

															$conn->close();
															returnWithInfo( $winner_Login);
														}


									$conn->close();
									returnWithInfo( $winner_Login);
								}
								else
								{
									$conn->close();
									returnWithError( "No_winner!" );
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
		function returnWithWiner( $err )
		{
			$retValue = '{"Winner":"' . $err . '"}';
			sendResultInfoAsJson( $retValue );
		}
		function returnWithInfo( $searchResults )
		{
			$retValue = '{"Players&NumberBlocks":[' . $searchResults . ']}';
			sendResultInfoAsJson( $retValue );
		}

	?>
