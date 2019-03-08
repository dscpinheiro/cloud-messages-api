Run API locally:
docker-compose build --pull && docker-compose up

Run unit tests:
docker build --pull --target testrunner -t cloud-aud-messages:unittests .
docker run --rm cloud-aud-messages:unittests