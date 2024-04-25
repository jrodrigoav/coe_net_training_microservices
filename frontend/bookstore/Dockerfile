FROM node:lts-alpine3.19 as build
WORKDIR /opt/forms
COPY package*.json ./
RUN npm install
COPY . ./
RUN npm run build

FROM nginx:stable-alpine
COPY --from=build /opt/forms/dist/frontend/browser /usr/share/nginx/html
COPY ./default.conf /etc/nginx/conf.d/default.conf