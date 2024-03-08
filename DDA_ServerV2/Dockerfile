# Use the official Golang image as the base image.
FROM golang:1.20 as builder

# Set the working directory to /app.
WORKDIR /app

# Copy go.mod and go.sum files and download dependencies.
COPY go.mod go.sum ./
RUN go mod download

# Copy the entire project source code into the container.
COPY . .

# Build the Go application inside the container.
RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o /app/build/main src/cmd/server/main.go

# Use a minimal image as the final base image.
FROM gcr.io/distroless/base-debian10

# Copy the binary from the builder stage.
COPY --from=builder /app/build/main /app/main

# Set the entry point for the container.
ENTRYPOINT ["/app/main"]