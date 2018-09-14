<?php
	$inData = getRequestInfo();
	$lobby_id = $inData["lobby_id"];
	$parent_id = $inData["parent_id"];
	$ready = $inData["ready"];
	$login = $inData["login"];
	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");


  //  $conn = new mysqli("107.180.40.120", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{

$sql = "SELECT player_limit FROM Lobbies where lobby_id = '" . $inData["lobby_id"] . "'";
$result = $conn->query($sql);
if ($result->num_rows > 0)
{
	//fetch the first element from result
	while($row = $result->fetch_assoc())
	{
		$player_limit =  $row["player_limit"] ;
	}
	//if (){
$sql = "SELECT lobby_id FROM Lobby where lobby_id = '" . $inData["lobby_id"] . "'";
	$result = $conn->query($sql);
		if ($result->num_rows>=0 )
		{
			$player_total = $result->num_rows;
		   if($player_total >= $player_limit){
				 returnWithError("Lobby_Full");
				}else{

					$sql = "insert into Lobby (lobby_id,parent_id,ready,login)	VALUES (". $lobby_id . ",". $parent_id . "," . $ready . ",'" . $login . "')";
						if( $result = $conn->query($sql) != TRUE )
						{
						$conn->close();
						returnWithError("Already Joined");
						}
						else{

											$sql = "SELECT host_lat, host_lon FROM Lobbies where lobby_id = '" . $inData["lobby_id"] . "'";
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
																							$searchResults .= '" ' . $row["host_lat"] .' "," '  . $row["host_lon"] . ' "';
																						}
																							$sql = "SELECT time FROM Lobbies where lobby_id = '" . $inData["lobby_id"] . "'";

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
																									$searchResults .= '"' . $row["time"] . '"';
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
																}


																				}
																				else
																				{
																					$conn->close();
																					returnWithError( "Cant_get_player_total" );
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
		$retValue = '{"Lat&lon":[' . $searchResults . ']}';
		sendResultInfoAsJson( $retValue );
	}
?>
