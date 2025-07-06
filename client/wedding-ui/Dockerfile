FROM node:20 AS build
WORKDIR /app
COPY . .
RUN npm ci
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist/wedding-ui/browser /usr/share/nginx/html
EXPOSE 80
