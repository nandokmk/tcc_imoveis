var polyX = new Array(1,4,1,4);
	var polyY = Array(4,4,1,1);
	

	function pointInPolygon(x, y)
	{
		var polySides = polyY.length;
		var	i, j=polySides-1 ;
		var  oddNodes = false;

		for (i=0; i<polySides; i++) 
		{
			if (polyY[i] < y && polyY[j] >= y ||  polyY[j] < y && polyY[i] >= y) 
			{
				if (polyX[i]+(y-polyY[i])/(polyY[j]-polyY[i])*(polyX[j]-polyX[i])<x) 
				{
					oddNodes=!oddNodes; 
				}
			}
			j=i; 
		}

		return oddNodes; 
	}
