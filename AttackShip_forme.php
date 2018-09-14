<?php
	$inData = getRequestInfo();
	$lobby_id = $inData["lobby_id"];
	$parent_id = $inData["parent_id"]; // your ship. cant attack your ship
	$att_location = $inData["att_location"];

	$target_parent_id=0;
	$target_point=0;

	$blocks=0;
	$ship_id=0;
	$lock = 1;
	$unlock=0;
	$x_out=0;
	$lock_status=0;
	$point=0;
	$conn = new mysqli("localhost", "ucfgroup3", "abc123", "UCFProject2");
	if ($conn->connect_error)
	{
		returnWithError( $conn->connect_error );
	}
	else
	{
		/* START: CHECK BUSY*/
		$sql = "SELECT isLock FROM Locktable where id =1 ";
		$result = $conn->query($sql);
		if ($result->num_rows > 0)
		{
			//fetch the first element from result
			while($row = $result->fetch_assoc())
			{
				$lock_status = $row["isLock"]; // get the lock status
					//hit the ship....
					// $conn->query($sql);

			}
		}
		/*END: BUSY*/

if($lock_status == 0) // if not locked.
{

	//Locked it first before do any edit
	$sql = "UPDATE Locktable SET isLock = 1  WHERE id = 1";
	$conn->query($sql);

	////////////////////////////////////////////////////////////
////////////////////Start Add player score////////////////////
////////////////////////////////////////////////////////////
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


		$sql = "SELECT blocks, ship_id, parent_id FROM Ships where lobby_id = '" . $inData["lobby_id"] . "'
		AND (Block1 = '" . $inData["att_location"] . "' OR Block2 = '" . $inData["att_location"] . "' OR Block3 = '" . $inData["att_location"] . "' OR Block4 = '" . $inData["att_location"] . "' )
		AND NOT parent_id = '" . $inData["parent_id"] . "'";
		$result = $conn->query($sql);
		if ($result->num_rows > 0)
		{

			//Addpoint  for player//
		  $point = $point + $result->num_rows;
			$sql = "UPDATE Leaderboard SET scores = $point WHERE
			parent_id = '" . $inData["parent_id"] . "'";
			$conn->query($sql);
			////////////////////////////////////////////////////////////
			////////////////////END Add player score////////////////////
			///////////////////////////////////////////////////////////


			while($row = $result->fetch_assoc())
			{
				$blocks = $row["blocks"]; // get block so we can subtract
				$ship_id = $row["ship_id"]; // get ship_id to know which ship to attack
				$target_parent_id = $row["parent_id"]; // get the target parent_id


				  ////////////////////////////////////////////////////////////
				////////////////////Start take-off target_player score////////////////////
				////////////////////////////////////////////////////////////
				  // get the target_player score
					$sql = "SELECT scores FROM Leaderboard where parent_id = $target_parent_id";
					$result1 = $conn->query($sql);
					if ($result1->num_rows > 0)
					{
						//fetch the first element from result
						while($row1 = $result1->fetch_assoc())
						{
							$target_point = $row1["scores"];

						}
					}

				  //Take point off for $target_parent_id
				  $target_point = $target_point - 1;
				  $sql = "UPDATE Leaderboard SET scores = $target_point WHERE
				  parent_id = $target_parent_id";
				  $conn->query($sql);

				  ////////////////////////////////////////////////////////////
				////////////////////END take-off target_player score////////////////////
				////////////////////////////////////////////////////////////


					//hit the ship....
					$blocks = $blocks - 1; // take out a block
						$sql = "UPDATE Ships SET BLock1 = $x_out , blocks = $blocks WHERE
						ship_id = $ship_id
						AND Block1 = '" . $inData["att_location"] . "'
						AND NOT parent_id = '" . $inData["parent_id"] . "'";
					 $conn->query($sql);

					 $sql = "UPDATE Ships SET BLock2 = $x_out , blocks = $blocks WHERE
					 ship_id = $ship_id
					 AND Block2 = '" . $inData["att_location"] . "'
					 AND NOT parent_id = '" . $inData["parent_id"] . "'";
					 $conn->query($sql);

					 $sql = "UPDATE Ships SET BLock3 = $x_out , blocks = $blocks WHERE
					 ship_id = $ship_id
					 AND Block3 = '" . $inData["att_location"] . "'
					 AND NOT parent_id = '" . $inData["parent_id"] . "'";
					 $conn->query($sql);

					 $sql = "UPDATE Ships SET BLock4 = $x_out , blocks = $blocks WHERE
					 ship_id = $ship_id
					 AND Block4 = '" . $inData["att_location"] . "'
					 AND NOT parent_id = '" . $inData["parent_id"] . "'";
					$conn->query($sql);
			}//			while($row = $result->fetch_assoc())

			///////////////////////////////////
			////CHECK IF ONLY 1 PLAYER LEFT////
			//////////////////////////////////

			// get parent_id of player that has blocks > 0
			$sql = "SELECT parent_id FROM Ships where lobby_id = '" . $inData["lobby_id"] . "'
			 AND NOT blocks = 0";
			 $result1 = $conn->query($sql);
			 if ($result1->num_rows > 0)
			 {
				 while($row1 = $result1->fetch_assoc())
				 {
					 $winner_parenet_id = $row1["parent_id"];
				 }
			 }
			 // User the parent_id that has blocks > 0 to check if there any other player also has blocks > 0
			 $sql = "SELECT blocks FROM Ships where lobby_id = '" . $inData["lobby_id"] . "'
				AND NOT parent_id = $winner_parenet_id AND NOT blocks = 0";

				$result1 = $conn->query($sql);
				if ($result1->num_rows == 0)
				{
					$winner = 1;
				}else {
				$winner = 0;
			}

			$sql = "SELECT Login FROM Users where parent_id = $winner_parenet_id";
			 $result1 = $conn->query($sql);
			 if ($result1->num_rows > 0)
			 {
				 while($row1 = $result1->fetch_assoc())
				 {
					 $winner_Login = $row1["Login"];

				 }
			 }


 		 ///////////////////////////////////
	 	///CHECK IF ONLY 1 PLAYER LEFT////
		/////////////////////////////////

			//Unlock
			$sql = "UPDATE Locktable SET isLock = 0  WHERE id = 1";
			$conn->query($sql);


		$conn->close();
		if($winner == 1){
			returnWithWiner($winner_Login ); // return what u need to
		}
		else{
	  	returnWithBusy( "hit" ); // return what u need to
		}
	}else{//		if ($result->num_rows > 0)
		//Unlock
		$sql = "UPDATE Locktable SET isLock = 0  WHERE id = 1";
		$conn->query($sql);
		$conn->close();
		returnWithBusy( "Miss" ); // return what u need to
	}

	}else{//if($lock_status == 0)
			$conn->close();
			returnWithBusy( "Locked_Please_try_again" ); // locked..keep trying
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
	function returnWithBusy( $err )
	{
		$retValue = '{"status":"' . $err . '"}';
		sendResultInfoAsJson( $retValue );
	}
	function returnWithWiner( $err )
	{
		$retValue = '{"Winner":"' . $err . '"}';
		sendResultInfoAsJson( $retValue );
	}
	function returnWithInfo( $searchResults )
	{
		$retValue = '{"Lat&lon":[' . $searchResults . ']}';
		sendResultInfoAsJson( $retValue );
	}
?>
