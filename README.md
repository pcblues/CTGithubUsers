# CTGithubUsers

A project to return Github user information via the Github API.

Endpoint retrieveUsers takes 0 or more names in the query URI.

Usage example: http://localhost:5268/retrieveUsers?username=Mark%20Osborne&username=Jon%20Skeet

Note that tests started failing with "Rate Limit Exceeded" while debugging them 
as a result of the limit of the ability to hit the Github API server with no authorisation, 
and my code has been tested successfully manually with the unit test cases.

