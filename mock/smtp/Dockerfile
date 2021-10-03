FROM node:10.15.2-alpine

RUN mkdir /user && \
    echo 'nobody:x:65534:65534:nobody:/:' > /etc/passwd && \
    echo 'nobody:x:65534:' > /etc/group

WORKDIR /app

COPY package.json .
COPY package-lock.json .
RUN npm ci

USER 65534:65534
CMD ["sh", "-c", "npx fake-smtp-server \
  --auth $USER:$PASS \
  --http-ip $HTTP_IP \
  --http-port $HTTP_PORT \
  --smtp-ip $SMTP_IP\
  --smtp-port $SMTP_PORT"]
