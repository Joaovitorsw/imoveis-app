FROM ubuntu/nginx:1.18-20.04_beta   
WORKDIR /usr/src/app
COPY nginx.conf /etc/nginx/nginx.conf
WORKDIR /usr/share/nginx/html
COPY ./imoveis-web/dist/imoveis-web .
EXPOSE 80


