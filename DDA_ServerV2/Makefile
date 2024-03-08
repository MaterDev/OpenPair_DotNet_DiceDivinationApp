.PHONY: build

# Run the server using air for hot reloading
dev:
	air

# Build the application
build:
	go build -o build/main src/cmd/server/main.go

# Run the application locally
run:
	go run src/cmd/server/main.go

# Clean up the build directory
clean:
	rm -rf build/main

# Build the Docker image
docker-build:
	docker build -t DDA_SERVERV-2 .

# Run the Docker container
docker-run: docker-build
	docker run -p 8080:8080 DDA_SERVERV-2