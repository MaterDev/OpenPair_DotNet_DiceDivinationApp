package handlers

import (
	"fmt"
	"net/http"
)

// helloHandler responds to HTTP requests with a greeting.
func ApiHandler() http.Handler {
	// ServeMux is an HTTP request multiplexer. It matches the URL of each incoming request against a list of registered patterns and calls the handler for the pattern that most closely matches the URL.
	mux := http.NewServeMux()

    // Register sub-handlers with the ServeMux.
    mux.HandleFunc("/1", apiHandler1)
    mux.HandleFunc("/2", apiHandler2)

    return mux
}

func apiHandler1(w http.ResponseWriter, r *http.Request) {
    fmt.Fprintln(w, "Hello, client 1!")
}

func apiHandler2(w http.ResponseWriter, r *http.Request) {
    fmt.Fprintln(w, "Hello, client 2!")
}
