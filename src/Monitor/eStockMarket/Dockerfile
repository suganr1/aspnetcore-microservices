## stage 1 - DOCKER RUN - MULTI CONTAINER
#FROM node:14-alpine AS build
#WORKDIR /app
#
#COPY . .
#
#WORKDIR "/app/Monitor/eStockMarket"
#
#RUN npm install
#
#RUN npm run build --dev
#
#FROM nginx:1.17.1-alpine AS dev-stage
#COPY --from=build /app/Monitor/eStockMarket/dist/eStockMarket /usr/share/nginx/html
#EXPOSE 80
#CMD ["nginx","-g","daemon off;"]

#----------------------
### STAGE 1: BUILD ###
#---------------------

# # stage 1 - INDIVIDUAL DOCKER RUN - SINGLE COINTAINER
# FROM node
# WORKDIR /app
# COPY package.json package.json
# RUN npm install --silent
# COPY . .
# RUN node_modules/.bin/ng build --prod --output-path=/dist

# # stage 2
# FROM nginx:alpine
# WORKDIR /app
# RUN rm -rf /usr/share/nginx/html/*
# #COPY nginx.conf /etc/nginx/conf.d/default.conf
# COPY --from=0 /dist /usr/share/nginx/html

# CMD ["nginx", "-g", "daemon off;"]

#----------------------
### STAGE 1: BUILD ###
#---------------------

# Building node from LTS version
FROM node as builder

# Copying all necessary files required for npm install

COPY package.json tsconfig.json ./

# Install npm dependencies in a different folder to optimize container build process

RUN npm install

# Create application directory and copy node modules to it
RUN mkdir /e-stock-market
RUN cp -R ./node_modules ./e-stock-market

# Setting application directory as work directory
WORKDIR /e-stock-market

# Copying application code to container application directory
COPY . .

# Building the angular app
RUN npm run build

#--------------------------------------------------
### STAGE 2: Setup nginx and Deploy application ###
#--------------------------------------------------
FROM nginx:latest

## Copy defualt nginx configuration file>

COPY .nginx/default.conf /etc/nginx/conf.d

# Copy dist folder from  the builder to nginx public folder(STAGE 1)

COPY --from=builder /e-stock-market/dist/e-stock-market /usr/share/nginx/html

CMD ["nginx","-g","daemon off;"]