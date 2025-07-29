### WEB
1. Start ```ng serve```

### Docker
- Angular web project uses localhost:4200
- ASP.NET api exposes 5103 or 7176
- Elasticsearch localhost:9200 - not using passwords, goal is learning not configuring the security

docker compose will start the api and elastic search project

### Elastic serach
- Load data to elastic search find the smaple-data.ndjson in API_ElasticSearch/ElasticsearchData folder
- Run a POST to ```http://localhost:9200/book/_bulk```
- body will be the sample data
- ndjson delimiters do not have an index using the POST URL to define the index 'book'

### Docker watch
In /Docker run ```BUILD_CONFIGURATION=Development docker compose --profile api watch``` 
* watching for changes on the api project
* rebuilds on change
* BUILD_CONFIGURATION determines if you build in Release or Development on the .NET project
* Currently one one profile active (api)

# Todo
1. When running docker watch put the build in development currently it runs release for all composes
2. Add angular web app to docker
