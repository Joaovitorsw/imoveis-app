FROM ubuntu/nginx:1.18-20.04_beta   
WORKDIR /usr/src/app
COPY nginx.conf /etc/nginx/nginx.conf
WORKDIR /usr/share/nginx/html
COPY ./imoveis-web-nginx/dist/imoveis-web .
EXPOSE 80

CMD /bin/sh -c "envsubst '\$PORT' < /etc/nginx/conf.d/default.conf.template > /etc/nginx/conf.d/default.conf" && nginx -g 'daemon off;'
