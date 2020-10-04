# dublinbikes
## PROJECT DESCRIPTION:
An API, Webclient, and Xamarin-based Mobile app for querying JCDecaux servers which monitor station availability of the DublinBikes bicycle sharing scheme.

The primary drive behind this project was the usecase of being in transit towards a station, and wanting to be alerted as soon as possible if the station availability ran out, and furthermore, to immediately be able to determine the next most convenient station.

Furthermore, this project showcases full implementation of a project, from idea to rollout. It demonstrates numerous common development challenges, including 3rdparty service consumption, multiple client consumer platforms, security concerns, data serialization, data caching, liveupdate/background processes, user alerts, error handling, planning for scalability, planning for periods of minimal traffic, and UX considerations (though the UI design/aesthetic was not a priority)


## FEATURES:
### API :
- controls calls to external JCDecaux server
  - Isolated API key
  - Caches results in API to minimize load on 3rdparty server (30sec expiration)
  - Only queries server when a client app makes a request (no needless polling)
  - Expanded scope to include all cities served by JCDecaux
- CORS policy
- Security validation key in header for authentication
- environment-based config
- Custom "GetNearbyStations" 

 
### BROWSER/WEBCLIENT
- Search by single/all/nearby
- sort results by number, name, location, availability, recently updated
- Custom formatting/validation for "low availability" warning, and recently-updated
- LiveUpdate/ automatically recurring http calls
  - Disabled by default to reduce needless API calls/server load
  - Auto-disables after 30 mins to protect against abandoned open browser tabs (as above)
- loading spinner & last updated time
- Error handling popups
- Adjusted default URL routing (initial web issue on page refresh)

### MOBILE
- Cross-Platform design: Written in Xamarin, to simplify targeting multiple frameworks(Android/ iOS/ Windows UPF)
  - (currently only Android tested)
- Search by single/all/nearby
- sort results by number, name, location, availability, recently updated
- Conditional formatting/ validation for low station availability
- "Watch"/track a station for live update
- Issue notification when target station updated
- Priority notification for low availability
- Auto-timeout of station tracker after 30mins (reduce API load)
- Loading spinner 
- Error Handling popups


##PLANNED UPGRADES:
- Stopwatch timer & notification for alerting when user is approaching their free 30 minute ride limit 
- Locally save user preferences (bookmarked stations, adjustable "low bike" threshold, "NearbyStation" radius)
- Adjust webclient UI, including mobile layout
- Logging: results, usage, stats, performance, 
  - Email alerts to developer
- Devops pipeline
- dynamic polling time adjustment (request-based)
- LocationServices "find nearest station to me"
- misc static info (how it works, cost, availability times)
- (unconfirmed) auto-reject API calls after stations have closed for the day
- (Unconfirmed) Maps integration


## ENVIRONMENT SETUP/ CHANGE
- general config in appsettings.{myEnv}.json
- environments config in Properties/launchsettings.json
- switch environment setting locally via {project} -> properties -> debug -> ASPNETCORE_ENVIRONMENT
- Xamarin settings hardcoded in DBikesSettings.cs, and toggles in App.Xaml.cs "bool isProduction"
- The real ApiKeyHelper has not been committed; a dummy helper has been committed instead
- ** Whenever deploying to production, generate a new randomised base AuthTokenKey, and replace inside each project's appsettings
- in case of HTTP/comms issue, check the following areas: (a) web console for errors, (b) network tab for correctly formatted request, (c) web authtoken, (d) breakpoint on api SimpleAuthenticationAttribute, (that it is reached and that auth token matches), (e) CORS policy urls, (f) API GenericHTTPRequestHelper for results from calls to 3rdparty
