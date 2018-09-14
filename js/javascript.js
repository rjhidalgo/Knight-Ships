var urlBase = 'http://cop4331project.com/';
var extension = "php";

var userId = 0;
var firstName = "";
var lastName = "";
var parent_id = "";

var tablelength=0;

setInterval(Update, 1000);
/* var buffer = [ 
	      { name : "name", score : -100 }, 
	      { name : "name", score : -100 }, 
	      { name : "name", score : -100 }, 
	      { name : "name", score : -100 }, 
	      { name : "name", score : -100 }, 
	      { name : "name", score : -100 },
	      { name : "name", score : -100 },
	      { name : "name", score : -100 },
	      { name : "name", score : -100 }
 		];
 
 var leaders = [ 
       {name : "Player1", score : 50},
       {name : "Player2", score : 90},
       {name : "Player3", score : 85},
       {name : "Player4", score : 32},
       {name : "Player5", score : 75},
       {name : "Player6", score : 70},
       {name : "Player7", score : 65},
       {name : "Player8", score : 60},
       {name : "Player9", score : 55},
       {name : "Player10", score : 100},
       {name : "Player11", score : 7} ];*/
       
   var leadername = []; 
   var leaderscore = []; 
   var buffername = []; 
   var bufferscore = [];
   var removed = [];
   var removed2 = []; 
	   
   
/////////////////////////////
//  MAIN LEADERBOARD FUNCTION
/////////////////////////////
function Update()
{
   search();
   //alert(tablelength);
   console.log ("near sort");
   if (tablelength > 0)
   {
     DeleteTable();
     InsertRows(tablelength, 3);
     //alert(tablelength);
     Sort();
     Display();
     buffername = []; 
     bufferscore = [];
   }   
}
	   
function Sort()
{
    //alert("test1");
    const LAST = 10;
    console.log ("length="+tablelength);
    for (i=0; i<tablelength; i++)
	{
	  //alert(typeof leadername[i]);
	  buffername[i] = leadername[i];
	  bufferscore[i] = Number(leaderscore[i]);
	  
	  //buffername[i] = leaders[i].name;
	  //bufferscore[i] = leaders[i].score;
	 
	}
	var found=false;
	var sorted=false;
	while (! sorted)
	{
		found = false;
		for (j=0; j<tablelength; j++)
		{
		   for (i=0; i<tablelength-1; i++)
		   {
		     
		     
		     if (bufferscore[i] < bufferscore[i+1])
		     {
			  	buffername[tablelength] = buffername[i];
			  	buffername[i] = buffername[i+1];
			  	buffername[i+1] = buffername[tablelength];
			  	bufferscore[tablelength] = bufferscore[i];
			  	bufferscore[i] = bufferscore[i+1];
			  	bufferscore[i+1] = bufferscore[tablelength];
			  	found = true;
		     }
		   }
		}
		if (! found) sorted=true;
	}
	//removed = buffername.splice(tablelength+1);
	//removed2 = bufferscore.splice(tablelength+1);
	//buffername = removed;
	//bufferscore = removed2;	
}

function Display()	   
{
	// POPULATE TABLE
	console.log ("Display Len=" + tablelength);
	for (r=0; r<tablelength; r++)
	{
			var x = document.getElementById( "lboard" ).rows[r+1].cells;
			x[0].innerHTML = (r+1);
			x[1].innerHTML = buffername[r];
			x[2].innerHTML = bufferscore[r];
	}  	
}



function DeleteTable()
{
	// DELETE ALL ROWS
	var myBoard = document.getElementById( "lboard" ); 

    while (myBoard.hasChildNodes()) 
	{ 
		myBoard.removeChild(myBoard.lastChild); 
    }
}		

function InsertRows(nRows, nCols)
{
	var rows = []; 
	var cells = [];
	var myBoard = document.getElementById( "lboard" ); 

	// Add ROWS
	for( var i=0; i<=nRows; i++ ) 
	{ 
		rows[i] = myBoard.insertRow(i); 
		cells[i] = []; 
		
		// Add COLS
		for( var x = 0; x < nCols ; x++ ) 
		{ // WEEK  DATE   AWAY   HOME
			cells[i][x] = document.createElement((i==0)?"th":"td"); 
			cells[i][x].innerHTML = (x==0)?"<input>":"<input>"; 
			rows[rows.length - 1].appendChild(cells[i][x]); 
		}
		
		// Add headers
		if (i == 0)
		{
			cells[i][0].innerHTML = "Rank";
			cells[i][1].innerHTML = "Name";
			cells[i][2].innerHTML = "Score";
		}
	}		
}

function search()
{
	//var srch = document.getElementById("searchText").value;
	document.getElementById("contactSearchResult").innerHTML = "";

	var contactList = document.getElementById("contactList");
	contactList.innerHTML = "";
	
	var jsonPayload = '{"parent_id" : "' + parent_id + '"}';
	var url = urlBase + '/LeaderboardStatus.' + extension;
	escape(jsonPayload);
	console.log ("In Search");
	
	var xhr = new XMLHttpRequest();
	xhr.open("GET", url, true);
	xhr.setRequestHeader("Content-type", "application/json; charset=UTF-8");
	console.log ("send jsonPayload");
	try
	{
		xhr.onreadystatechange = function()
		{
		  console.log ("Ready=" + this.readyState);
			if (this.readyState == 4 && this.status == 200)
			{
				console.log ("Callback");
				//alert(contactList);
				hideOrShow( "contactList", false );
				
				//alert("Contacts have been retrieved");
				
				var jsonObject = JSON.parse( xhr.responseText );
				var i;
				for( i=0; i<jsonObject.results.length; i++ )
				{
					var opt = document.createElement("option");
					opt.text = jsonObject.results[i];
					//alert(jsonObject.results[i]);
					opt.value = "";
					contactList.options.add(opt);
				}
				var j = 0;
				console.log ("jso len"+jsonObject.results.length);
				
			//	for( i=1; i<jsonObject.results.length; i += 3 )
			//	{
			//		leadername.push('name');
			//		j++;
			//		console.log ("search i=" + j);
			//	}
				tablelength = ([jsonObject.results.length-1]/3);
				j = 0;
				for( i=1; i<jsonObject.results.length; i += 3 )
				{
					leadername[j] = jsonObject.results[i];
					//alert(leadername[j]);
					j++;
				}
				j=0;
				///////////////
				for( i=0; i<=tablelength; i++ )
				{
					buffername.push('name');
				}
				j = 0;
				for( i=0; i<=tablelength; i++ )
				{
					bufferscore.push(0);
				}
				//////////////////////
				j=0;
				//for( i=2; i<jsonObject.results.length; i += 3 )
				//{
				//	leaderscore.push('score');
				//	j++;
				//}
				//j = 0;
				for( i=2; i<jsonObject.results.length; i += 3 )
				{
					leaderscore[j] = jsonObject.results[i];
					j++;
				}
				
			}
		};
		xhr.send(jsonPayload);
	}
	catch(err)
	{
	console.log ("Catch Err");
		document.getElementById("contactSearchResult").innerHTML = err.message;
	}
      
}

async function sleep(ms = 0) {
  return new Promise(r => setTimeout(r, ms));
}

async function run() {
  console.log("Before: " + (new Date()).toString());
  await sleep(1000);
  console.log("After:  " + (new Date()).toString());
}


function hideOrShow( elementId, showState )
{
	var vis = "visible";
	var dis = "block";
	if( !showState )
	{
		vis = "hidden";
		dis = "none";
	}

	document.getElementById( elementId ).style.visibility = vis;
	document.getElementById( elementId ).style.display = dis;
}

function escape(str) {
    return str.replace(/[\0\x08\x09\x1a\n\r"'\\\%]/g, function (char) {
        switch (char) {
            case "\0":
                return "\\0";
            case "\x08":
                return "\\b";
            case "\x09":
                return "\\t";
            case "\x1a":
                return "\\z";
            case "\n":
                return "\\n";
            case "\r":
                return "\\r";
            case "\"":
            case "'":
            case "\\":
            case "%":
                return "\\"+char; // prepends a backslash to backslash, percent,
                                  // and double/single quotes
        }
    });
}