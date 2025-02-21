# MicroLexxika

Open terminal from the root solution folder 

Running the API
</br>
`docker build -t lexxikaapi -f MicroLexxika.Api/Dockerfile .`
</br>
`docker run -it --rm -p 8080:8080 --name lexxikaapi lexxikaapi`

Running the App
</br>
`docker build -t lexxikaweb -f MicroLexxika.Web/Dockerfile .`
</br>
`docker run -it --rm -p 4200:4200 --name lexxikaweb lexxikaweb`
