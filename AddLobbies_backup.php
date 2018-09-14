<?php
	$inData = getRequestInfo();

$gameName = $inData["gameName"];
	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
  //  $conn = new mysqli("107.180.40.120", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{
		//$sql = "insert into Ships (Block1)
	//	VALUES ('". $Block1 . "')";
$sql = "insert into Lobbies (gameName)	VALUES ('" . $gameName . "')";
		if( $result = $conn->query($sql) != TRUE )
		{
			returnWithError( $conn->error );
		}
		$conn->close();
	}

	returnWithError("Joined!");

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
