# dublinbikes

PROJECT GOALS:
- funnel Dublinbikes requests through API, to allow caching later
- 	also allows safe storage of API key
- CLIENT: view all stations
-  		incl bikes remaining, GPS loc, etc
- CLIENT: view single station/ set live update
- In-memory caching (Redis is overkill for this solution)
- 


	stations list
	filter available & spaces
	favourites? (would need login)
	broadcaster?
		generic recurring status update i guess
		unsure 100% how to create a "live http stream" connection
		
		
		
		
LIBRARIES / TOOLS
	redis cache
	signalR
	simple DB
		preferably with interface
			allow textfile, sql, inmemory?
			output html, json, variants
	simple homepage selector
	simple observable eg
	simple user login
	email service
	logging service
	
