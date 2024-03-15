package main

import (
    "fmt"
    "net/http"
	"CoreServerV2/src/api/handlers"
)

func main() {
    mux := http.NewServeMux()

    // Register the helloHandler with the http.DefaultServeMux.
    mux.Handle("/api/", http.StripPrefix("/api", handlers.ApiHandler()))
	// Register the helloHandler with the http.DefaultServeMux.
    mux.Handle("/healthcheck/", http.StripPrefix("/healthcheck", handlers.HealthcCheckHandler()))

    // Start the server on port 8080.
    fmt.Println("Server is listening on port 8080...")
    if err := http.ListenAndServe(":8080", mux); err != nil {
        fmt.Printf("Error starting server: %s\n", err)
        return
    }
}