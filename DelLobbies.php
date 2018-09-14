<?php
	$inData = getRequestInfo();
	$lobby_id = $inData["lobby_id"];
	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{

			$sql = "DELETE FROM Lobbies Where lobby_id = " .$lobby_id. " ";

		if( $result = $conn->query($sql) != TRUE )
		{
			$conn->close();
			returnWithError( $conn->error );
		}
		$conn->close();
		returnWithError("Deleted");
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
