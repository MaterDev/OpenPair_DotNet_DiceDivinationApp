package handlers

import (
	"fmt"
	"net/http"
)

// helloHandler responds to HTTP requests with a greeting.
func HealthcCheckHandler() http.Handler {
	mux := http.NewServeMux()

	mux.HandleFunc("/", healthCheckHandler)
    mux.HandleFunc("/pg", healthCheckHandler_pg)

	return mux
}

func healthCheckHandler(w http.ResponseWriter, r *http.Request) {

    // This swotcj statement is used to handle different HTTP methods
	switch r.Method {
        case http.MethodGet:
            fmt.Fprintln(w, "Server is healthy and running.")
        default:
            // Respond with an error or not allowed status
            http.Error(w, "Method is not supported.", http.StatusMethodNotAllowed)
	}
}

func healthCheckHandler_pg(w http.ResponseWriter, r *http.Request) {
    // This swotcj statement is used to handle different HTTP methods
    switch r.Method {
        case http.MethodGet:
            // Here will add the logic to check the health of the PostgreSQL database
            fmt.Fprintln(w, "PostgreSQL is healthy and running.")
        default:
            // Respond with an error or not allowed status
            http.Error(w, "Method is not supported.", http.StatusMethodNotAllowed)
    }
}
