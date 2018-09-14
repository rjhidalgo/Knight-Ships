<?php
	$inData = getRequestInfo();
	$lobby_id = $inData["lobby_id"];
	$parent_id = $inData["parent_id"];
	$Block1 = $inData["Block1"];
	$Block2 = $inData["Block2"];
	$Block3 = $inData["Block3"];
	$Block4 = $inData["Block4"];
	$block = $inData["blocks"];


//	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
    $conn = new mysqli("107.180.40.120", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{
		//$sql = "insert into Ships (Block1)
	//	VALUES ('". $Block1 . "')";
$sql = "insert into Ships (lobby_id,parent_id,Block1,Block2,Block3,Block4,blocks)
VALUES (". $lobby_id . ",". $parent_id . ",'" . $Block1 . "','" . $Block2 . "','" . $Block3 . "','" . $Block4 . "',". $block . ")";
		if( $result = $conn->query($sql) != TRUE )
		{
			returnWithError( $conn->error );
		}else{
		//	$conn->query($sql);
	}

	// get the player score
	$sql = "SELECT scores FROM Leaderboard where parent_id = '" . $inData["parent_id"] . "'";
	$result = $conn->query($sql);
	if ($result->num_rows > 0)
	{
		//fetch the first element from result
		while($row = $result->fetch_assoc())
		{
			$point = $row["scores"];
		}
	}

	$point = $point + $block;
	$sql = "UPDATE Leaderboard SET scores = $point WHERE
	parent_id = $parent_id";
	$conn->query($sql);

	$conn->close();
	returnWithError("Ship_Added!");

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
		$retValue = '{"Status":"' . $err . '"}';
		sendResultInfoAsJson( $retValue );
	}

?>
