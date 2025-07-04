FROM node:20 AS build
WORKDIR /app
COPY . .
RUN npm ci
RUN npm run build -- --output-path=dist

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
