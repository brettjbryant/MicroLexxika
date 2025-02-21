FROM node:22.14.0
EXPOSE 4200

WORKDIR /web
COPY ./MicroLexxika.Web/ .

RUN npm i -g @angular/cli
RUN npm i
RUN ng analytics off

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]
