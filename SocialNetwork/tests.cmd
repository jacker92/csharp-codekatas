@echo off
REM Run Docker compose build and stops after the container exits
docker-compose up --build --abort-on-container-exit
REM Removes volumes, networks and images
docker-compose down