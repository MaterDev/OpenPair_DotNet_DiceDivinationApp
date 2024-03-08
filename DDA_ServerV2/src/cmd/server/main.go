package main

import (
    "fmt"
    "net/http"
)

// helloHandler responds to HTTP requests with a greeting.
func helloHandler(w http.ResponseWriter, r *http.Request) {
    // For simplicity, we're ignoring the error that Write can return here.
    fmt.Fprintf(w, "Hello, you've requested: %s\n", r.URL.Path)
}

func main() {
    // Register the helloHandler with the http.DefaultServeMux.
    http.HandleFunc("/", helloHandler)

    // Start the server on port 8080.
    fmt.Println("Server is listening on port 8080...")
    if err := http.ListenAndServe(":8080", nil); err != nil {
        fmt.Printf("Error starting server: %s\n", err)
        return
    }
}